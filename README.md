# .NET REST API - Attendance Tracker

> *A primeira versão ainda não foi disponibilizada e o projeto ainda vai ter algumas mudanças.*

Um sistema intermediário para **gerenciamento de presença acadêmica**, desenvolvido com **ASP.NET Core, API & Razor Pages**.  
Permite o **registro, listagem e acompanhamento em tempo real** de alunos, com interface simples e funcional.

![.NET](https://img.shields.io/badge/.NET-9.0-blue)
![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)
![Build](https://github.com/JehanLuk/attendancetracker-api/actions/workflows/dotnet.yml/badge.svg)
![PostgreSQL](https://img.shields.io/badge/Database-PostgreSQL-blue)

## 🚀 Tecnologias Utilizadas

- **.NET 9 / ASP.NET Core**
- **Razor Pages** - template de projeto.
- **SignalR** - notificações em tempo real.
- **IMemoryCache** - cache em memória para armazenamento temporário.
- **QRCoder** - geração de QR Codes para identificação de alunos.
- **Chart.js** - exibição de dados em gráficos interativos.
- **OpenAPI (Swagger)** - documentação automática da API.

### Passos no CLI

```bash
# Clonar o repositório
git clone https://github.com/JehanLuk/attendancetracker-api.git
cd attendancetracker-api

# Restaurar dependências
dotnet restore

# Executar o servidor (localmente)
dotnet run
```