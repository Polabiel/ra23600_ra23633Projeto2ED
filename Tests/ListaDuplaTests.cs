using apListaLigada;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
internal class ListaDuplaTestItem : IComparable<ListaDuplaTestItem>, IRegistro
{
    public string Palavra { get; set; }
    public string Dica { get; set; }

    public ListaDuplaTestItem(string palavra, string dica)
    {
        Palavra = palavra;
        Dica = dica;
    }

    public int CompareTo(ListaDuplaTestItem other)
    {
        return string.Compare(Palavra, other.Palavra, StringComparison.Ordinal);
    }

    public string FormatoDeArquivo() => $"{Palavra.PadRight(30)}{Dica}";
    public override string ToString() => $"{Palavra} - {Dica}";
}

[TestClass]
public class ListaDuplaTests
{
    [TestMethod]
    public void TestaListaVazia()
    {
        var lista = new ListaDupla<ListaDuplaTestItem>();
        Assert.IsTrue(lista.EstaVazia);
        Assert.AreEqual(0, lista.QuantosNos);
    }

    [TestMethod]
    public void TestaInserirEmOrdem()
    {
        var lista = new ListaDupla<ListaDuplaTestItem>();
        var p1 = new ListaDuplaTestItem("Banana", "Fruta amarela");
        var p2 = new ListaDuplaTestItem("Abacate", "Fruta verde");
        var p3 = new ListaDuplaTestItem("Caju", "Fruta do nordeste");

        lista.InserirEmOrdem(p1);
        lista.InserirEmOrdem(p2);
        lista.InserirEmOrdem(p3);

        Assert.AreEqual(3, lista.QuantosNos);
        Assert.AreEqual("Abacate", lista[0].Palavra);
        Assert.AreEqual("Banana", lista[1].Palavra);
        Assert.AreEqual("Caju", lista[2].Palavra);
    }

    [TestMethod]
    public void TestaExiste()
    {
        var lista = new ListaDupla<ListaDuplaTestItem>();
        var p1 = new ListaDuplaTestItem("Banana", "Fruta amarela");
        lista.InserirEmOrdem(p1);

        Assert.IsTrue(lista.Existe(new ListaDuplaTestItem("Banana", "")));
        Assert.IsFalse(lista.Existe(new ListaDuplaTestItem("Maçã", "")));
    }

    [TestMethod]
    public void TestaRemover()
    {
        var lista = new ListaDupla<ListaDuplaTestItem>();
        var p1 = new ListaDuplaTestItem("Banana", "Fruta amarela");
        var p2 = new ListaDuplaTestItem("Abacate", "Fruta verde");
        lista.InserirEmOrdem(p1);
        lista.InserirEmOrdem(p2);

        Assert.IsTrue(lista.Remover(p1));
        Assert.AreEqual(1, lista.QuantosNos);
        Assert.IsFalse(lista.Existe(p1));
        Assert.IsTrue(lista.Existe(p2));
    }

    [TestMethod]
    public void TestaIndexador()
    {
        var lista = new ListaDupla<ListaDuplaTestItem>();
        var p1 = new ListaDuplaTestItem("Banana", "Fruta amarela");
        var p2 = new ListaDuplaTestItem("Abacate", "Fruta verde");
        lista.InserirEmOrdem(p1);
        lista.InserirEmOrdem(p2);

        Assert.AreEqual("Abacate", lista[0].Palavra);
        lista[0] = new ListaDuplaTestItem("Ameixa", "Fruta roxa");
        Assert.AreEqual("Ameixa", lista[0].Palavra);
    }

    [TestMethod]
    public void TestaInserirAposFim()
    {
        var lista = new ListaDupla<ListaDuplaTestItem>();
        var p1 = new ListaDuplaTestItem("Banana", "Fruta amarela");
        var p2 = new ListaDuplaTestItem("Caju", "Fruta do nordeste");
        lista.InserirAposFim(p1);
        lista.InserirAposFim(p2);

        Assert.AreEqual(2, lista.QuantosNos);
        Assert.AreEqual("Banana", lista[0].Palavra);
        Assert.AreEqual("Caju", lista[1].Palavra);
    }

    [TestMethod]
    public void TestaListagemParaFrenteEParaTras()
    {
        var lista = new ListaDupla<ListaDuplaTestItem>();
        var p1 = new ListaDuplaTestItem("Banana", "Fruta amarela");
        var p2 = new ListaDuplaTestItem("Caju", "Fruta do nordeste");
        lista.InserirAposFim(p1);
        lista.InserirAposFim(p2);

        var frente = lista.Listagem(Direcao.paraFrente);
        var tras = lista.Listagem(Direcao.paraTras);

        Assert.AreEqual("Banana", frente[0].Palavra);
        Assert.AreEqual("Caju", frente[1].Palavra);
        Assert.AreEqual("Caju", tras[0].Palavra);
        Assert.AreEqual("Banana", tras[1].Palavra);
    }
}
