# Decidindo nome do projeto

### Objetivo

Após concluir o curso de DotNet oferecido pela `Digital Innovation One`, decidi empreender em um mini projeto destinado a aprimorar minhas habilidades nessa linguagem. Este projeto será exclusivamente back-end, sem qualquer interface web. A única ferramenta que estará presente será o `Swagger/OpenAi`, visando agilizar a consulta de rotas.

### Feitos

Caso queira ver o que foi feito dentro do projeto e seus dias, consulte o arquivo `changelog.md` na raiz do projeto.

### Utilitários

<b>Requisitos Necessários:</b>

- Docker | Postgres
- dotnet ef
- dotnet 8.0

#### Geração de um container Docker

```bash
docker pull postgres:16
docker run --name postgres-db -p 5432:5432 -e POSTGRES_PASSWORD=<Sua senha> -d postgres:16
docker exec -it postgres-db psql -U postgres
postgres=# CREATE DATABASE <Nome da Database>;
```

por definição o usuario do postgres é `postgres`

#### Instalação do Dotnet ef

```bash
dotnet tool install --global dotnet-ef --version <Versão Desejada>
dotnet ef migrations add GeracaoDasTabelas
dotnet ef database update
```

#### Inserir `Secrets` no projeto

##### PostgreSQL

```bash
dotnet user-secrets set "PostgreSQL:HOST" "<Value>"
dotnet user-secrets set "PostgreSQL:PORT" "<Value>"
dotnet user-secrets set "PostgreSQL:DATABASE" "<Value>"
dotnet user-secrets set "PostgreSQL:USER" "<Value>"
dotnet user-secrets set "PostgreSQL:PASSWORD" "<Value>"
```

##### Json Web Token

```bash
dotnet user-secrets set "Jwt:SecretKey" "<Value>"
dotnet user-secrets set "Jwt:Issuer" "<Value>"
dotnet user-secrets set "Jwt:Audience" "<Value>"
```
