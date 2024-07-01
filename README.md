# FanDuelTradingSolution

NFL Depth Charts API.

# Prerequisites
* `.NET 8`
* `Visual Studio 2022`
* `SQLite and SQL Server Compact Toolbox` extention for `Visual Studio 2022` (optional)

# Usage
1. Clone the repository to your local drive and open the solution in `Visual Studio 2022`.
1. Build the solution.
1. A SQLite database, `FanDuel.db`, with seed data is included in the repository. If you need to regenerate the database, run `update-database` in the Package Manager Console.
1. Launch the `https` configuration of the solution with or without debugging (`F5` or `Ctrl-F5` respectively).
1. A browser will start with a Swagger endpoint opened; explore the API from there.
1. Seed data are located in `FanDuelSolution.API\DbContexts\AppDbContext.cs` where all IDs to explore the API are.
1. To explore the database, install the `SQLite and SQL Server Compact Toolbox` extension and point it to `FanDuel.db`.

# Swagger endpoint
![](/FanDuelMain.png)


