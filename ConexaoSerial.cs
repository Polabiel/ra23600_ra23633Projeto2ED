using System;
using System.IO.Ports;

namespace apListaLigada
{
    /// <summary>
    /// Classe para gerenciar a comunicação serial com o Arduino
    /// </summary>
    public class ConexaoSerial
    {
        private SerialPort porta;
        private bool estaConectado = false;

        /// <summary>
        /// Indica se a conexão serial está atualmente estabelecida
        /// </summary>
        public bool EstaConectado => estaConectado;

        /// <summary>
        /// Nome da porta COM atualmente em uso
        /// </summary>
        public string PortaNome => porta?.PortName ?? "Nenhuma";

        /// <summary>
        /// Construtor padrão
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
        /// Abre a conexão com a porta serial especificada
        /// </summary>
        /// <param name="portaNome">Nome da porta COM (ex: "COM3")</param>
        /// <returns>True se a conexão foi estabelecida com sucesso</returns>
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
        /// Fecha a conexão com a porta serial
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
                // Ignora exceções ao fechar a porta
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
        /// Envia caractere indicando início do jogo
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
        /// Envia caractere indicando fim de jogo por vitória
        /// </summary>
        public bool EnviarVitoria()
        {
            return EnviarCaractere('V');
        }

        /// <summary>
        /// Envia número de erros para o Arduino
        /// </summary>
        /// <param name="numErros">Número de erros (1-8)</param>
        public bool EnviarErros(int numErros)
        {
            if (numErros >= 1 && numErros <= 8)
                return EnviarCaractere(numErros.ToString()[0]);
            
            return false;
        }

        /// <summary>
        /// Obtém lista de portas seriais disponíveis no sistema
        /// </summary>
        /// <returns>Array com nomes das portas disponíveis</returns>
        public static string[] ObterPortasDisponiveis()
        {
            return SerialPort.GetPortNames();
        }
    }
}
