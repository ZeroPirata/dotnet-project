#### [02-12-2024
- Desenvolvimento de duas novas Entidades:
    - `Movie`: Tabela para armazenar informa��es sobre os filmes dentro do sistema.
    - `Review`: Tabela para armazenar as avalia��es dos usu�rios em rela��o aos filmes.
- Cria��o de Rotas para `Movies`:
    - `ReadOne`: Leitura de um filme pelo seu `Guid`.
    - `ReadMany`: Leitura de todos os `Movies` no banco de dados.
    - `Delete`: Remo��o de um `Movie` pelo seu `Guid`.
    - `GetByCrew`: Busca de filmes atrav�s da fun��o e nome da pessoa envolvida.
    - `Create`: Cria��o de novos filmes no banco de dados.
- Implementa��o da fun��o para gerar a nota do filme atrav�s do campo `Ratings` do `Movie`.
- Cria��o do DTO para `Movie` e sua pasta para futuros `DTOs`.

#### [02-11-2024]
- Corre��o da entidade `Session`:
    - A entidade `Session` agora cont�m apenas os campos `TokenId` e `UserId`
    - Os campos `BearerToken` e `Id` foram removidos
- Reajuste dos arquivos para reduzir o tamanho das classes:
    - Um arquivo para o `Json Web Token` foi criado como um Middleware
    - O registro do usu�rio foi movido para uma pasta separada
    - O `ServiceResponse` foi movido para a pasta de configura��o
    - A inje��o de depend�ncia `Scoped` foi movida para a pasta de configura��o, visando reduzir o tamanho do arquivo `Program.cs`
    - Um utilit�rio `Utils` foi criado, incluindo o arquivo `HashPassword`.
- Configura��o do `user-secrt`
- Remo��o da dependencia `DotEnv` do projeto

#### [02-10-2024]
- Inicializa��o do Projeto e Defini��o da Estrutura
- Estabelecimento da Conex�o com o Banco de Dados `PostgreSQL`
- Desenvolvimento de Duas Entidades:
  - User
    - Armazenamento de Informa��es do Usu�rio
  - Session
    - Gest�o de Sess�es do Usu�rio
- Implementa��o da Autentica��o com `JWT (JSON Web Tokens)`
- Criptografia de Senhas com `Argon2` 
- Defini��o de Rotas para Verificar Integridade da API e do Banco de Dados
- Cria��o de Rotas para Cadastro e Login de Usu�rios