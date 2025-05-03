using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace apListaLigada
{
    public enum Direcao { paraFrente, paraTras };

    public class ListaDupla<Dado> where Dado : IComparable<Dado>, IRegistro
    {
        private NoDuplo<Dado> primeiro, ultimo, atual, anterior;
        private int quantosNos;
        private int numeroDoNoAtual;
        private bool primeiroAcessoDoPercurso;

        public ListaDupla()
        {
            primeiro = ultimo = atual = null;
            quantosNos = numeroDoNoAtual = 0;
            primeiroAcessoDoPercurso = false;
        }

        public bool EstaVazia => primeiro == null;

        public int QuantosNos => quantosNos;

        public NoDuplo<Dado> Atual => atual;

        public int NumeroDoNoAtual { get => numeroDoNoAtual; set => numeroDoNoAtual = value; }

        public void PosicionarNoInicio()
        {
            atual = primeiro;
            numeroDoNoAtual = 0;
        }

        public void PosicionarNoFinal()
        {
            atual = ultimo;
            numeroDoNoAtual = quantosNos - 1;
        }

        public void Avancar()
        {
            if (atual != null && atual.Prox != null)
            {
                atual = atual.Prox;
                numeroDoNoAtual++;
            }
        }

        public void Retroceder()
        {
            if (atual != null && atual.Ant != null)
            {
                atual = atual.Ant;
                numeroDoNoAtual--;
            }
        }

        public void PosicionarEm(int indice)
        {
            if (indice >= 0 && indice < quantosNos)
            {
                atual = primeiro;
                numeroDoNoAtual = 0;
                for (int i = 0; i < indice; i++)
                {
                    atual = atual.Prox;
                    numeroDoNoAtual++;
                }
            }
        }

        public Dado this[int indice]
        {
            get
            {
                PosicionarEm(indice);
                return atual.Info;
            }
            set
            {
                PosicionarEm(indice);
                atual.Info = value;
            }
        }

        public bool InserirEmOrdem(Dado dados)
        {
            var novoNo = new NoDuplo<Dado>(dados);

            if (EstaVazia)
            {
                primeiro = ultimo = novoNo;
            }
            else if (dados.CompareTo(primeiro.Info) < 0)
            {
                novoNo.Prox = primeiro;
                primeiro.Ant = novoNo;
                primeiro = novoNo;
            }
            else if (dados.CompareTo(ultimo.Info) > 0)
            {
                ultimo.Prox = novoNo;
                novoNo.Ant = ultimo;
                ultimo = novoNo;
            }
            else
            {
                var atual = primeiro;
                while (atual != null && dados.CompareTo(atual.Info) > 0)
                {
                    atual = atual.Prox;
                }

                if (atual != null)
                {
                    novoNo.Prox = atual;
                    novoNo.Ant = atual.Ant;
                    atual.Ant.Prox = novoNo;
                    atual.Ant = novoNo;
                }
            }

            quantosNos++;
            return true;
        }

        public bool Existe(Dado dados)
        {
            var atual = primeiro;
            while (atual != null)
            {
                if (atual.Info.CompareTo(dados) == 0)
                {
                    this.atual = atual;
                    return true;
                }
                atual = atual.Prox;
            }
            return false;
        }

        public bool Remover(Dado dados)
        {
            if (EstaVazia)
                return false;

            var atual = primeiro;
            while (atual != null)
            {
                if (atual.Info.CompareTo(dados) == 0)
                {
                    if (atual == primeiro)
                    {
                        primeiro = atual.Prox;
                        if (primeiro != null)
                            primeiro.Ant = null;
                    }
                    else if (atual == ultimo)
                    {
                        ultimo = atual.Ant;
                        if (ultimo != null)
                            ultimo.Prox = null;
                    }
                    else
                    {
                        atual.Ant.Prox = atual.Prox;
                        atual.Prox.Ant = atual.Ant;
                    }

                    quantosNos--;
                    return true;
                }
                atual = atual.Prox;
            }
            return false;
        }

        public void InserirAposFim(Dado dados)
        {
            var novoNo = new NoDuplo<Dado>(dados);

            if (EstaVazia)
            {
                primeiro = ultimo = novoNo;
            }
            else
            {
                ultimo.Prox = novoNo;
                novoNo.Ant = ultimo;
                ultimo = novoNo;
            }

            quantosNos++;
        }

        public List<Dado> Listagem(Direcao qualDirecao)
        {
            var lista = new List<Dado>();
            var atual = qualDirecao == Direcao.paraFrente ? primeiro : ultimo;

            while (atual != null)
            {
                lista.Add(atual.Info);
                atual = qualDirecao == Direcao.paraFrente ? atual.Prox : atual.Ant;
            }

            return lista;
        }

        public void GravarDados(string nomeArq)
        {
            var arquivo = new StreamWriter(nomeArq);
            atual = primeiro;
            while (atual != null)
            {
                arquivo.WriteLine(atual.Info.FormatoDeArquivo());
                atual = atual.Prox;
            }
            arquivo.Close();
        }
    }
}