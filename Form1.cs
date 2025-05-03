using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace apListaLigada
{
  /// <summary>
  /// Represents the main form for managing a list of words and their hints.
  /// </summary>
  public partial class FrmAlunos : Form
  {
    ListaDupla<PalavraEDica> lista1 = new ListaDupla<PalavraEDica>();
    private string caminhoArquivoSelecionado = null; // Stores the selected file path

    /// <summary>
    /// Initializes a new instance of the <see cref="FrmAlunos"/> class.
    /// </summary>
    public FrmAlunos()
    {
      InitializeComponent();
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
    /// Reads data from a file and populates the provided list.
    /// </summary>
    /// <param name="qualLista">The list to populate with data.</param>
    private void FazerLeitura(ref ListaDupla<PalavraEDica> qualLista)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
      if (ofd.ShowDialog() != DialogResult.OK)
        return;

      qualLista = new ListaDupla<PalavraEDica>();
      using (StreamReader sr = new StreamReader(ofd.FileName))
      {
        string linha;
        while ((linha = sr.ReadLine()) != null)
        {
          if (linha.Length < 30)
            continue; // Ignore invalid lines
          string palavra = linha.Substring(0, 30).Trim();
          string dica = linha.Substring(30).Trim();
          var registro = new PalavraEDica(palavra, dica);
          qualLista.InserirEmOrdem(registro);
        }
      }
    }

    /// <summary>
    /// Handles the click event for the "Include" button.
    /// Adds a new word and hint to the list.
    /// </summary>
    private void btnIncluir_Click(object sender, EventArgs e)
    {
      string palavra = txtRA.Text.Trim();
      string dica = txtNome.Text.Trim();
      if (string.IsNullOrEmpty(palavra) || palavra.Length > 30)
      {
        MessageBox.Show("Enter a word (up to 30 characters).");
        return;
      }

      // Check if the word already exists before adding
      var duplicado = new PalavraEDica(palavra, "");
      if (lista1.Existe(duplicado))
      {
        MessageBox.Show("Cannot include. The word already exists!");
        return;
      }

      var registro = new PalavraEDica(palavra, dica);
      lista1.InserirEmOrdem(registro);
      ExibirDados(lista1, lsbDados, Direcao.paraFrente);
      ExibirRegistroAtual();
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
      if (lista1.EstaVazia)
        return;

      var atual = lista1.Atual.Info;
      var resp = MessageBox.Show($"Do you want to delete '{atual.Palavra}'?", "Confirmation", MessageBoxButtons.YesNo);
      if (resp == DialogResult.Yes)
      {
        lista1.Remover(atual);
        ExibirDados(lista1, lsbDados, Direcao.paraFrente);
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
      using (var sw = new StreamWriter(caminhoArquivoSelecionado, false))
      {
        foreach (var registro in lista1.Listagem(Direcao.paraFrente))
          sw.WriteLine(registro.ParaArquivo());
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
        }
      }
      lista1.PosicionarNoInicio();
      ExibirRegistroAtual();
      ExibirDados(lista1, lsbDados, Direcao.paraFrente);
    }

    private void btnInicio_Click(object sender, EventArgs e)
    {
      lista1.PosicionarNoInicio();
      ExibirRegistroAtual();
    }

    private void btnAnterior_Click(object sender, EventArgs e)
    {
      lista1.Retroceder();
      ExibirRegistroAtual();
    }

    private void btnProximo_Click(object sender, EventArgs e)
    {
      lista1.Avancar();
      ExibirRegistroAtual();
    }

    private void btnFim_Click(object sender, EventArgs e)
    {
      lista1.PosicionarNoFinal();
      ExibirRegistroAtual();
    }

    /// <summary>
    /// Displays the current record in the form fields.
    /// </summary>
    private void ExibirRegistroAtual()
    {
      if (lista1.EstaVazia || lista1.Atual == null)
      {
        txtRA.Text = "";
        txtNome.Text = "";
        toolStripStatusLabel1.Text = "0/0";
        return;
      }
      var atual = lista1.Atual.Info;
      txtRA.Text = atual.Palavra;
      txtNome.Text = atual.Dica;
      toolStripStatusLabel1.Text = $"Record: {lista1.NumeroDoNoAtual + 1}/{lista1.QuantosNos}";
    }

    private void btnEditar_Click(object sender, EventArgs e)
    {
      if (lista1.EstaVazia || lista1.Atual == null)
        return;

      var atual = lista1.Atual.Info;
      var resp = MessageBox.Show($"Deseja realmente editar '{atual.Palavra}'?", "Confirmação", MessageBoxButtons.YesNo);
      if (resp == DialogResult.Yes)
      {
        lista1.Atual.Info.Dica = txtNome.Text.Trim();
        ExibirDados(lista1, lsbDados, Direcao.paraFrente);
        ExibirRegistroAtual();
      }
    }

    private void btnSair_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
      ExibirRegistroAtual();
    }
  }
}
