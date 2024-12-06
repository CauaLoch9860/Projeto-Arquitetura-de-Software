***Projeto - Arquitetura de Software***
Documento de Requisitos do Sistema
1. Propósito do Sistema
O sistema gerencia pagamentos de usuários em uma aplicação baseada em microsserviços. Ele oferece funcionalidades de criação, consulta, atualização e exclusão de informações de pagamentos, além de verificar a existência de usuários no microsserviço de Autenticação. Esse sistema integra-se com outros microsserviços, como Estoque e Autenticação, para compor a funcionalidade geral da aplicação.

2. Usuários do Sistema
2.1. Usuários Administrativos
Gerenciam pagamentos (criação, leitura, atualização e exclusão).
Acessam via endpoints API.
Monitoram e gerenciam erros e dados.
2.2. Microsserviços Integrados
Outros microsserviços que utilizam a API para validar informações ou realizar operações:

Autenticação: Valida usuários.
Estoque e Pagamentos: Dependem dos dados transacionais para integração.
3. Requisitos Funcionais
3.1. Operações sobre Usuários
Listar Usuários

Endpoint: GET /api/Usuarios
Retorna todos os usuários cadastrados.
Retorna erro se não houver usuários.
Consultar Usuário por ID

Endpoint: GET /api/Usuarios/{id}
Retorna o usuário com o ID especificado.
Retorna erro se o ID não for encontrado.
Criar Usuário

Endpoint: POST /api/Usuarios
Valida que os dados enviados não sejam nulos ou inválidos.
Dados necessários:
Nome
E-mail
Senha
Atualizar Usuário

Endpoint: PUT /api/Usuarios/{id}
Valida se o ID do usuário existe antes de atualizar.
Dados que podem ser atualizados:
Nome
E-mail
Senha
Excluir Usuário

Endpoint: DELETE /api/Usuarios/{id}
Retorna erro se o ID do usuário não for encontrado.
3.2. Operações de Pagamento
Criar Pagamento

Endpoint: POST /api/Pagamentos
Valida a existência do usuário no microsserviço de Autenticação.
Consultar Pagamento por ID

Endpoint: GET /api/Pagamentos/{id}
Atualizar Pagamento

Endpoint: PUT /api/Pagamentos/{id}
Permite alterações em:
Valor
Usuário
Data
Excluir Pagamento

Endpoint: DELETE /api/Pagamentos/{id}
3.3. Persistência de Dados - Pagamentos
Banco de dados SQLite deve armazenar:

ID do pagamento
ID do usuário
Valor do pagamento
Data do pagamento
3.4. Operações sobre Produtos
Cadastrar Produto

Endpoint: POST /api/Produtos
Valida a existência do usuário no microsserviço de Autenticação antes de associar o produto.
Dados necessários:
Nome do produto
Quantidade
UsuarioId
Consultar Produto por ID

Endpoint: GET /api/Produtos/{id}
Retorna erro se o produto não for encontrado.
Atualizar Produto

Endpoint: PUT /api/Produtos/{id}
Valida se o ID do produto existe antes de atualizar.
Dados que podem ser atualizados:
Nome do produto
Quantidade
Excluir Produto

Endpoint: DELETE /api/Produtos/{id}
Retorna erro se o produto não for encontrado.
3.5. Persistência de Dados - Produtos
Banco de dados SQLite deve armazenar:

ID (chave primária)
Nome
Quantidade
UsuarioId (relacionamento entre Produto e Usuário)
4. Integração com o Microsserviço de Autenticação
Verifica a existência de um usuário antes de associar um produto ou criar/atualizar pagamentos.
Realizada via cliente HTTP no endpoint GET /api/Usuarios/{UsuarioId}.
Retorna erro caso o usuário não exista.
5. Validação e Consistência
Validações gerais:
Quantidade de produtos não pode ser negativa.
Nome do produto não pode ser vazio.
Mensagens claras em caso de erros, como:
Dados inválidos ou nulos.
Produto ou usuário não encontrado.
6. Documentação e Testes
Disponibilizar interface Swagger para documentar e testar os endpoints da API.
7. Disponibilidade e Gerenciamento
O sistema deve estar disponível para consultas HTTP por outros microsserviços, especialmente para validação de IDs de usuários.
