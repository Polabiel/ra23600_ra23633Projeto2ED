// ra23600 ra23305
// Gabriel - Andrew
using System;
namespace apListaLigada
{
    internal class PalavraEDica : IComparable<PalavraEDica>, IRegistro
    {
        public string Palavra { get; set; }
        public string Dica { get; set; }

        public PalavraEDica(string palavra, string dica)
        {
            Palavra = palavra;
            Dica = dica;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared.
        /// </returns>
        public int CompareTo(PalavraEDica other)
        {
            return string.Compare(Palavra, other?.Palavra, StringComparison.OrdinalIgnoreCase);
        }

        public string ParaArquivo()
        {
            return $"{Palavra.PadRight(30)}{Dica}";
        }

        // Implementação do método exigido pela interface IRegistro
        public string FormatoDeArquivo()
        {
            return ParaArquivo();
        }

        // Implementação do método ToString() para exibição no ListBox
        public override string ToString()
        {
            return $"{Palavra} - {Dica}";
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// True if the specified object is equal to the current object; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is PalavraEDica other)
            {
                return string.Equals(Palavra, other.Palavra, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(Dica, other.Dica, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked // Allow overflow for hash code calculation
            {
                int hash = 17;
                hash = hash * 23 + (Palavra?.ToLowerInvariant().GetHashCode() ?? 0);
                hash = hash * 23 + (Dica?.ToLowerInvariant().GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
