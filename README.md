# BattleshipApi

Example Battleship API written in dotnet 6.

## Instructions

* Clone this repository
* Open a terminal and navigate to the directory where the source was cloned to
* Run `dotnet restore` then `dotnet run --project .\BattleshipApi\BattleshipApi.csproj`
* The code should build and then run a Kestrel server on localhost:5000.
* View the Swagger / OpenAPI documentation at http://localhost:5000/swagger/index.html

## Tests

* A considerable number of unit tests have been written, please refer to BattleshipApi.UnitTests and BattleshipApi.IntegrationTests

## Project structure

* **BattleshipApi**: the dotnet 6 API application
* **BattleshipApi.Common**: common classes / enums / interfaces used between the api and core logic of the application
* **BattleshipApi.Core**: contains the core game logic for Battleship

## AWS EBS 

Build the package for AWS EBS using `dotnet publish .\BattleshipApi\BattleshipApi.csproj -c Release`, zip the contents and upload.
