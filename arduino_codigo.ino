// Definição de pinos
const int LED_VERDE = 13;     
const int LED_VERMELHO = 12; 
const int LED_ERRO_1 = 2;
const int LED_ERRO_2 = 3;     
const int LED_ERRO_3 = 4;     
const int LED_ERRO_4 = 5;     
const int LED_ERRO_5 = 6;
const int LED_ERRO_6 = 7;
const int LED_ERRO_7 = 8;
const int LED_ERRO_8 = 9;

// Variáveis globais
char comando = '\0';
bool jogoAtivo = false;
unsigned long tempoAnterior = 0;
const long intervalo = 500;   // Intervalo de piscada em milissegundos
int estadoLED = LOW;

void setup() {
  // Inicializa comunicação serial
  Serial.begin(9600);
  
  // Configura os pinos como saída
  pinMode(LED_VERDE, OUTPUT);
  pinMode(LED_VERMELHO, OUTPUT);
  pinMode(LED_ERRO_1, OUTPUT);
  pinMode(LED_ERRO_2, OUTPUT);
  pinMode(LED_ERRO_3, OUTPUT);
  pinMode(LED_ERRO_4, OUTPUT);
  pinMode(LED_ERRO_5, OUTPUT);
  pinMode(LED_ERRO_6, OUTPUT);
  pinMode(LED_ERRO_7, OUTPUT);
  pinMode(LED_ERRO_8, OUTPUT);
  
  // Inicializa todos os LEDs como desligados
  resetarLEDs();
}

void loop() {
  // Verifica se há dados disponíveis na porta serial
  if (Serial.available() > 0) {
    // Lê o caractere recebido
    comando = Serial.read();
    
    // Processa o comando recebido
    processarComando(comando);
  }
  
  // Verifica se precisa piscar LEDs em caso de vitória ou derrota
  if (comando == 'V' || comando == 'P') {
    unsigned long tempoAtual = millis();
    
    // Verifica se passou tempo suficiente para alternar estado do LED
    if (tempoAtual - tempoAnterior >= intervalo) {
      tempoAnterior = tempoAtual;
      
      // Alterna o estado do LED
      if (estadoLED == LOW) {
        estadoLED = HIGH;
      } else {
        estadoLED = LOW;
      }
      
      // Acende o LED correspondente baseado no comando
      if (comando == 'V') {
        digitalWrite(LED_VERDE, estadoLED);
      } else if (comando == 'P') {
        digitalWrite(LED_VERMELHO, estadoLED);
      }
    }
  }
}

// Processa o comando recebido pela porta serial
void processarComando(char cmd) {
  switch (cmd) {
    case 'I':  // Iniciar jogo
      resetarLEDs();
      digitalWrite(LED_VERDE, HIGH);
      jogoAtivo = true;
      break;
      
    case '1':  // Erro 1
      if (jogoAtivo) {
        digitalWrite(LED_ERRO_1, HIGH);
      }
      break;
      
    case '2':  // Erro 2
      if (jogoAtivo) {
        digitalWrite(LED_ERRO_1, HIGH);
        digitalWrite(LED_ERRO_2, HIGH);
      }
      break;
      
    case '3':  // Erro 3
      if (jogoAtivo) {
        digitalWrite(LED_ERRO_1, HIGH);
        digitalWrite(LED_ERRO_2, HIGH);
        digitalWrite(LED_ERRO_3, HIGH);
      }
      break;
      
    case '4':  // Erro 4
      if (jogoAtivo) {
        digitalWrite(LED_ERRO_1, HIGH);
        digitalWrite(LED_ERRO_2, HIGH);
        digitalWrite(LED_ERRO_3, HIGH);
        digitalWrite(LED_ERRO_4, HIGH);
      }
      break;
      
    case '5':  // Erro 5
      if (jogoAtivo) {
        digitalWrite(LED_ERRO_1, HIGH);
        digitalWrite(LED_ERRO_2, HIGH);
        digitalWrite(LED_ERRO_3, HIGH);
        digitalWrite(LED_ERRO_4, HIGH);
        digitalWrite(LED_ERRO_5, HIGH);
      }
      break;
      
    case '6':  // Erro 6
      if (jogoAtivo) {
        digitalWrite(LED_ERRO_1, HIGH);
        digitalWrite(LED_ERRO_2, HIGH);
        digitalWrite(LED_ERRO_3, HIGH);
        digitalWrite(LED_ERRO_4, HIGH);
        digitalWrite(LED_ERRO_5, HIGH);
        digitalWrite(LED_ERRO_6, HIGH);
      }
      break;
      
    case '7':  // Erro 7
      if (jogoAtivo) {
        digitalWrite(LED_ERRO_1, HIGH);
        digitalWrite(LED_ERRO_2, HIGH);
        digitalWrite(LED_ERRO_3, HIGH);
        digitalWrite(LED_ERRO_4, HIGH);
        digitalWrite(LED_ERRO_5, HIGH);
        digitalWrite(LED_ERRO_6, HIGH);
        digitalWrite(LED_ERRO_7, HIGH);
      }
      break;
      
    case '8':  // Erro 8
      if (jogoAtivo) {
        digitalWrite(LED_ERRO_1, HIGH);
        digitalWrite(LED_ERRO_2, HIGH);
        digitalWrite(LED_ERRO_3, HIGH);
        digitalWrite(LED_ERRO_4, HIGH);
        digitalWrite(LED_ERRO_5, HIGH);
        digitalWrite(LED_ERRO_6, HIGH);
        digitalWrite(LED_ERRO_7, HIGH);
        digitalWrite(LED_ERRO_8, HIGH);
      }
      break;
      
    case 'V':  // Vitória
      jogoAtivo = false;
      resetarLEDs();
      tempoAnterior = millis(); // Inicia o temporizador para piscar
      break;
      
    case 'P':  // Derrota
      jogoAtivo = false;
      resetarLEDs();
      tempoAnterior = millis(); // Inicia o temporizador para piscar
      break;
  }
}

// Reseta todos os LEDs de erro para o estado LOW
void resetarLEDs() {
  digitalWrite(LED_VERDE, LOW);
  digitalWrite(LED_VERMELHO, LOW);
  digitalWrite(LED_ERRO_1, LOW);
  digitalWrite(LED_ERRO_2, LOW);
  digitalWrite(LED_ERRO_3, LOW);
  digitalWrite(LED_ERRO_4, LOW);
  digitalWrite(LED_ERRO_5, LOW);
  digitalWrite(LED_ERRO_6, LOW);
  digitalWrite(LED_ERRO_7, LOW);
  digitalWrite(LED_ERRO_8, LOW);
}