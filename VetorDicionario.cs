using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apListaLigada
{
    internal class VetorDicionario
    {
        // Atributos
        private Dicionario[] dados;
        private int qtosDados;
        private int posicaoAtual;

        // Propriedades
        public int QtosDados
        {
            get { return qtosDados; }
        }

        public int PosicaoAtual
        {
            get { return posicaoAtual; }
            set
            {
                if (value >= 0 && value < qtosDados)
                    posicaoAtual = value;
            }
        }

        // Construtor
        public VetorDicionario(int capacidade = 100)
        {
            dados = new Dicionario[capacidade];
            qtosDados = 0;
            posicaoAtual = 0;
        }

        // Indexador para acessar Dicionario por índice
        public Dicionario this[int indice]
        {
            get
            {
                if (indice >= 0 && indice < qtosDados)
                    return dados[indice];
                return null;
            }
        }

        // Adiciona um novo Dicionario ao vetor
        public void Adicionar(Dicionario d)
        {
            if (qtosDados < dados.Length)
            {
                dados[qtosDados] = d;
                qtosDados++;
            }
            // else: poderia lançar exceção ou aumentar capacidade
        }

        // Remove Dicionario na posição informada
        public void Remover(int indice)
        {
            if (indice >= 0 && indice < qtosDados)
            {
                for (int i = indice; i < qtosDados - 1; i++)
                    dados[i] = dados[i + 1];
                dados[qtosDados - 1] = null;
                qtosDados--;
                if (posicaoAtual >= qtosDados)
                    posicaoAtual = qtosDados - 1;
            }
        }

        // Limpa o vetor
        public void Limpar()
        {
            for (int i = 0; i < qtosDados; i++)
                dados[i] = null;
            qtosDados = 0;
            posicaoAtual = 0;
        }

        // Carrega dados do arquivo
        public void CarregarArquivo(string caminho)
        {
            Limpar();
            using (StreamReader sr = new StreamReader(caminho, Encoding.UTF8))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    Dicionario d = new Dicionario();
                    d.LerLinha(linha);
                    Adicionar(d);
                }
            }
        }

        // Salva dados no arquivo
        public void SalvarArquivo(string caminho)
        {
            using (StreamWriter sw = new StreamWriter(caminho, false, Encoding.UTF8))
            {
                for (int i = 0; i < qtosDados; i++)
                {
                    sw.WriteLine(dados[i].ParaLinhaArquivo());
                }
            }
        }

        // Retorna uma lista de tuplas (posição, palavra, dica) para exibição em DataGridView
        public List<(int posicao, string palavra, string dica)> ListarDados()
        {
            var lista = new List<(int, string, string)>();
            for (int i = 0; i < qtosDados; i++)
            {
                if (dados[i] != null)
                    lista.Add((i, dados[i].Palavra, dados[i].Dica));
            }
            return lista;
        }

        // Pesquisa binária: retorna índice se existir, -1 se não existir
        public int Existe(string palavra)
        {
            // Converta a palavra buscada para maiúscula
            string buscaUpper = palavra.ToUpper();

            // Exemplo de pesquisa simples (linear)
            for (int i = 0; i < qtosDados; i++)
            {
                if (dados[i] != null &&
                    dados[i].Palavra != null &&
                    dados[i].Palavra.ToUpper() == buscaUpper)
                    return i;
            }

            return -1;
        }

        // Insere em ordem alfabética pelo campo Palavra
        public int InserirEmOrdem(Dicionario novo)
        {
            int i = 0;
            while (i < qtosDados && string.Compare(dados[i].Palavra, novo.Palavra, StringComparison.CurrentCultureIgnoreCase) < 0)
                i++;

            if (qtosDados < dados.Length)
            {
                for (int j = qtosDados; j > i; j--)
                    dados[j] = dados[j - 1];

                dados[i] = novo;
                qtosDados++;
                return i;
            }
            return -1;
        }

        // Add the missing GetAtual method to the VetorDicionario class.  
        public (string palavra, string dica) GetAtual()
        {
            if (posicaoAtual < 0 || posicaoAtual >= qtosDados)
                throw new InvalidOperationException("Posição atual inválida.");

            var dicionarioAtual = dados[posicaoAtual];
            return (dicionarioAtual.Palavra, dicionarioAtual.Dica);
        }

        /// <summary>
        /// Exclui um Dicionario na posição informada (alias para o método Remover)
        /// </summary>
        /// <param name="posicaoAtual">A posição do item a ser excluído</param>
        internal void ExcluirNaPosicao(int posicaoAtual)
        {
            // Chama o método Remover já existente
            Remover(posicaoAtual);
        }

        internal void AlterarDica(int posicaoAtual, string dica)
        {
            // Verificar se a posição é válida
            if (posicaoAtual < 0 || posicaoAtual >= qtosDados)
                throw new ArgumentOutOfRangeException(nameof(posicaoAtual), "Posição inválida para alteração de dica.");

            // Verificar se há dados na posição indicada
            if (dados[posicaoAtual] == null)
                throw new InvalidOperationException("Não há registro na posição informada.");

            // Atualizar a dica do objeto Dicionario na posição especificada
            dados[posicaoAtual].Dica = dica;
        }
    }
}
