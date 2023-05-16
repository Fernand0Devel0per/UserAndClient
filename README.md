# 📰 UserAndClient API

Bem-vindo à API do UserAndClient, uma API de gerenciamento de usuários e clientes desenvolvida em ASP.NET Core 7.0, utilizando o acesso direto ao banco de dados SQL Server com a biblioteca Data.SqlClient. Este projeto foi projetado para fornecer uma maneira simples e eficaz de gerenciar usuários e seus clientes associados.

# 🌟 Funcionalidades

    CRUD completo para usuários e clientes.
    Recuperação de clientes por ID do usuário.
    API REST seguindo as boas práticas de design.

# 🚀 Começando

Para começar a usar a API do UserAndClient, siga as etapas abaixo:

    Clone este repositório em sua máquina local.
    Configure a string de conexão do SQL Server no arquivo appsettings.json.
    Execute o comando dotnet restore para restaurar os pacotes NuGet necessários.
    Execute o comando dotnet run para iniciar a aplicação.

A API estará disponível no endereço http://localhost:5000.

# 📚 Rotas

A API do UserAndClient possui as seguintes rotas:

Usuários:

    GET /api/users/{id}: Recupera um usuário pelo ID.
    POST /api/users: Cria um novo usuário.
    PUT /api/users/{id}: Atualiza um usuário existente.
    DELETE /api/users/{id}: Exclui um usuário.

Clientes:

    GET /api/clients/{id}: Recupera um cliente pelo ID.
    GET /api/clients/user/{userId}: Recupera todos os clientes associados a um usuário.
    POST /api/clients: Cria um novo cliente.
    PUT /api/clients/{id}: Atualiza um cliente existente.
    DELETE /api/clients/{id}: Exclui um cliente.

# 🛠️ Tecnologias utilizadas

    ASP.NET Core 7.0
    SQL Server
    Data.SqlClient
    AutoMapper
    Swagger

# 📖 Conclusão

A API do UserAndClient é uma solução completa para gerenciamento de usuários e clientes, seguindo as melhores práticas de desenvolvimento e arquitetura. Com esta API, você pode criar, atualizar, excluir e recuperar usuários e seus clientes associados.

Além disso, a API segue os princípios RESTful, facilitando a integração com outras aplicações e garantindo escalabilidade e manutenibilidade.

Esperamos que você aproveite este projeto e que ele atenda às suas necessidades de gerenciamento de usuários e clientes. Se você tiver alguma dúvida ou sugestão, sinta-se à vontade para abrir uma issue ou enviar um pull request.

Boa sorte e feliz codificação! 🚀👩‍💻👨‍💻

## English Version

# 📰 UserAndClient API

Welcome to the UserAndClient API, a user and client management API developed in ASP.NET Core 7.0, using direct access to the SQL Server database with the Data.SqlClient library. This project has been designed to provide a simple and effective way to manage users and their associated clients.

# 🌟 Features

    Complete CRUD for users and clients.
    Retrieval of clients by user ID.
    REST API following good design practices.

# 🚀 Getting Started

To start using the UserAndClient API, follow these steps:

    Clone this repository to your local machine.
    Configure the SQL Server connection string in the appsettings.json file.
    Run the dotnet restore command to restore the necessary NuGet packages.
    Run the dotnet run command to start the application.

The API will be available at http://localhost:5000.

# 📚 Routes

The UserAndClient API provides the following routes:

Users:

    GET /api/users/{id}: Retrieves a user by ID.
    POST /api/users: Creates a new user.
    PUT /api/users/{id}: Updates an existing user.
    DELETE /api/users/{id}: Deletes a user.

Clients:

    GET /api/clients/{id}: Retrieves a client by ID.
    GET /api/clients/user/{userId}: Retrieves all clients associated with a user.
    POST /api/clients: Creates a new client.
    PUT /api/clients/{id}: Updates an existing client.
    DELETE /api/clients/{id}: Deletes a client.

# 🛠️ Technologies Used

    ASP.NET Core 7.0
    SQL Server
    Data.SqlClient
    AutoMapper
    Swagger

# 📖 Conclusion

The UserAndClient API is a complete solution for user and client management, following the best practices of development and architecture. With this API, you can create, update, delete, and retrieve users and their associated clients.

In addition, the API follows RESTful principles, making it easy to integrate with other applications and ensuring scalability and maintainability.

We hope you enjoy this project and that it meets your user and client management needs. If you have any questions or suggestions, feel free to open an issue or send a pull request.

Good luck and happy coding! 🚀👩‍💻👨‍💻