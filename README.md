### Minesweeper-game

##### Minesweeper is a game where a player chooses a location on a grid and then can reveal, flag or if already flagged, deflag the location.
##### A location can beadd flagged by a player to mark a suspected mine location. Flagging is not necessary to win.
##### A player wins by selecting and revealing all locations that are not mines.
##### A player loses by selecting and revealing a location that is a mine.

#### Installation
System Requirements
A command line interface (CLI) such as Command Prompt for Windows or Terminal for macOS
.Net Core 5.0 SDK or later. Using ```homebrew``` you can install the latest version of the .NET Core SDK by running the command ```brew cask install dotnet-sdk``` in the CLI

#### Cloning
This repo can be cloned to your local machine using the command ```git clone``` along with the URL copied from repository on GitHub.
Once cloned, navigate into the folder containing the solution and type ```dotnet restore``` to install the package dependencies

#### Running the application
Navigate to the correct folder using the following command
```$ cd Minesweeper/Minesweeper```
Then type dotnet run to run the game
```$ dotnet run ```

#### In Game Options
When prompted, users have the option of entering "1" for a randomly-sized grid. The random grids can range in size from 3 x 3 to 10 x 10.
If a user enters "2", then the user can create a custom-sized grid with a user selected number of rows and columns.
In the case of both options "1" and "2" any mines created are randomly assigned to the grid each time a new game is run.
The next prompt is for a user to select a location e.g. (0,0) for the top left location on the grid
Following this input a user is prompted to (R)eveal or (F)lag their chosen location
If the user chooses to reveal, then the value of the cell at that location will be revealed.
This is achieved by changing the properties of the Cell, rebuilding the Grid and printing the string output to the Console.

#### Running the tests
Type ```$ dotnet test``` in your CLI to run the unit tests in the solution

#### Solution Dependencies
Unit Testing was developed using the XUnit - Testing framework
Moq was installed to provide Mocking capabilities for testing. This was necessary to Mock randomness, thereby manipulating where the mines would be located for testing purposes.
This allowed the GameTests to emulate an end-to-end user play routine ending in a win result or a lose result.
