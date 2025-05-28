# Ambev Developer Evaluation - Sales API

API desenvolvida para o desafio técnico da Ambev, implementando o fluxo completo de vendas (**Sales Flow**) com arquitetura robusta baseada em **DDD, Clean Code e SOLID**.

---

## 🏗️ Arquitetura e Tecnologias

- ✅ **.NET 8**
- ✅ **C# 12**
- ✅ **Entity Framework Core**
- ✅ **MediatR** (CQRS com Commands e Queries)
- ✅ **AutoMapper**
- ✅ **FluentValidation**
- ✅ **Rebus** (publicação de eventos simulada)
- ✅ **Serilog** (logs estruturados com Console + File)
- ✅ **xUnit + FluentAssertions + NSubstitute + Bogus** (testes unitários e de integração)

Arquitetura baseada em:

- 🏛️ **Domain-Driven Design (DDD)**
- ♻️ **Clean Architecture**
- 🔥 **Princípios SOLID**
- 📦 Separação por módulos (Features, Application, Domain, Infrastructure, WebAPI)

---

## ✅ Funcionalidades da Sales API

- 🔸 Criar uma venda (`POST /api/sales`)
- 🔸 Consultar uma venda por ID (`GET /api/sales/{id}`)
- 🔸 Listar todas as vendas (`GET /api/sales`)
- 🔸 Cancelar uma venda (`DELETE /api/sales/{id}`)

Cada endpoint aplica:

- ✔️ Validações de entrada (FluentValidation)
- ✔️ Validações de domínio (Regras de negócio)
- ✔️ Logs estruturados com Serilog
- ✔️ Publicação de eventos (simulada com Rebus)

---

## 🔥 Observabilidade

- Logs estruturados:
  - Saída no **Console**
  - Arquivos diários em `logs/log-YYYY-MM-DD.txt`

Exemplos de logs:

```plaintext
[INF] Sale created successfully with Id 58eecb20-xxxx-xxxx-xxxx-xxxxxxxxxxxx
[WRN] Sale with Id 00000000-0000-0000-0000-000000000000 not found
