#### [02-13-2024]
- Desenvolvimento de tr�s novas entidades:
    - `UserLikedMovie`: Respons�vel por armazenar o crit�rio do usu�rio em rela��o a um filme.
    - `Role`: Gerenciamento de privil�gios dentro do sistema.
    - `UserRole`: Tabela de liga��o entre `Role` e `Usu�rio`.
- Cria��o de rotas finais para `Movie`:
    - `Atualizar informa��es b�sicas`: Permite a atualiza��o das informa��es b�sicas do filme.
    - `Atualizar Equipe`: Permite a atualiza��o dos integrantes da equipe do filme com base em um formato JSON.
- Cria��o de rotas para `Review`:
    - `Criar`: Permite que o usu�rio adicione um coment�rio para um filme espec�fico.
    - `Atualizar`: Permite a atualiza��o do coment�rio em um filme espec�fico.
    - `Deletar`: Permite a exclus�o de um coment�rio.
- Cria��o de rotas para `Rating`:
    - `Curtir`: Permite que o usu�rio curta um filme.
    - `Avaliar`: Permite que o usu�rio d� uma nota de 1 a 5 para o filme.
    - `Avalia��o`: Indica se o usu�rio avaliou positivamente o filme (`True`) ou n�o (`False`).
- Adi��o de um `Handler` para verificar a `role` do usu�rio logado no sistema.
- Altera��o do `JWT` para buscar as `roles` do usu�rio com base no seu `Sub`.
- Cria��o de pol�ticas (`Policy`) para `Usu�rio` e `Administrador`.
- Gera��o autom�tica das `roles` mencionadas acima na primeira execu��o do banco de dados.
- Cria��o de DTOs para `Rating` e `Review`.
- Adi��o do pacote `AutoMapper` para facilitar a execu��o do `Update`.

#### [02-12-2024]
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