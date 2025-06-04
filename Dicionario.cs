using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apListaLigada
{
    internal class Dicionario
    {
        // Constantes para mapeamento dos campos no arquivo
        public const int TAM_PALAVRA = 15;
        public const int TAM_DICA = 30;
        public const int INICIO_PALAVRA = 0;
        public const int INICIO_DICA = INICIO_PALAVRA + TAM_PALAVRA;

        // Atributos privados
        private string palavra;
        private string dica;
        private bool[] acertou = new bool[TAM_PALAVRA];
        private int id; // Novo atributo para o ID (índice da linha)

        // Propriedades de acesso
        public string Palavra
        {
            get { return palavra; }
            set { palavra = value; }
        }

        public string Dica
        {
            get { return dica; }
            set { dica = value; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public Dicionario()
        {
            for (int i = 0; i < acertou.Length; i++)
                acertou[i] = false;
            id = -1;
        }

        public void LerLinha(string linha)
        {
           
            string palavraLida = "";
            string dicaLida = "";

            if (!string.IsNullOrEmpty(linha))
            {
                if (linha.Length >= INICIO_PALAVRA + TAM_PALAVRA)
                    palavraLida = linha.Substring(INICIO_PALAVRA, TAM_PALAVRA).Trim();
                else if (linha.Length > INICIO_PALAVRA)
                    palavraLida = linha.Substring(INICIO_PALAVRA).Trim();

                if (linha.Length >= INICIO_DICA + TAM_DICA)
                    dicaLida = linha.Substring(INICIO_DICA, TAM_DICA).Trim();
                else if (linha.Length > INICIO_DICA)
                    dicaLida = linha.Substring(INICIO_DICA).Trim();
            }

            palavra = palavraLida;
            dica = dicaLida;

           
            for (int i = 0; i < acertou.Length; i++)
                acertou[i] = false;

            
        }

      
        public string ParaLinhaArquivo()
        {
            return palavra.PadRight(TAM_PALAVRA) + dica.PadRight(TAM_DICA);
        }

        public bool TentarLetra(char letra)
        {
            bool encontrou = false;
            for (int i = 0; i < palavra.Length && i < TAM_PALAVRA; i++)
            {
                if (char.ToUpperInvariant(palavra[i]) == char.ToUpperInvariant(letra))
                {
                    acertou[i] = true;
                    encontrou = true;
                }
            }
            return encontrou;
        }

        public bool[] GetAcertou()
        {
            return acertou;
        }
    }
}
