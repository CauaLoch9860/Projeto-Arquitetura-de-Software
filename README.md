# Projeto-Arquitetura-de-Software
Documento de Requisitos do Sistema

1. Propósito do Sistema
O sistema tem como objetivo gerenciar pagamentos de usuários em uma aplicação baseada em microsserviços. Ele permite criar, consultar, atualizar e excluir informações de pagamentos, além de verificar a existência de usuários no microsserviço de Autenticação. Este sistema opera como parte de uma arquitetura distribuída, interligando outros microsserviços, como Estoque e Autenticação, para compor a funcionalidade geral da aplicação.

2. Usuários do Sistema
Usuários Administrativos
São responsáveis por gerenciar os pagamentos, realizando operações de criação, leitura, atualização e exclusão.

Acessam via endpoints API.
Podem monitorar e gerenciar erros e dados.
Microsserviços Integrados
Outros microsserviços que utilizam a API para validar informações ou realizar operações relacionadas aos pagamentos:

Autenticação: Fornece validação de usuários.
Estoque e Pagamentos: Dependem dos dados transacionais do sistema de pagamentos para integração.

3. Requisitos Funcionais
3.1. Operações sobre Usuários
Listar Usuários

O sistema deve permitir a recuperação de todos os usuários cadastrados.
Endpoint: GET /api/Usuarios.
Deve retornar uma mensagem de erro caso não existam usuários cadastrados.
Consultar Usuário por ID

O sistema deve permitir a consulta de um usuário específico com base no ID.
Endpoint: GET /api/Usuarios/{id}.
Deve retornar uma mensagem de erro caso o ID do usuário não seja encontrado.
Criar Usuário

O sistema deve permitir o cadastro de um novo usuário.
Endpoint: POST /api/Usuarios.
Deve validar que os dados enviados no corpo da requisição não sejam nulos ou inválidos.
Dados necessários:
Nome.
E-mail.
Senha.
Atualizar Usuário

O sistema deve permitir a atualização de informações de um usuário existente.
Endpoint: PUT /api/Usuarios/{id}.
Deve validar se o ID do usuário existe antes de realizar a atualização.
Dados que podem ser atualizados:
Nome.
E-mail.
Senha.
Excluir Usuário

O sistema deve permitir a exclusão de um usuário pelo ID.
Endpoint: DELETE /api/Usuarios/{id}.
Deve retornar uma mensagem de erro caso o ID do usuário não seja encontrado.

3.2. Operações de Pagamento
Criar Pagamento

O sistema deve permitir a criação de um pagamento associado a um usuário.
Deve validar se o usuário existe no microsserviço de Autenticação.
Endpoint: POST /api/Pagamentos.
Consultar Pagamento por ID

O sistema deve permitir a recuperação de informações de um pagamento existente por meio do ID.
Endpoint: GET /api/Pagamentos/{id}.
Atualizar Pagamento

O sistema deve permitir a atualização de um pagamento existente, alterando dados como valor, usuário ou data.
Endpoint: PUT /api/Pagamentos/{id}.
Excluir Pagamento

O sistema deve permitir a exclusão de um pagamento existente, identificado por seu ID.
Endpoint: DELETE /api/Pagamentos/{id}.
3.2. Validação de Usuários
O sistema deve consultar o microsserviço de Autenticação para verificar se o ID do usuário associado a um pagamento existe antes de processar a criação ou atualização do pagamento.
3.3. Persistência de Dados
O sistema deve armazenar informações de pagamento em um banco de dados SQLite, incluindo:
ID do pagamento.
ID do usuário.
Valor do pagamento.
Data do pagamento.

3.1. Operações sobre Produtos
Cadastrar Produto

O sistema deve permitir o cadastro de um novo produto associado a um usuário existente.
Endpoint: POST /api/Produtos.
Deve verificar a existência do usuário no microsserviço de Autenticação utilizando o UsuarioId antes de associar o produto.
Dados necessários:
Nome do produto.
Quantidade.
UsuarioId (ID do usuário proprietário do produto).
Deve retornar erro caso o usuário não seja encontrado ou os dados do produto sejam inválidos.
Consultar Produto por ID

O sistema deve permitir a consulta de um produto específico com base no ID.
Endpoint: GET /api/Produtos/{id}.
Deve retornar uma mensagem de erro caso o produto não seja encontrado.
Atualizar Produto

O sistema deve permitir a atualização das informações de um produto existente.
Endpoint: PUT /api/Produtos/{id}.
Deve validar se o ID do produto existe antes de realizar a atualização.
Dados que podem ser atualizados:
Nome do produto.
Quantidade.
Excluir Produto

O sistema deve permitir a exclusão de um produto pelo ID.
Endpoint: DELETE /api/Produtos/{id}.
Deve retornar uma mensagem de erro caso o produto não seja encontrado.


3.2. Integração com o Microsserviço de Autenticação
O sistema deve integrar-se ao microsserviço de Autenticação para verificar a existência de um usuário antes de associar um produto:
Utiliza um cliente HTTP para consultar o endpoint GET /api/Usuarios/{UsuarioId}.
Deve retornar erro caso a verificação falhe ou o usuário não exista.
3.3. Persistência de Dados
O sistema deve armazenar as informações dos produtos em um banco de dados SQLite, contendo os seguintes campos:
ID (chave primária, gerado automaticamente).
Nome.
Quantidade.
UsuarioId (ID do usuário associado ao produto, representando a relação entre Produto e Usuário).
3.4. Disponibilidade e Gerenciamento
O sistema deve incluir uma interface Swagger para documentar e testar os endpoints da API.
Mensagens claras devem ser retornadas em caso de erros, como:
Dados inválidos ou nulos enviados no corpo da requisição.
Produto ou usuário não encontrado ao consultar, atualizar ou excluir.
3.5. Validação e Consistência
O sistema deve validar a consistência dos dados antes de salvar ou atualizar registros, como:
Quantidade de produtos não pode ser negativa.
O nome do produto deve ser informado e não pode ser vazio.
3.3. Integração com Outros Microsserviços
O sistema deve estar disponível para consultas HTTP realizadas por outros microsserviços, especialmente para validação de IDs de usuários.
