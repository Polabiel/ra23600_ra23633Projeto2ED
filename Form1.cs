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
    ListaDupla<PalavraEDica> lista1 = new ListaDupla<PalavraEDica>();
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
    /// Handles the click event for the "Read File" button.
    /// Reads data from a file and displays it in the list.
    /// </summary>
    private void btnLerArquivo1_Click(object sender, EventArgs e)
    {
      FazerLeitura(ref lista1);
      ExibirDados(lista1, lsbDados, Direcao.paraFrente);
      ExibirRegistroAtual();
    }

    /// <summary>
    /// Reads data from a file and populates the provided list and VetorDicionario.
    /// </summary>
    /// <param name="qualLista">The list to populate with data.</param>
    private void FazerLeitura(ref ListaDupla<PalavraEDica> qualLista)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
      if (ofd.ShowDialog() != DialogResult.OK)
        return;

      qualLista = new ListaDupla<PalavraEDica>();
      vetorDicionario.Limpar(); // Limpa o vetor antes de popular

      using (StreamReader sr = new StreamReader(ofd.FileName))
      {
        string linha;
        while ((linha = sr.ReadLine()) != null)
        {
          if (linha.Length < 30)
            continue; // Ignore invalid lines

          // Para lista1 (ListaDupla)
          string palavra = linha.Substring(0, 30).Trim();
          string dica = linha.Substring(30).Trim();
          var registro = new PalavraEDica(palavra, dica);
          qualLista.InserirEmOrdem(registro);

          // Para VetorDicionario
          Dicionario dic = new Dicionario();
          dic.LerLinha(linha);
          vetorDicionario.Adicionar(dic);
        }
      }

      // Atualiza a tabela após leitura
      ExibirVetorDicionarioNaTabela();
    }

    /// <summary>
    /// Handles the click event for the "Include" button.
    /// Adds a new word and hint to the list.
    /// </summary>
    private void btnIncluir_Click(object sender, EventArgs e)
    {
      modoAtual = Modo.Inclusao;
      txtRA.Text = "";
      txtNome.Text = "";
      txtRA.Enabled = true;
      txtNome.Enabled = false;  // Será habilitado após verificar que a palavra não existe
      txtRA.Focus();
    }

    /// <summary>
    /// Handles the click event for the "Search" button.
    /// Searches for a word in the list.
    /// </summary>
    private void btnBuscar_Click(object sender, EventArgs e)
    {
      string palavra = txtRA.Text.Trim();
      if (string.IsNullOrEmpty(palavra))
      {
        MessageBox.Show("Enter the word to search.");
        return;
      }
      var registro = new PalavraEDica(palavra, "");
      if (lista1.Existe(registro))
      {
        // Position the current pointer on the found node
        ExibirRegistroAtual();
      }
      else
        MessageBox.Show("Word not found!");
    }

    /// <summary>
    /// Handles the click event for the "Delete" button.
    /// Deletes the currently selected word from the list.
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

    /// <summary>
    /// Displays the data from the list in the specified direction.
    /// </summary>
    /// <param name="aLista">The list to display.</param>
    /// <param name="lsb">The ListBox to populate.</param>
    /// <param name="qualDirecao">The direction to display the data.</param>
    private void ExibirDados(ListaDupla<PalavraEDica> aLista, ListBox lsb, Direcao qualDirecao)
    {
      lsb.Items.Clear();
      var dadosDaLista = aLista.Listagem(qualDirecao);
      foreach (PalavraEDica registro in dadosDaLista)
        lsb.Items.Add(registro);
    }

    private void tabControl1_Enter(object sender, EventArgs e)
    {
      rbFrente.PerformClick();
    }

    private void rbFrente_Click(object sender, EventArgs e)
    {
      ExibirDados(lista1, lsbDados, Direcao.paraFrente);
    }

    private void rbTras_Click(object sender, EventArgs e)
    {
      ExibirDados(lista1, lsbDados, Direcao.paraTras);
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
      lista1 = new ListaDupla<PalavraEDica>();
      vetorDicionario.Limpar();

      using (var sr = new StreamReader(caminhoArquivoSelecionado))
      {
        string linha;
        while ((linha = sr.ReadLine()) != null)
        {
          if (linha.Length < 30)
            continue;
          string palavra = linha.Substring(0, 30).Trim();
          string dica = linha.Substring(30).Trim();
          var registro = new PalavraEDica(palavra, dica);
          lista1.InserirAposFim(registro);

          // Também adiciona ao VetorDicionario
          Dicionario dic = new Dicionario();
          dic.LerLinha(linha);
          vetorDicionario.Adicionar(dic);
        }
      }
      lista1.PosicionarNoInicio();
      ExibirRegistroAtual();
      ExibirDados(lista1, lsbDados, Direcao.paraFrente);
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
      if (tableData.Rows.Count > 0)
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
      if (tableData.Rows.Count > 0)
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
      if (tableData.Rows.Count > 0)
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

    private void btnEditar_Click(object sender, EventArgs e)
    {
      if (vetorDicionario.QtosDados == 0) return;
    
      modoAtual = Modo.Edicao;
      txtRA.Enabled = false;  // Não permite alterar a palavra, apenas a dica
      txtNome.Enabled = true;
      txtNome.Focus();
    }

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
          if (vetorDicionario.Existe(palavra) >= 0)
          {
              MessageBox.Show("Esta palavra já existe no dicionário!");
              return;
          }
          
          Dicionario novo = new Dicionario();
          novo.Palavra = palavra;
          novo.Dica = dica;
          
          int novaPosicao = vetorDicionario.InserirEmOrdem(novo);
          if (novaPosicao >= 0)
              vetorDicionario.PosicaoAtual = novaPosicao;
          
          ExibirVetorDicionarioNaTabela();
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
      tableData.Rows.Clear();
      var dados = vetorDicionario.ListarDados();
      foreach (var item in dados)
      {
        // Add row: position, word, hint
        tableData.Rows.Add(item.posicao, item.palavra, item.dica);
      }
      // Select the row of the current position, if there are data
      if (vetorDicionario.QtosDados > 0 && vetorDicionario.PosicaoAtual >= 0)
      {
        tableData.ClearSelection();
        if (vetorDicionario.PosicaoAtual < tableData.Rows.Count)
          tableData.Rows[vetorDicionario.PosicaoAtual].Selected = true;
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
