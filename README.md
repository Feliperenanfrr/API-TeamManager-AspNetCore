# 🏈 Team Manager API – Sistema de Gestão Esportiva

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-green.svg)](https://docs.microsoft.com/en-us/ef/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-13+-blue.svg)](https://www.postgresql.org/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## 📋 Sobre o Projeto

A **Team Manager API** é uma aplicação robusta desenvolvida em **.NET 8** seguindo os princípios da **Clean Architecture**, especialmente projetada para a **gestão completa de times amadores de futebol americano e flag football**. 

Este projeto nasceu como uma **contribuição voluntária e gratuita** para auxiliar equipes amadoras na digitalização de seus processos de gestão, oferecendo uma solução escalável e profissional.

---

## ✨ Funcionalidades Principais

- 👥 **Gestão de Atletas**: Cadastro completo com informações pessoais e posições
- 🏃‍♂️ **Gerenciamento de Treinos**: Controle de presenças e planejamento
- 📊 **Estatísticas Avançadas**: Registro e análise de performance por jogador
- 👨‍💼 **Gestão de Comissão Técnica**: Controle de coaches e staff
- 💰 **Controle Financeiro**: Gestão de transações e mensalidades
- 🔐 **Autenticação Segura**: Sistema completo com JWT
- 📱 **API RESTful**: Endpoints organizados e documentados
- 🎯 **Arquitetura Limpa**: Código desacoplado e testável

---

## 🏗️ Arquitetura

O projeto implementa **Clean Architecture** com clara separação de responsabilidades:

```
📁 TeamManager/
├── 🎯 TeamManager.API/              # Camada de Apresentação (Controllers, Middleware)
├── 💼 TeamManager.Application/      # Casos de Uso e Lógica de Aplicação
├── 🏛️ TeamManager.Domain/          # Entidades, Regras de Negócio e Interfaces
├── 🔧 TeamManager.Infrastructure/   # Persistência, Repositórios e Serviços Externos
└── 🧪 TeamManager.Test/            # Testes Unitários e de Integração
```

### Benefícios da Arquitetura:
- **Testabilidade**: Cada camada pode ser testada independentemente
- **Manutenibilidade**: Código organizado e fácil de evoluir  
- **Flexibilidade**: Fácil troca de tecnologias (banco, frameworks, etc.)
- **Escalabilidade**: Preparado para crescimento e novas funcionalidades

---

## 🛠️ Stack Tecnológica

### Backend
- **.NET 8** - Framework principal
- **Entity Framework Core** - ORM para acesso a dados
- **PostgreSQL** - Banco de dados relacional
- **JWT** - Autenticação e autorização

### Documentação & Testes
- **Swagger/OpenAPI** - Documentação interativa da API
- **xUnit** - Framework de testes unitários
  
---

## 🎯 Objetivos do Projeto

### Para a Comunidade
- 🏈 **Digitalizar o esporte amador** brasileiro
- 📈 **Profissionalizar a gestão** de equipes amadoras
- 🆓 **Oferecer solução gratuita** para times com recursos limitados
- 📊 **Auxiliar decisões técnicas** com dados estatísticos

### Para o Portfólio
- 💻 **Demonstrar expertise** em .NET e Clean Architecture
- 🏗️ **Mostrar capacidade** de criar soluções escaláveis
- 🧪 **Evidenciar boas práticas** de desenvolvimento
- 🚀 **Preparar base** para evolução para SaaS

---

## 🔄 Próximas Funcionalidades

- [ ] Dashboard com métricas em tempo real
- [ ] Integração com calendário (Google Calendar)
- [ ] Notificações push via WhatsApp/SMS
- [ ] Relatórios avançados em PDF
- [ ] App mobile (React Native)
- [ ] Sistema de pagamentos online

---

## 📞 Contato

**Felipe Renan** - Desenvolvedor Backend

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/felipe-renan/)
[![Email](https://img.shields.io/badge/Email-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:felipeferreira3146@gmail.com)

