# 💸 Antecipação de Recebíveis
## Sistema completo (frontend + backend) para simular antecipação de recebíveis com base em NFes e limite de crédito das empresas.

### 🧠 Funcionalidades

- Cadastro de empresas com limite de crédito
- Cadastro de notas fiscais
- Adição de NFes no carrinho de antecipação
- Cálculo bruto, deságio e valor líquido
- Restrições baseadas no limite de crédito da empresa
- API RESTful com .NET 6
- Frontend em React + TypeScript + Vite

### 🧰 Tecnologias e versões utilizadas

| Tecnologia     | Versão |
|----------------|--------|
| .NET SDK       | 8.0    |
| React          | 19     |

> Recomenda-se utilizar as versões listadas para garantir compatibilidade e bom funcionamento do projeto.

### 📁 Estrutura de pastas

```
├── antecipacao-recebiveis-backend
│   ├── AntecipacaoRecebiveis.Api          # API principal
│   ├── AntecipacaoRecebiveis.Application  # Regras de negocio (Services, DTOs, Interfaces)
│   ├── AntecipacaoRecebiveis.Domain       # Entidades e enums
│   ├── AntecipacaoRecebiveis.Infrastructure # Repositorios e contexto de dados
│   └── AntecipacaoRecebiveis.Tests        # Testes com xUnit e Moq
└── antecipacao-recebiveis-frontend        # Frontend com React + Vite
```
## Clonando o projeto

Para clonar este projeto em sua máquina local, siga os passos abaixo:

1. Abra o terminal ou prompt de comando.
2. Execute o comando abaixo para clonar o repositório:

```bash
git clone https://github.com/luizolivas/antecipacao-recebiveis.git
```

### 📁 Como rodar o projeto localmente

#Backend (.NET)
```bash
cd antecipacao-recebiveis-backend/AntecipacaoRecebiveis.Api
dotnet run
```

#Frontend (React)
```bash
cd antecipacao-recebiveis-frontend
npm install
npm run dev
```

Altere a baseURL no src/services/api.ts se a porta do backend for diferente.

## Configuração e Execução

- O banco de dados utilizado é **em memória**, adequado para testes e desenvolvimento. Não armazena dados após o encerramento da aplicação.
- A API está configurada para permitir CORS de todas as origens (`AllowAll`), o que facilita testes locais, mas deve ser ajustado para produção.

## Documentação da API

A API possui documentação automática via **Swagger**, que pode ser acessada em:

```
https://localhost:{port}/swagger/v1/swagger.json
```
### ✅ Testes

Testes unitarios com xUnit e Moq localizados na pasta AntecipacaoRecebiveis.Tests
