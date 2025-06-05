using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// ra23600 ra23305
// Gabriel - Andrew
namespace apListaLigada
{
    /// <summary>
    /// Represents the main form for managing a list of words and their hints.
    /// </summary>
    public partial class FrmAlunos : Form
    {
        private string caminhoArquivoSelecionado = null; // Stores the selected file path
        private VetorDicionario vetorDicionario = new VetorDicionario(); // Field for VetorDicionario

        private enum Modo { Navegacao, Inclusao, Edicao }
        private Modo modoAtual = Modo.Navegacao;

        public FrmAlunos()
        {
            InitializeComponent();
            tableData.CellClick += tableData_CellClick;
            txtRA.Leave += txtRA_Leave;
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
            if (vetorDicionario.QtosDados == 0) return;

            modoAtual = Modo.Edicao;
            txtRA.Enabled = false;  // Não permite alterar a palavra, apenas a dica
            txtNome.Enabled = true;
            txtNome.Focus();
        }

        /// <summary>
        /// Handles the click event for the "Save" button.
        /// Saves a new or edited record to VetorDicionario and updates the DataGridView.
        /// </summary>
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
                }
            }
            else if (modoAtual == Modo.Edicao)
            {
                string dica = txtNome.Text.Trim();
                vetorDicionario.AlterarDica(vetorDicionario.PosicaoAtual, dica);
                ExibirVetorDicionarioNaTabela();
            }

            modoAtual = Modo.Navegacao;
            txtRA.Enabled = true; // Restaura o estado normal dos campos
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
    }
}
