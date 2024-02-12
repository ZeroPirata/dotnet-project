#### [02-11-2024]
- Correção da entidade `Session`:
    - A entidade `Session` agora contém apenas os campos `TokenId` e `UserId`
    - Os campos `BearerToken` e `Id` foram removidos
- Reajuste dos arquivos para reduzir o tamanho das classes:
    - Um arquivo para o `Json Web Token` foi criado como um Middleware
    - O registro do usuário foi movido para uma pasta separada
    - O `ServiceResponse` foi movido para a pasta de configuração
    - A injeção de dependência `Scoped` foi movida para a pasta de configuração, visando reduzir o tamanho do arquivo `Program.cs`
    - Um utilitário `Utils` foi criado, incluindo o arquivo `HashPassword`.
- Configuração do `user-secrt`
- Remoção da dependencia `DotEnv` do projeto

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