# Todo API

API RESTful desenvolvida em .NET 8 para gerenciamento de tarefas.

## Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- MySQL + Pomelo
- AutoMapper
- Swagger / OpenAPI
- xUnit + Moq (testes unitários)

## Padrões de Projeto

- Repository Pattern
- Dependency Injection
- DTO Pattern com AutoMapper

## Pré-requisitos

- .NET 8 SDK
- MySQL 8+

## Como rodar

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/todo-api.git
cd todo-api
```

### 2. Configure a string de conexão

Crie o arquivo `TodoAPI/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TodoDB;User Id=root;Password=sua-senha;"
  }
}
```

### 3. Execute as migrations

```bash
dotnet ef database update --project TodoAPI
```

### 4. Rode a aplicação

```bash
dotnet run --project TodoAPI
```

### 5. Acesse o Swagger

```
http://localhost:5190
```

## Endpoints

| Método | Rota                | Descrição                   |
| ------ | ------------------- | --------------------------- |
| GET    | /api/tarefas        | Lista todas as tarefas      |
| GET    | /api/tarefas/{id}   | Busca tarefa por ID         |
| GET    | /api/tarefas/filtro | Filtra por status e/ou data |
| POST   | /api/tarefas        | Cria nova tarefa            |
| PUT    | /api/tarefas/{id}   | Atualiza tarefa             |
| DELETE | /api/tarefas/{id}   | Remove tarefa               |

## Filtros disponíveis

```
GET /api/tarefas/filtro?status=Pendente
GET /api/tarefas/filtro?dataVencimentoInicio=2026-06-01&dataVencimentoFim=2026-06-30
GET /api/tarefas/filtro?status=Pendente&dataVencimentoInicio=2026-06-01
```

## Status disponíveis

| Valor       | Descrição                 |
| ----------- | ------------------------- |
| Pendente    | Tarefa ainda não iniciada |
| EmAndamento | Tarefa em execução        |
| Concluido   | Tarefa finalizada         |

## Exemplo de requisição

```json
POST /api/tarefas
{
  "titulo": "Minha tarefa",
  "descricao": "Descrição da tarefa",
  "status": "Pendente",
  "dataVencimento": "2026-06-30"
}
```

## Testes

O projeto conta com testes unitários cobrindo o `TarefaService`.

### Como rodar os testes

```bash
dotnet test
```

### Cobertura

| Método       | Cenário                          | Resultado |
| ------------ | -------------------------------- | --------- |
| GetAllAsync  | Retorna lista de tarefas         | ✅        |
| GetByIdAsync | Retorna tarefa quando existir    | ✅        |
| GetByIdAsync | Retorna null quando não existir  | ✅        |
| CreateAsync  | Cria tarefa com sucesso          | ✅        |
| UpdateAsync  | Atualiza tarefa com sucesso      | ✅        |
| UpdateAsync  | Retorna false quando não existir | ✅        |
| DeleteAsync  | Retorna true quando existir      | ✅        |
| DeleteAsync  | Retorna false quando não existir | ✅        |

## Estrutura do Projeto

```
TodoAPI/
├── Controllers/
│   └── TarefasController.cs
├── Data/
│   └── AppDbContext.cs
├── DTOs/
│   ├── Mappings/
│   │   └── MappingProfile.cs
│   ├── TarefaDTO.cs
│   └── TarefaFiltroDTO.cs
├── Models/
│   └── Tarefa.cs
├── Repositories/
│   ├── Interfaces/
│   │   └── ITarefaRepository.cs
│   └── TarefaRepository.cs
├── Services/
│   ├── Interfaces/
│   │   └── ITarefaService.cs
│   └── TarefaService.cs
└── Program.cs

TodoAPI.Tests/
└── TarefaServiceTests.cs
```
