using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Timers;
using System.IO.Ports; // Adicione se não existir, mas usaremos System.Windows.Forms.Timer

// ra23600 ra23305
// Gabriel - Julio
namespace apListaLigada
{
    /// <summary>
    /// Represents the main form for managing a list of words and their hints.
    /// </summary>
    public partial class FrmAlunos : Form
    {
        private string caminhoArquivoSelecionado = null; // Stores the selected file path
        private VetorDicionario vetorDicionario = new VetorDicionario(); // Field for VetorDicionario
        private bool jogoEmAndamento = false;
        private Random random = new Random();
        private ConexaoSerial conexaoSerial = new ConexaoSerial();

        private string palavraAtual = string.Empty;
        private int contadorErros = 0;
        private int pontosAtuais = 0;
        private const int MAX_ERROS = 8;

        private System.Windows.Forms.Timer timerJogo;
        private int tempoRestante = 0;
        private const int TEMPO_INICIAL = 60; // segundos

        private System.Windows.Forms.Timer timerEnforcado;
        private int enforcadoElapsedTime = 0; // in seconds
        private int enforcadoInitialTop = 0;

        private enum Modo { Navegacao, Inclusao, Edicao }
        private Modo modoAtual = Modo.Navegacao;

        public FrmAlunos()
        {
            InitializeComponent();
            tableData.CellClick += tableData_CellClick;
            txtRA.Leave += txtRA_Leave;
            btnIniciarJogo.Click += btnIniciarJogo_Click;
            checkBoxDica.CheckedChanged += checkBoxDica_CheckedChanged;

            // Registrar eventos para os botões de letras
            RegistrarEventosDosBotoes();

            // Inicialização do timer
            timerJogo = new System.Windows.Forms.Timer();
            timerJogo.Interval = 1000; // 1 segundo
            timerJogo.Tick += timerJogo_Tick;

            // Inicialização do timer para animação do "enforcado"
            timerEnforcado = new System.Windows.Forms.Timer();
            timerEnforcado.Interval = 250; // adjust as desired for smoother/faster animation
            timerEnforcado.Tick += timerEnforcado_Tick;

            // Save the initial vertical position of the "enforcado" for later use
            enforcadoInitialTop = enforcado.Top;

            img_ERRO1.Visible = false;
            img_ERRO2.Visible = false;
            img_ERRO3.Visible = false;
            img_ERRO4.Visible = false;
            img_ERRO5.Visible = false;
            img_ERRO6.Visible = false;
            img_ERRO_0_7.Visible = false;
            img_ERRO_1_7.Visible = false;
            img_ERRO8.Visible = false;

            labelPort.Visible = false;
            labelPortValor.Visible = false;
        }

        /// <summary>
        /// Evento de clique para os botões de letras. Filtra a tabela para mostrar apenas palavras que começam com a letra selecionada.
        /// </summary>
        private void BotaoLetra_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn) || btn.Tag == null)
                return;

            char letra = (char)btn.Tag;
            // Filtra os dados do VetorDicionario e exibe apenas os que começam com a letra selecionada
            var dados = vetorDicionario.ListarDados();
            tableData.Rows.Clear();
            foreach (var (posicao, palavra, dica) in dados)
            {
                if (!string.IsNullOrEmpty(palavra) && char.ToUpper(palavra[0]) == letra)
                    tableData.Rows.Add(posicao, palavra, dica);
            }
            // Limpa seleção e status
            if (tableData.Rows.Count > 0)
            {
                tableData.ClearSelection();
                tableData.Rows[0].Selected = true;
                tableData.FirstDisplayedScrollingRowIndex = 0;
            }
            else
            {
                toolStripStatusLabel1.Text = $"Nenhuma palavra com '{letra}'";
            }
        }

        /// <summary>
        /// Handles the click event for the "Read File" button.
        /// Reads data from a file and displays it in the DataGridView.
        /// </summary>
        private void btnLerArquivo1_Click(object sender, EventArgs e)
        {
            FazerLeitura();
            ExibirVetorDicionarioNaTabela();
            ExibirRegistroAtual();
        }

        /// <summary>
        /// Reads data from a file and populates VetorDicionario.
        /// </summary>
        private void FazerLeitura()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            caminhoArquivoSelecionado = ofd.FileName;
            vetorDicionario.Limpar(); // Limpa o vetor antes de popular

            using (StreamReader sr = new StreamReader(ofd.FileName))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    if (linha.Length < 30)
                        continue; // Ignore invalid lines

                    // Para VetorDicionario
                    Dicionario dic = new Dicionario();
                    dic.LerLinha(linha);
                    vetorDicionario.Adicionar(dic);
                }
            }

            if (vetorDicionario.QtosDados > 0)
                vetorDicionario.PosicaoAtual = 0;
        }

        /// <summary>
        /// Handles the click event for the "Include" button.
        /// Prepares the form for a new entry.
        /// </summary>
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            modoAtual = Modo.Inclusao;
            txtRA.Text = "";
            txtNome.Text = "";
            txtRA.Enabled = true;
            txtNome.Enabled = true;
            txtRA.Focus();

            // Limpa seleção na tabela para evitar confusão visual
            tableData.ClearSelection();
        }

        /// <summary>
        /// Handles the click event for the "Search" button.
        /// Searches for a word in VetorDicionario and updates the selection in DataGridView.
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string palavra = txtRA.Text.Trim();
            if (string.IsNullOrEmpty(palavra))
            {
                MessageBox.Show("Digite uma palavra para buscar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRA.Focus();
                return;
            }

            int pos = vetorDicionario.Existe(palavra);
            if (pos >= 0)
            {
                modoAtual = Modo.Navegacao;
                vetorDicionario.PosicaoAtual = pos;
                ExibirRegistroAtual();

                // Seleciona linha na tabela e garante que esteja visível
                if (pos < tableData.Rows.Count)
                {
                    tableData.ClearSelection();
                    tableData.Rows[pos].Selected = true;
                    tableData.FirstDisplayedScrollingRowIndex = pos;
                }
            }
            else
                MessageBox.Show("Palavra não encontrada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Handles the click event for the "Delete" button.
        /// Deletes the currently selected word from the VetorDicionario and updates the DataGridView.
        /// </summary>
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (vetorDicionario.QtosDados == 0) return;

            var (palavra, _) = vetorDicionario.GetAtual();
            var resp = MessageBox.Show($"Deseja realmente excluir a palavra '{palavra}'?",
                                     "Confirmação", MessageBoxButtons.YesNo);
            if (resp == DialogResult.Yes)
            {
                vetorDicionario.ExcluirNaPosicao(vetorDicionario.PosicaoAtual);
                if (vetorDicionario.PosicaoAtual >= vetorDicionario.QtosDados && vetorDicionario.QtosDados > 0)
                    vetorDicionario.PosicaoAtual = vetorDicionario.QtosDados - 1;

                ExibirVetorDicionarioNaTabela();
                ExibirRegistroAtual();
            }
        }

        /// <summary>
        /// Handles the form closing event.
        /// Saves the list data to the selected file.
        /// </summary>
        private void FrmAlunos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(caminhoArquivoSelecionado))
                return;

            try
            {
                vetorDicionario.SalvarArquivo(caminhoArquivoSelecionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            ExibirVetorDicionarioNaTabela();
        }

        /// <summary>
        /// Handles the form load event.
        /// Initializes the list and loads data from a file.
        /// </summary>
        private void FrmAlunos_Load(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("No file selected. The program will close.");
                Close();
                return;
            }

            caminhoArquivoSelecionado = ofd.FileName;
            vetorDicionario.Limpar();

            using (var sr = new StreamReader(caminhoArquivoSelecionado))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    if (linha.Length < 30)
                        continue;

                    // Adiciona ao VetorDicionario
                    Dicionario dic = new Dicionario();
                    dic.LerLinha(linha);
                    vetorDicionario.Adicionar(dic);
                }
            }

            if (vetorDicionario.QtosDados > 0)
                vetorDicionario.PosicaoAtual = 0;

            ExibirRegistroAtual();
            ExibirVetorDicionarioNaTabela();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            if (vetorDicionario.QtosDados == 0) return;

            modoAtual = Modo.Navegacao;
            vetorDicionario.PosicaoAtual = 0;
            ExibirRegistroAtual();

            // Seleciona a linha correspondente na tabela
            if (tableData.Rows.Count > 0)
            {
                tableData.ClearSelection();
                tableData.Rows[0].Selected = true;
                tableData.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (vetorDicionario.QtosDados == 0) return;

            modoAtual = Modo.Navegacao;
            if (vetorDicionario.PosicaoAtual > 0)
                vetorDicionario.PosicaoAtual--;

            ExibirRegistroAtual();

            // Seleciona a linha correspondente na tabela
            if (tableData.Rows.Count > 0 && vetorDicionario.PosicaoAtual >= 0)
            {
                tableData.ClearSelection();
                tableData.Rows[vetorDicionario.PosicaoAtual].Selected = true;
                tableData.FirstDisplayedScrollingRowIndex = vetorDicionario.PosicaoAtual;
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (vetorDicionario.QtosDados == 0) return;

            modoAtual = Modo.Navegacao;
            if (vetorDicionario.PosicaoAtual < vetorDicionario.QtosDados - 1)
                vetorDicionario.PosicaoAtual++;

            ExibirRegistroAtual();

            // Seleciona a linha correspondente na tabela
            if (tableData.Rows.Count > 0 && vetorDicionario.PosicaoAtual < tableData.Rows.Count)
            {
                tableData.ClearSelection();
                tableData.Rows[vetorDicionario.PosicaoAtual].Selected = true;
                tableData.FirstDisplayedScrollingRowIndex = vetorDicionario.PosicaoAtual;
            }
        }

        private void btnFim_Click(object sender, EventArgs e)
        {
            if (vetorDicionario.QtosDados == 0) return;

            modoAtual = Modo.Navegacao;
            vetorDicionario.PosicaoAtual = vetorDicionario.QtosDados - 1;
            ExibirRegistroAtual();

            // Seleciona a linha correspondente na tabela
            if (tableData.Rows.Count > 0 && vetorDicionario.PosicaoAtual < tableData.Rows.Count)
            {
                tableData.ClearSelection();
                tableData.Rows[vetorDicionario.PosicaoAtual].Selected = true;
                tableData.FirstDisplayedScrollingRowIndex = vetorDicionario.PosicaoAtual;
            }
        }

        /// <summary>
        /// Displays the current record in the form fields.
        /// </summary>
        private void ExibirRegistroAtual()
        {
            // Se estamos em modo de inclusão ou edição, não atualiza os campos
            if (modoAtual == Modo.Inclusao || modoAtual == Modo.Edicao)
                return;

            if (vetorDicionario.QtosDados == 0)
            {
                txtRA.Text = "";
                txtNome.Text = "";
                toolStripStatusLabel1.Text = "0/0";
                return;
            }

            var (palavra, dica) = vetorDicionario.GetAtual();
            txtRA.Text = palavra;
            txtNome.Text = dica;
            toolStripStatusLabel1.Text = $"Registro: {vetorDicionario.PosicaoAtual + 1}/{vetorDicionario.QtosDados}";
        }

        /// <summary> 
        /// Handles the click event for the "Edit" button.
        /// Enables editing of the current record's hint.
        /// </summary>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Verificar se há registros e se a posição atual é válida
            if (vetorDicionario.QtosDados == 0 || vetorDicionario.PosicaoAtual < 0 || vetorDicionario.PosicaoAtual >= vetorDicionario.QtosDados)
            {
                MessageBox.Show("Não há registro selecionado para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            modoAtual = Modo.Edicao;
            txtRA.Enabled = false;  // Não permite alterar a palavra, apenas a dica
            txtNome.Enabled = true;
            txtNome.Focus();
        }

        // No método btnSalvar_Click em Form1.cs
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoAtual == Modo.Inclusao)
            {
                string palavra = txtRA.Text.Trim();
                string dica = txtNome.Text.Trim();

                if (string.IsNullOrEmpty(palavra))
                {
                    MessageBox.Show("Por favor, informe a palavra.");
                    txtRA.Focus();
                    return;
                }

                // Verifica novamente se a palavra já existe
                int posExistente = vetorDicionario.Existe(palavra);
                if (posExistente >= 0)
                {
                    MessageBox.Show("Esta palavra já existe no dicionário!", "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Mostra o registro existente
                    vetorDicionario.PosicaoAtual = posExistente;
                    modoAtual = Modo.Navegacao;
                    ExibirRegistroAtual();
                    ExibirVetorDicionarioNaTabela();
                    return;
                }

                // Cria um novo registro e adiciona ao VetorDicionario
                Dicionario novo = new Dicionario();
                novo.Palavra = palavra;
                novo.Dica = dica;

                int novaPosicao = vetorDicionario.InserirEmOrdem(novo);
                if (novaPosicao >= 0)
                {
                    vetorDicionario.PosicaoAtual = novaPosicao;

                    // Atualiza o DataGridView
                    ExibirVetorDicionarioNaTabela();

                    // Seleciona a nova linha no DataGridView
                    if (novaPosicao < tableData.Rows.Count)
                    {
                        tableData.ClearSelection();
                        tableData.Rows[novaPosicao].Selected = true;
                        tableData.FirstDisplayedScrollingRowIndex = novaPosicao;
                    }

                    // Salva imediatamente após inclusão
                    if (!string.IsNullOrEmpty(caminhoArquivoSelecionado))
                    {
                        try
                        {
                            vetorDicionario.SalvarArquivo(caminhoArquivoSelecionado);
                            toolStripStatusLabel1.Text = "Palavra adicionada e arquivo salvo com sucesso!";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao salvar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else if (modoAtual == Modo.Edicao)
            {
                // Verificar se há registros e se a posição atual é válida
                if (vetorDicionario.QtosDados == 0 || vetorDicionario.PosicaoAtual < 0 || vetorDicionario.PosicaoAtual >= vetorDicionario.QtosDados)
                {
                    MessageBox.Show("Operação inválida: não há registro selecionado para salvar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    modoAtual = Modo.Navegacao;
                    txtRA.Enabled = true;
                    txtNome.Enabled = true;
                    ExibirRegistroAtual();
                    return;
                }

                string dica = txtNome.Text.Trim();
                vetorDicionario.AlterarDica(vetorDicionario.PosicaoAtual, dica);
                ExibirVetorDicionarioNaTabela();

                // Salva imediatamente após edição
                if (!string.IsNullOrEmpty(caminhoArquivoSelecionado))
                {
                    try
                    {
                        vetorDicionario.SalvarArquivo(caminhoArquivoSelecionado);
                        toolStripStatusLabel1.Text = "Dica alterada e arquivo salvo com sucesso!";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao salvar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            modoAtual = Modo.Navegacao;
            txtRA.Enabled = true;
            txtNome.Enabled = true;
            ExibirRegistroAtual();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            modoAtual = Modo.Navegacao;
            txtRA.Enabled = true;
            txtNome.Enabled = true;
            ExibirRegistroAtual();
        }

        /// <summary>
        /// Displays the data from VetorDicionario in the DataGridView tableData.
        /// </summary>
        private void ExibirVetorDicionarioNaTabela()
        {
            try
            {
                tableData.Rows.Clear();
                var dados = vetorDicionario.ListarDados();
                foreach (var (posicao, palavra, dica) in dados)
                {
                    tableData.Rows.Add(posicao, palavra, dica);
                }

                if (vetorDicionario.QtosDados > 0 && vetorDicionario.PosicaoAtual >= 0)
                {
                    tableData.ClearSelection();
                    if (vetorDicionario.PosicaoAtual < tableData.Rows.Count)
                    {
                        tableData.Rows[vetorDicionario.PosicaoAtual].Selected = true;
                        tableData.FirstDisplayedScrollingRowIndex = vetorDicionario.PosicaoAtual;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exibir dados na tabela: {ex.Message}", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event triggered when the tpCadastro tab is selected.
        /// </summary>
        private void tpCadastro_Enter(object sender, EventArgs e)
        {
            ExibirVetorDicionarioNaTabela();
        }

        private void txtRA_Leave(object sender, EventArgs e)
        {
            if (modoAtual != Modo.Inclusao) return;

            string palavra = txtRA.Text.Trim();
            if (string.IsNullOrEmpty(palavra)) return;

            int posicao = vetorDicionario.Existe(palavra);
            if (posicao >= 0)
            {
                MessageBox.Show("Esta palavra já existe no dicionário!");
                modoAtual = Modo.Navegacao;
                ExibirRegistroAtual();
                return;
            }

            txtNome.Enabled = true;
            txtNome.Focus();
        }

        private void tableData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < vetorDicionario.QtosDados)
            {
                modoAtual = Modo.Navegacao;
                vetorDicionario.PosicaoAtual = e.RowIndex;
                ExibirRegistroAtual();
            }
        }

        /// <summary>
        /// Handles the click event for the "Start Game" button.
        /// Initiates the game by selecting a random word and hint.
        /// </summary>
        private void btnIniciarJogo_Click(object sender, EventArgs e)
        {
            if (vetorDicionario.QtosDados == 0)
            {
                MessageBox.Show("Não há palavras cadastradas para iniciar o jogo.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Reinicia os contadores e variáveis do jogo
            contadorErros = 0;
            pontosAtuais = 0;
            labelPontosValor.Text = "0";
            label6.Text = "0";

            // Reset das imagens da forca
            img_ERRO1.Visible = false;
            img_ERRO2.Visible = false;
            img_ERRO3.Visible = false;
            img_ERRO4.Visible = false;
            img_ERRO5.Visible = false;
            img_ERRO6.Visible = false;
            img_ERRO_0_7.Visible = false;
            img_ERRO_1_7.Visible = false;
            img_ERRO8.Visible = false;
            // Redefina outros componentes visuais da forca conforme necessário

            cabeca_normal.Visible = true; // Exibe a cabeça normal do boneco da forca
            cabeca_normal.Image = Properties.Resources.Forca_05;

            // Sorteia uma posição aleatória no vetor
            int posicaoSorteada = random.Next(vetorDicionario.QtosDados);

            // Define a posição atual como a sorteada
            vetorDicionario.PosicaoAtual = posicaoSorteada;

            // Obtém a palavra e a dica sorteadas
            var (palavra, dica) = vetorDicionario.GetAtual();

            // Converte a palavra para maiúsculo e remove espaços em branco à direita
            palavraAtual = palavra.ToUpper().TrimEnd();

            // Oculta a guia de cadastro para evitar trapaças
            tpCadastro.Parent = null;
            jogoEmAndamento = true;

            // Preparar a interface do jogo
            labelDicaValor.Text = checkBoxDica.Checked ? dica : "_______________";

            // Configurar o tableForca
            tableForca.Rows.Clear();
            tableForca.Columns.Clear();

            // Define o número de colunas baseado no comprimento da palavra processada
            for (int i = 0; i < palavraAtual.Length; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Width = 30; // Define uma largura fixa para cada coluna
                col.HeaderText = (i + 1).ToString();
                tableForca.Columns.Add(col);
            }

            // Adiciona uma linha para mostrar as letras
            tableForca.Rows.Add();

            // Inicializa as células com espaços em branco ou outro caractere visual para representar letras ocultas
            for (int i = 0; i < palavraAtual.Length; i++)
            {
                tableForca.Rows[0].Cells[i].Value = "_";
            }

            // Ajustar a altura da linha
            tableForca.RowTemplate.Height = 30;
            tableForca.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Centralizar o conteúdo das células
            tableForca.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableForca.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);

            // Habilita todos os botões de letras
            foreach (Control ctrl in flowLayoutPanel6.Controls)
            {
                if (ctrl is Button button)
                    button.Enabled = true;
            }

            // Exibe uma mensagem informativa
            toolStripStatusLabel1.Text = "Jogo iniciado! Adivinhe a palavra.";

            // Tempo inicial do jogo
            tempoRestante = TEMPO_INICIAL;
            labelTempoRestante.Text = tempoRestante.ToString();

            // Inicia o timer
            timerJogo.Stop();
            timerJogo.Start();

            if (checkBoxArduino.Checked && conexaoSerial.EstaConectado)
            {
                conexaoSerial.EnviarInicioJogo();
            }
        }

        /// <summary>
        /// Ends the game and restores the interface.
        /// </summary>
        public void FinalizarJogo()
        {
            if (jogoEmAndamento)
            {
                timerJogo.Stop();
                tpCadastro.Parent = tabControl1;
                jogoEmAndamento = false;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event for the checkBoxDica.
        /// Updates the displayed hint based on the checkbox state.
        /// </summary>
        private void checkBoxDica_CheckedChanged(object sender, EventArgs e)
        {
            if (!jogoEmAndamento)
                return;

            var (_, dica) = vetorDicionario.GetAtual();
            labelDicaValor.Text = checkBoxDica.Checked ? dica : "_______________";
        }

        /// <summary>
        /// Handles the click event for the letter buttons.
        /// </summary>
        private void LetraButton_Click(object sender, EventArgs e)
        {
            if (!jogoEmAndamento)
                return;

            Button btn = sender as Button;
            if (btn == null)
                return;

            // Obtém a letra do botão pressionado
            char letra = btn.Text[0];

            // Desabilita o botão para que não possa ser clicado novamente
            btn.Enabled = false;

            // Obtém o objeto Dicionario atual
            Dicionario dicionarioAtual = vetorDicionario[vetorDicionario.PosicaoAtual];

            // Verifica se a letra existe na palavra
            bool acertou = dicionarioAtual.TentarLetra(letra);

            if (acertou)
            {
                // Atualiza a exibição das letras na tabela
                AtualizarExibicaoPalavra(dicionarioAtual);

                // Incrementa pontos
                pontosAtuais += 10;
                labelPontosValor.Text = pontosAtuais.ToString();

                // Verifica se o jogo foi concluído com sucesso
                VerificarFimDeJogo(dicionarioAtual);
            }
            else
            {
                // Incrementa contador de erros
                contadorErros++;
                label6.Text = contadorErros.ToString(); // Atualiza o contador de erros na interface

                // Atualiza a representação visual do boneco da forca
                AtualizarImagemForca();

                if (checkBoxArduino.Checked && conexaoSerial.EstaConectado)
                {
                    conexaoSerial.EnviarErros(contadorErros);
                }

                // Verifica se o jogo acabou por excesso de erros
                if (contadorErros >= MAX_ERROS)
                {
                    cabeca_normal.Image = Properties.Resources.Forca_1_05;

                    MessageBox.Show($"Fim de jogo! A palavra era: {palavraAtual}", "Derrota",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Add this line to send defeat signal to Arduino
                    if (checkBoxArduino.Checked && conexaoSerial.EstaConectado)
                    {
                        conexaoSerial.EnviarDerrota();
                    }

                    enforcado.Top = enforcadoInitialTop;
                    enforcado.Visible = true;
                    enforcadoElapsedTime = 0;
                    btnIniciarJogo.Enabled = false;

                    timerEnforcado.Start();
                    FinalizarJogo();
                }
            }
        }

        /// <summary>
        /// Updates the display of the word in the DataGridView based on guessed letters.
        /// </summary>
        private void AtualizarExibicaoPalavra(Dicionario dicionario)
        {
            if (dicionario == null) return;

            bool[] acertos = dicionario.GetAcertou();
            string palavra = dicionario.Palavra.ToUpper().TrimEnd();

            // Atualiza o grid com as letras descobertas
            for (int i = 0; i < palavra.Length && i < tableForca.Columns.Count; i++)
            {
                if (acertos[i])
                    tableForca.Rows[0].Cells[i].Value = palavra[i].ToString();
            }
        }

        /// <summary>
        /// Checks if the game has been won by checking if all letters have been guessed.
        /// </summary>
        private void VerificarFimDeJogo(Dicionario dicionario)
        {
            if (dicionario == null) return;

            bool[] acertos = dicionario.GetAcertou();
            string palavra = dicionario.Palavra.ToUpper().TrimEnd();
            bool ganhou = true;

            // Verifica se todas as letras da palavra foram descobertas
            for (int i = 0; i < palavra.Length; i++)
            {
                if (!acertos[i])
                {
                    ganhou = false;
                    break;
                }
            }

            if (ganhou)
            {
                // Adiciona pontuação bônus por finalizar a palavra
                pontosAtuais += 50;
                labelPontosValor.Text = pontosAtuais.ToString();

                if (checkBoxArduino.Checked && conexaoSerial.EstaConectado)
                {
                    conexaoSerial.EnviarVitoria();
                }

                MessageBox.Show($"Parabéns! Você acertou a palavra: {palavra}\nPontuação: {pontosAtuais}",
                    "Vitória", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Finaliza o jogo
                FinalizarJogo();
            }
        }

        /// <summary>
        /// Updates the visualization of the hangman based on the number of errors.
        /// </summary>
        private void AtualizarImagemForca()
        {
            switch (contadorErros)
            {
                case 1:
                    img_ERRO1.Visible = true;
                    img_ERRO2.Visible = true;
                    break;
                case 2:
                    img_ERRO2.Visible = false;
                    img_ERRO3.Visible = true;
                    break;
                case 3:
                    img_ERRO4.Visible = true;
                    break;
                case 4:
                    img_ERRO5.Visible = true;
                    break;
                case 5:
                    img_ERRO6.Visible = true;
                    break;
                case 6:
                    img_ERRO_0_7.Visible = true;
                    break;
                case 7:
                    img_ERRO_1_7.Visible = true;
                    break;
                case 8:
                    img_ERRO8.Visible = true;
                    break;
                    // Adicione mais casos conforme necessário para os outros componentes da forca
            }
        }

        /// <summary>
        /// Registers the click events for letter buttons.
        /// </summary>
        public void RegistrarEventosDosBotoes()
        {
            // Registre o evento Click de todos os botões no flowLayoutPanel6
            foreach (Control ctrl in flowLayoutPanel6.Controls)
            {
                if (ctrl is Button button)
                {
                    button.Click += LetraButton_Click;
                }
            }
        }

        /// <summary>
        /// Handles the Tick event for the game timer.
        /// </summary>
        private void timerJogo_Tick(object sender, EventArgs e)
        {
            if (!jogoEmAndamento)
            {
                timerJogo.Stop();
                return;
            }

            tempoRestante--;
            labelTempoRestante.Text = tempoRestante.ToString();

            if (tempoRestante <= 0)
            {
                timerJogo.Stop();
                MessageBox.Show("Tempo esgotado! Você perdeu o jogo.", "Tempo Esgotado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FinalizarJogo();
            }
        }

        /// <summary>
        /// Handles the Tick event for the "enforcado" animation timer.
        /// </summary>
        private void timerEnforcado_Tick(object sender, EventArgs e)
        {
            enforcadoElapsedTime += timerEnforcado.Interval; // counts in ms

            // Move the enforcado upward a bit each tick
            enforcado.Top -= 2;

            // After 4 seconds (4000ms), stop and reset
            if (enforcadoElapsedTime >= 4000)
            {
                timerEnforcado.Stop();
                // Optionally reset the position if desired:
                enforcado.Top = enforcadoInitialTop;
                // Re-enable the "Iniciar Jogo" button
                btnIniciarJogo.Enabled = true;
                enforcado.Visible = false;
            }
        }

        // Updated checkBoxArduino_CheckedChanged method
        private void checkBoxArduino_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxArduino.Checked)
            {
                labelPort.Visible = true;
                labelPortValor.Visible = true;

                try
                {
                    string[] portas = SerialPort.GetPortNames();

                    if (portas.Length == 0)
                    {
                        labelPortValor.Text = "N/D";
                        MessageBox.Show("Nenhuma porta serial disponível.",
                            "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        checkBoxArduino.Checked = false;
                        return;
                    }
                    else if (portas.Length == 1)
                    {
                        if (conexaoSerial.AbrirConexao(portas[0]))
                        {
                            labelPortValor.Text = portas[0];
                            toolStripStatusLabel1.Text = $"Conexão Arduino estabelecida em {portas[0]}";
                        }
                        else
                        {
                            labelPortValor.Text = "Falha";
                            MessageBox.Show($"Não foi possível estabelecer conexão na porta {portas[0]}.",
                                "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            checkBoxArduino.Checked = false;
                        }
                    }
                    else
                    {
                        Form formPortas = new Form
                        {
                            Text = "Selecionar Porta COM",
                            Size = new System.Drawing.Size(300, 150),
                            FormBorderStyle = FormBorderStyle.FixedDialog,
                            StartPosition = FormStartPosition.CenterParent,
                            MaximizeBox = false,
                            MinimizeBox = false
                        };

                        ComboBox comboPortas = new ComboBox
                        {
                            DropDownStyle = ComboBoxStyle.DropDownList,
                            Location = new System.Drawing.Point(50, 20),
                            Size = new System.Drawing.Size(200, 24),
                            Font = new System.Drawing.Font("Microsoft Sans Serif", 10F)
                        };
                        comboPortas.Items.AddRange(portas);
                        comboPortas.SelectedIndex = 0;

                        Button btnOK = new Button
                        {
                            DialogResult = DialogResult.OK,
                            Text = "Conectar",
                            Location = new System.Drawing.Point(50, 60),
                            Size = new System.Drawing.Size(90, 30)
                        };

                        Button btnCancelar = new Button
                        {
                            DialogResult = DialogResult.Cancel,
                            Text = "Cancelar",
                            Location = new System.Drawing.Point(160, 60),
                            Size = new System.Drawing.Size(90, 30)
                        };

                        formPortas.Controls.Add(comboPortas);
                        formPortas.Controls.Add(btnOK);
                        formPortas.Controls.Add(btnCancelar);
                        formPortas.AcceptButton = btnOK;
                        formPortas.CancelButton = btnCancelar;

                        if (formPortas.ShowDialog() == DialogResult.OK)
                        {
                            string portaSelecionada = comboPortas.SelectedItem.ToString();
                            if (conexaoSerial.AbrirConexao(portaSelecionada))
                            {
                                labelPortValor.Text = portaSelecionada;
                                toolStripStatusLabel1.Text = $"Conexão Arduino estabelecida em {portaSelecionada}";
                            }
                            else
                            {
                                labelPortValor.Text = "Falha";
                                MessageBox.Show($"Não foi possível estabelecer conexão na porta {portaSelecionada}.",
                                    "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                checkBoxArduino.Checked = false;
                            }
                        }
                        else
                        {
                            checkBoxArduino.Checked = false;
                            labelPort.Visible = false;
                            labelPortValor.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao tentar abrir conexão: {ex.Message}",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBoxArduino.Checked = false;
                    labelPort.Visible = false;
                    labelPortValor.Visible = false;
                }
            }
            else
            {
                conexaoSerial.FecharConexao();
                labelPort.Visible = false;
                labelPortValor.Visible = false;
                toolStripStatusLabel1.Text = "Conexão Arduino encerrada";
            }
        }
    }
}