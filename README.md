# Decidindo nome do projeto...

### Objetivo
Após concluir o curso de DotNet oferecido pela `Digital Innovation One`, decidi empreender em um mini projeto destinado a aprimorar minhas habilidades nessa linguagem. Este projeto será exclusivamente back-end, sem qualquer interface web. A única ferramenta que estará presente será o `Swagger/OpenAi`, visando agilizar a consulta de rotas.

### Feitos
#### [02-10-2024]
- Inicialização do Projeto e Definição da Estrutura
- Estabelecimento da Conexão com o Banco de Dados `PostgreSQL`
- Desenvolvimento de Duas Entidades:
  - User
    - Armazenamento de Informações do Usuário
  - Session
    - Gestão de Sessões do Usuário
- Implementação da Autenticação com `JWT (JSON Web Tokens)`
- Criptografia de Senhas com `Argon2` 
- Definição de Rotas para Verificar Integridade da API e do Banco de Dados
- Criação de Rotas para Cadastro e Login de Usuários

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
