using System;
using System.IO.Ports;

namespace apListaLigada
{
    /// <summary>
    /// Classe para gerenciar a comunica��o serial com o Arduino
    /// </summary>
    public class ConexaoSerial
    {
        private SerialPort porta;
        private bool estaConectado = false;

        /// <summary>
        /// Indica se a conex�o serial est� atualmente estabelecida
        /// </summary>
        public bool EstaConectado => estaConectado;

        /// <summary>
        /// Nome da porta COM atualmente em uso
        /// </summary>
        public string PortaNome => porta?.PortName ?? "Nenhuma";

        /// <summary>
        /// Construtor padr�o
        /// </summary>
        public ConexaoSerial()
        {
            porta = new SerialPort();
            porta.BaudRate = 9600;
            porta.DataBits = 8;
            porta.Parity = Parity.None;
            porta.StopBits = StopBits.One;
        }

        /// <summary>
        /// Abre a conex�o com a porta serial especificada
        /// </summary>
        /// <param name="portaNome">Nome da porta COM (ex: "COM3")</param>
        /// <returns>True se a conex�o foi estabelecida com sucesso</returns>
        public bool AbrirConexao(string portaNome)
        {
            try
            {
                if (estaConectado)
                    FecharConexao();

                porta.PortName = portaNome;
                porta.Open();
                estaConectado = porta.IsOpen;
                return estaConectado;
            }
            catch (Exception)
            {
                estaConectado = false;
                return false;
            }
        }

        /// <summary>
        /// Fecha a conex�o com a porta serial
        /// </summary>
        public void FecharConexao()
        {
            try
            {
                if (porta != null && porta.IsOpen)
                {
                    porta.Close();
                }
                estaConectado = false;
            }
            catch (Exception)
            {
                // Ignora exce��es ao fechar a porta
            }
        }

        /// <summary>
        /// Envia um caractere para o Arduino
        /// </summary>
        /// <param name="c">Caractere a ser enviado</param>
        /// <returns>True se o envio foi bem-sucedido</returns>
        public bool EnviarCaractere(char c)
        {
            if (!estaConectado)
                return false;

            try
            {
                porta.Write(new[] { c }, 0, 1);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Envia caractere indicando in�cio do jogo
        /// </summary>
        public bool EnviarInicioJogo()
        {
            return EnviarCaractere('I');
        }

        /// <summary>
        /// Envia caractere indicando fim de jogo por derrota
        /// </summary>
        public bool EnviarDerrota()
        {
            return EnviarCaractere('P');
        }

        /// <summary>
        /// Envia caractere indicando fim de jogo por vit�ria
        /// </summary>
        public bool EnviarVitoria()
        {
            return EnviarCaractere('V');
        }

        /// <summary>
        /// Envia n�mero de erros para o Arduino
        /// </summary>
        /// <param name="numErros">N�mero de erros (1-8)</param>
        public bool EnviarErros(int numErros)
        {
            if (numErros >= 1 && numErros <= 8)
                return EnviarCaractere(numErros.ToString()[0]);
            
            return false;
        }

        /// <summary>
        /// Obt�m lista de portas seriais dispon�veis no sistema
        /// </summary>
        /// <returns>Array com nomes das portas dispon�veis</returns>
        public static string[] ObterPortasDisponiveis()
        {
            return SerialPort.GetPortNames();
        }
    }
}
