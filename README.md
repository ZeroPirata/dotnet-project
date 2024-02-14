# Decidindo nome do projeto

### Objetivo

Após concluir o curso de DotNet oferecido pela `Digital Innovation One`, decidi empreender em um mini projeto destinado a aprimorar minhas habilidades nessa linguagem. Este projeto será exclusivamente back-end, sem qualquer interface web. A única ferramenta que estará presente será o `Swagger/OpenAi`, visando agilizar a consulta de rotas. <br/>
Decidi seguir uma sugestão do meu irmão e embarcar em um projeto desafiador: criar algo semelhante ao site/aplicativo `LetterBox`, como forma de consolidar o conhecimento adquirido durante o curso. Ao longo de três dias e meio, dediquei parte do meu tempo diário a esse desafio, com o objetivo de entregá-lo até o dia 14/02/2024. O resultado que apresento agora reflete o esforço e a criatividade empregados nesse período. <br/>
Embora reconheça que há espaço para melhorias e ajustes, sinto-me satisfeito com o que consegui realizar dentro do prazo estabelecido. Ainda assim, percebo que poderia aprimorar alguns aspectos com mais calma e dedicação. Desenvolver essa aplicação do zero, sempre considerando "Como o LetterBox o faz?", foi um desafio instigante. Espero, no futuro, dispor de mais tempo nas próximas semanas para retornar a este projeto e refiná-lo ainda mais. A experiência de criar e explorar o ambiente `dotnet` foi gratificante e despertou meu interesse em continuar aprimorando minhas habilidades nessa área.

### O que eu gostei de fazer
Sinceramente, todo o projeto em si foi algo desafiador por estar fazendo totalmente do inicio essa aplicação, mas o que eu mais gostei de fazer com toda a certeza foi a authenticação utilizando `JWT` e o `CustomHandler` que foi foi totalmente do zero sem utilizar o `IdentityUser` do dotnet, foi algo totalmente prazerosso de ver funcionando, e vendo na pratica ainda.
### O que eu não goste
Logicamente que enfrentei alguns desafios durante o desenvolvimento do projeto. Um deles foi compreender a importância de colocar todas as coisas dentro do escopo (`Scoped`). Levei um tempo para perceber que isso era obrigatório e, consequentemente, perdi algumas horas nesse processo de aprendizado.

Além disso, outro obstáculo foi minha tentativa inicial de executar uma query que acabou não funcionando. Isso foi bastante frustrante para mim. No entanto, não desisti e acabei encontrando uma solução usando o próprio `Entity Framework`. No final das contas, essa situação se transformou em uma situação de ganha-ganha.

### Possíveis melhorias futuras:

1. Implementar um serviço para associar funções (roles) aos usuários, tornando mais fácil a gestão de permissões.
2. Refinar e otimizar algumas funções e consultas no sistema.
3. Explorar a possibilidade de integrar um banco de dados NoSQL para avaliações de filmes.
4. Investigar a viabilidade de hospedar a aplicação na Oracle Cloud.
5. Desenvolver uma interface web para tornar a aplicação mais acessível e amigável ao usuário final.
6. Incorporar serviços de armazenamento em nuvem, como o Amazon S3, para gerenciar e exibir imagens dos filmes.
7. Implementar funcionalidades restantes relacionadas à gestão de usuários, como atualização e leitura de informações.
8. Criar uma tabela de "Favoritos" para permitir que os usuários salvem e acessem facilmente seus filmes preferidos.
9. Revisar e corrigir eventuais erros de ortografia e gramática presentes na aplicação.

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
