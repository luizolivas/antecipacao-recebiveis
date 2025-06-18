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

-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

## ⚙️ Decisões tomadas

- **Separação em camadas (API, Application, Domain, Infrastructure):**  
  Adotei um padrão DDD simplificado para garantir uma organização clara e coerente entre regras de negócio, entidades, persistência de dados e exposição via API.  
- **Banco de dados em memória (InMemory):**  
  Escolha ideal para facilitar testes e demonstrações, eliminando a necessidade de configuração complexa de banco relacional durante o desenvolvimento inicial.  
- **CORS liberado (AllowAll):**  
  Configuração que simplificou a comunicação entre frontend e backend em ambiente local, evitando problemas de política de segurança. Reforço que essa configuração não é recomendada para ambientes de produção.  
- **Testes unitários com xUnit e Moq:**  
  Implementação de testes para validar as regras de negócio de forma isolada, promovendo qualidade e confiança no código com boa performance.

---

## 🏗 Arquitetura adotada

O projeto está estruturado em camadas bem definidas, favorecendo a manutenção, extensibilidade e testabilidade:

- **Domain Layer:** contém as entidades, enums e regras fundamentais do negócio.  
- **Application Layer:** responsável pelas regras de negócio específicas, serviços, DTOs e interfaces.  
- **Infrastructure Layer:** implementação dos repositórios, contexto de dados e integração com a persistência.  
- **API Layer:** camada de apresentação, que expõe a funcionalidade via API RESTful para o frontend e clientes.

---

## 📐 Aplicação dos princípios SOLID

Busquei aplicar os princípios SOLID para tornar o código mais limpo, modular e fácil de evoluir:

- **SRP (Single Responsibility Principle):** responsabilidades separadas em classes e camadas específicas.  
- **OCP (Open/Closed Principle) e DIP (Dependency Inversion Principle):** uso de interfaces e injeção de dependências para permitir extensões sem modificações diretas no código existente.  
- **ISP (Interface Segregation Principle):** priorização de interfaces pequenas e específicas, evitando acoplamento desnecessário.

Reconheço que sempre há espaço para melhorias e estou aberto a evoluir a arquitetura conforme o projeto avance.

---

## 🚀 Melhorias futuras sugeridas
 
- Criar pipeline de CI/CD com GitHub Actions para builds, testes automatizados e deploy.  
- Adicionar autenticação e autorização, por exemplo, com JWT, para controlar acesso.  
- Substituir o banco em memória por um banco relacional como PostgreSQL ou SQL Server.  
- Integrar análise estática de código, por exemplo, usando SonarQube.  
- Automatizar o cálculo do faturamento mensal das empresas com base nos valores das notas fiscais, evitando entrada manual.
