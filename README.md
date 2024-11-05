# FootballTeamsApp

Projeto de aplicativo web para exibir informações de times de futebol e estatísticas de jogadores. Desenvolvido com ASP.NET Core MVC e Razor Pages, o projeto oferece uma interface para visualizar detalhes de equipes, jogadores e suas estatísticas em tempo real.

## 📄 Descrição

O **FootballTeamsApp** é um sistema web desenvolvido para mostrar dados de equipes e jogadores de futebol, incluindo estatísticas como partidas jogadas, gols, assistências, cartões, entre outros. O projeto foi criado com o objetivo de explorar o uso de ASP.NET Core MVC e Razor Pages, conectando-se a um banco de dados para gerenciar e exibir informações esportivas.

## ✨ Funcionalidades

- Exibição de detalhes de times e jogadores.
- Integração com uma API para obter dados atualizados (simulada para o projeto).
- Visualização de estatísticas dos jogadores, incluindo:
  - Partidas jogadas
  - Gols e assistências
  - Cartões amarelos e vermelhos
- Filtros por posição e time.
- Interface amigável e responsiva.

## 🛠️ Tecnologias

- **ASP.NET Core MVC** e **Razor Pages** - para estruturação da aplicação e renderização do front-end.
- **Entity Framework Core** - para acesso ao banco de dados.
- **SQL Server** - para armazenamento dos dados de equipes e jogadores.
- **HTML5, CSS3 e JavaScript** - para a criação da interface e interatividade.
- **Bootstrap** - para responsividade.
- **Visual Studio 2022** - como ambiente de desenvolvimento.

## 📝 Pré-requisitos

Antes de iniciar, certifique-se de ter as seguintes ferramentas instaladas:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) ou [Azure SQL](https://azure.microsoft.com/pt-br/services/sql-database/) (se estiver hospedando o banco na nuvem)
- Visual Studio 2022 (ou um editor de sua preferência)
- Git (para controle de versão)

## 🚀 Configuração e Execução

### 1. Clone o Repositório

```bash
git clone https://github.com/username/FootballTeamsApp.git
cd FootballTeamsApp
```

### 2. Configuração do Banco de Dados
Crie um banco de dados no SQL Server chamado FootballDB.
Atualize a string de conexão em appsettings.json com suas credenciais do SQL Server:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=FootballDB;User Id=SEU_USUARIO;Password=SUA_SENHA;"
}
```

###3. Aplicar as Migrações do Entity Framework
Use o comando a seguir para criar o banco de dados e aplicar as migrações:

```bash
dotnet ef database update
```
### 4. Executar o Projeto
No diretório do projeto, execute o comando:

```bash
dotnet run
```
O projeto estará disponível em http://localhost:5000 no seu navegador.

## 📂 Estrutura do Projeto
/Controllers - Contém os controladores que gerenciam as requisições HTTP.
/Models - Define as classes de dados e entidades do Entity Framework.
/Views - Contém as Razor Views para renderizar o front-end.
/wwwroot - Diretório de arquivos estáticos (CSS, JavaScript, imagens).
/Data - Contém o contexto do Entity Framework para interagir com o banco de dados.

## 🤝 Contribuições
Contribuições são sempre bem-vindas! Sinta-se à vontade para fazer um fork do projeto, criar uma branch para suas alterações e enviar um pull request.
