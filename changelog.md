#### [02-13-2024]
- Desenvolvimento de três novas entidades:
    - `UserLikedMovie`: Responsável por armazenar o critério do usuário em relação a um filme.
    - `Role`: Gerenciamento de privilégios dentro do sistema.
    - `UserRole`: Tabela de ligação entre `Role` e `Usuário`.
- Criação de rotas finais para `Movie`:
    - `Atualizar informações básicas`: Permite a atualização das informações básicas do filme.
    - `Atualizar Equipe`: Permite a atualização dos integrantes da equipe do filme com base em um formato JSON.
- Criação de rotas para `Review`:
    - `Criar`: Permite que o usuário adicione um comentário para um filme específico.
    - `Atualizar`: Permite a atualização do comentário em um filme específico.
    - `Deletar`: Permite a exclusão de um comentário.
- Criação de rotas para `Rating`:
    - `Curtir`: Permite que o usuário curta um filme.
    - `Avaliar`: Permite que o usuário dê uma nota de 1 a 5 para o filme.
    - `Avaliação`: Indica se o usuário avaliou positivamente o filme (`True`) ou não (`False`).
- Adição de um `Handler` para verificar a `role` do usuário logado no sistema.
- Alteração do `JWT` para buscar as `roles` do usuário com base no seu `Sub`.
- Criação de políticas (`Policy`) para `Usuário` e `Administrador`.
- Geração automática das `roles` mencionadas acima na primeira execução do banco de dados.
- Criação de DTOs para `Rating` e `Review`.
- Adição do pacote `AutoMapper` para facilitar a execução do `Update`.

#### [02-12-2024]
- Desenvolvimento de duas novas Entidades:
    - `Movie`: Tabela para armazenar informações sobre os filmes dentro do sistema.
    - `Review`: Tabela para armazenar as avaliações dos usuários em relação aos filmes.
- Criação de Rotas para `Movies`:
    - `ReadOne`: Leitura de um filme pelo seu `Guid`.
    - `ReadMany`: Leitura de todos os `Movies` no banco de dados.
    - `Delete`: Remoção de um `Movie` pelo seu `Guid`.
    - `GetByCrew`: Busca de filmes através da função e nome da pessoa envolvida.
    - `Create`: Criação de novos filmes no banco de dados.
- Implementação da função para gerar a nota do filme através do campo `Ratings` do `Movie`.
- Criação do DTO para `Movie` e sua pasta para futuros `DTOs`.

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