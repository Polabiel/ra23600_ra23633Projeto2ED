public interface ICriterioDeSeparacao<Dado>
{
  bool DeveSeparar();
  bool Igual(Dado a, Dado b);
  int Comparar(Dado a, Dado b);
}

