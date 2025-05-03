public interface IRegistro
{
  string Palavra { get; set; }
  string Dica { get; set; }
  string FormatoDeArquivo();
  string ToString();
}

