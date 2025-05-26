# 🏈 Team Manager API – Sistema de Gestão Esportiva

**Descrição:**
A **Team Manager API** é uma aplicação desenvolvida em .NET com arquitetura inspirada na **Clean Architecture**, voltada para a **gestão de um time amador de futebol americano e flag football**. Este projeto foi criado de forma voluntária e visa oferecer uma solução gratutia e escalável para o gerenciamento do time.
---

## 📌 Funcionalidades

* Cadastro de atletas
* Gerenciamento de posições e times
* Controle de treinos e presenças
* Registro de estatísticas por jogador
* Sistema de autenticação (JWT)
* Arquitetura desacoplada (Clean Architecture)
* Pronto para deploy em cloud (AWS)

---

## 🧱 Arquitetura

> O projeto segue os princípios da **Clean Architecture**, com separação clara de responsabilidades entre as camadas:

```
/Domain         -> Entidades e interfaces (núcleo da regra de negócio)
├── Athlete.cs
├── ITeamRepository.cs

/Application    -> Casos de uso (Use Cases)
├── GetAllAthletes.cs
├── RegisterAthlete.cs

/Infrastructure -> Persistência de dados (EF Core, DbContext, Repos)
├── AppDbContext.cs
├── AthleteRepository.cs

/API            -> Controladores HTTP
├── AthleteController.cs
```

---

## 🥪 Tecnologias Utilizadas

* .NET 8
* Entity Framework Core
* PostgreSQL
* Swagger para documentação da API
* JWT para autenticação segura

---

## 📚 Documentação da API

* Documentação gerada com **Swagger** (acesso via `/swagger`)
* Rotas RESTful organizadas por entidade
* Tratamento de erros e validações de entrada

---

## 🚀 Objetivo do Projeto

Este projeto foi criado como **contribuição gratuita para um time amador**, com o intuito de:

* Gerenciar estatísticas dos atletas durante a temporada para auxiliar na tomada de decisão da comissão técnica
* Ajudar na organização de treinos e escalações
* Controlar presença e desempenho dos atletas
* Incentivar a digitalização do esporte amador
* **Demonstrar minha capacidade técnica** com .NET e arquitetura limpa

---

## 💼 Para o Portfólio

* Demonstra habilidades em **backend moderno com boas práticas**
* Projeto **real e funcional**, pronto para escalar
* Código limpo, reutilizável e testável
* Pronto para evoluir para um **SaaS completo**

