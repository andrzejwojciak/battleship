# Battleship

## About

Project implements a game with rules based on [battleship game](https://en.wikipedia.org/wiki/Battleship_(game), program randomly places ships on two boards and simulates the gameplay between 2 players.

## Solution architecture

Project has been written using N-tier architecture, as well called Layered Architecture. But also have some CleanArchitecture features. User interface is created in VueJs as single page application and communicate with api using HTTP Request Methods.

## Code structure

Solution is divided to 4 projects. Backend part is written in .NET 6 and uses entity framework core with in memory database. Database seed is performed on application startup. Frontend part is written in VueJS 3.

| Project                  | Description |
|--------------------------| ----------- |
| Battleship.UI  | User interface - rest api and vue spa project |
| Battleship.Core  | Business Logic project |
| Battleship.Data   | Data Access Layer project |
| Battleship.UnitTests | Unit Tests project |

## Launching the project

### presequites
- [NodeJs](https://nodejs.org/en/ "NodeJs"), v16.13.0 or newer
- [.NET 6](https://dotnet.microsoft.com/en-us/download")
- Unused ports 44454 and 7284

### steps
1. Clone repository
2. Open cloned repository folder and change directory to ./Battleship.UI/
3. Run command `dotnet run` and wint for backend to build
4. Open url https://localhost:7284/ and wait for frontend to build
5. You can use the app