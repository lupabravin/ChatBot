
# ChatBot

**Jobisty Back-End Challenge**
[.NET Core](https://dotnet.microsoft.com/download) Web Application that implements a Chat with SignalR and a Bot with RabbitMQ
## Features
- Registered users can log in and talk with other users in a chatroom.
- Users can post messages as commands into the chatroom with the following format: **/stock=stock_code**
- Decoupled Bot that calls [Stooq](stooq.com) to get the user's requested stock info
- Only the last 50 chat messages are shown ordered by their timestamps.
- Services Unit Testing with [XUnit](https://xunit.net/) and Fake [SQLite DB](https://www.sqlite.org/).
- [Identity Server](https://identityserver.io/) Authentication

## Installation with Docker

1. Install [Docker](https://docker.com) and [Docker Compose](https://docs.docker.com/compose/install/) in your machine.
2. Clone the repository and go to the root folder
3. Open up your terminal and run the following command to build the containers: 
	```bash
	$ docker-compose build
	```
4. Run this next command to start. After that you'll need to wait a few seconds until SQL Server and RabbitMQ are up and running to start using the Chat.
	```bash
	$ docker-compose up
	```
5. The Chat Application will be hosted on http://localhost:4000. You may access it with any browser.

## Default Installation
1.  Download and Install:
	- [RabbitMQ Server](https://www.rabbitmq.com/download.html) 
	- [SQL Server 2019](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
	 - [.NET 5 Build SDK](https://dotnet.microsoft.com/download/dotnet/5.0) 
	 - [.NET 5 Runtime](https://dotnet.microsoft.com/download/dotnet/5.0) 
2.  Change the RabbitMQ Connection String in both appsettings.json files at /Chat.Bot and /Chat.WebApp
3.  Open a terminal in the root folder and run the following commands to build the Chat application:
	```
	$ dotnet restore
	$ dotnet build
	```
4.  Open another terminal in /Chat.Bot folder an run the following command to start the Bot:
	```
	$ dotnet run
	```
5.  Open another terminal and run the following command in the /Chat.WebApp folder to start the Chat Web Application
	```
	$ dotnet run
	```
6.  The Chat Application will be hosted on http://localhost:5000. You may access it with any browser.
## Testing the Application
1. Go to the /Chat.Tests folder
2. Open a terminal and run the following command:
```$ dotnet tests```
## Usage

1. Register with your e-mail account
2. Log in with your registered account
3. Type your message inside the page's text input
	- If you want to get some stock prices, write down '/stock={stockcode}' where {stockcode} is the desired stock code. 
4. Press the button 'Send' to send your message. If you wrote a command, the ChatBot should answer.
## Author
**Luís Paulo Bravin Pires**
- [Linkedin](https://www.linkedin.com/in/lu%C3%ADs-paulo-bravin-291348110/)
- [GitHub](https://github.com/lupabravin/)

## License
[MIT](https://choosealicense.com/licenses/mit/)
