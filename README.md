# 🧠 Projeto Jogo da Forca com Lista Duplamente Ligada em C# 🎮

## 🎯 Objetivo

Desenvolver um programa em **C#** utilizando **Windows Forms** e **listas duplamente ligadas** como estrutura de dados principal para armazenar e manipular palavras e dicas do Jogo da Forca.

---

## 🛠️ Funcionalidades

- 📂 Abrir um arquivo `.txt` com palavras e dicas via `OpenFileDialog`
- ➕ Incluir novas palavras na lista (ordenadamente, sem repetições)
- 🔍 Pesquisar palavras
- 🗑️ Excluir palavra atual
- ⬅️➡️ Navegar entre os elementos da lista (Anterior / Próximo)
- 📝 Listar todas as palavras
- 💾 Salvar automaticamente o conteúdo no mesmo arquivo ao encerrar

---

## 🧱 Estrutura de Classes

- `ListaDupla.cs` - implementação da estrutura de lista duplamente ligada
- `NoDuplo.cs` - nó da lista
- `FrmForca.cs` - formulário principal da aplicação
- `Program.cs` - ponto de entrada

---

## 📌 Requisitos e Comportamento

- O ponteiro `Atual` inicia no primeiro nó, se houver
- Ao navegar pelos botões, o ponteiro é movido conforme métodos `MoverProximo()` e `MoverAnterior()`
- O arquivo carregado deve ser gravado com os dados atualizados ao finalizar o programa

---

## ✅ Boas Práticas Adotadas

- 🧾 Identificadores com **camelCase** (métodos, variáveis) e **PascalCase** (classes)
- 🗃️ Separação clara entre lógica de interface e regras de negócio
- 🧹 Código limpo, documentado e reutilizável
- ✅ Verificações de nulidade e mensagens amigáveis de erro
- 🔐 Tratamento de exceções em operações de arquivo

---

## 🧪 Métodos Esperados na Classe `ListaDupla`

```csharp
InserirOrdenado(string palavra, string dica)
ExcluirAtual()
Pesquisar(string palavra)
MoverProximo()
MoverAnterior()
ListarTodos()
SalvarEmArquivo(string caminho)
```
🧼 Regras para Submissão
📦 Enviar apenas arquivos essenciais (.cs, .sln, .resx etc.)

❌ Não incluir as pastas bin/, obj/, .vs/

🗂️ Nome do ZIP: RA1_RA2_Projeto1ED.zip

⚠️ Observação Importante
🔴 Este projeto não deve conter nenhuma referência a "alunos" ou funcionalidades de cadastro de alunos.

✔️ O foco deve ser exclusivamente no Jogo da Forca!

🧠 Créditos
Desenvolvido como parte da disciplina de Estrutura de Dados - 2025
👨‍💻 Aluno: Gabriel Oliveira
📅 Data: 04/05/2025
