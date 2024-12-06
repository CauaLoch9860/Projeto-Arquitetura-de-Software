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
