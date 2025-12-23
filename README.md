# PokemonSearchWPF

A modern WPF application for searching and displaying Pokémon data from the PokeAPI. This application serves as a practical example of building a responsive and modern desktop application using Windows Presentation Foundation (WPF).

> [!IMPORTANT]
> **Local API Environment Required**
> This application **requires** a local instance of the PokeAPI to function correctly. The search functionality utilizes complex queries that are disabled or rate-limited on the public online API (`pokeapi.co`) to maintain performance. You must run the API via Docker as described in the "Getting Started" section below.

## Features

* **Modern and Responsive UI:** A clean and intuitive user interface built with XAML, featuring a custom-styled window and controls.
* **Real-time Pokémon Search:** Search for Pokémon by name with real-time feedback.
* **Detailed Pokémon Information:** View detailed information for each Pokémon, including its name, type, and image. The application handles cases where a Pokémon's image is not available, displaying a placeholder image instead.
* **Custom Fonts and Styling:** The application uses custom fonts and styles to create a unique and visually appealing user experience.
* **Asynchronous Data Fetching:** Pokémon data is fetched asynchronously from the local PokeAPI to ensure the UI remains responsive.

## Project Structure

The solution is organized into two main projects:

### `PokemonSearchWPF`

This is the main WPF application project.

* `Assets/`: Contains all the static assets for the application, such as images (`.png`), fonts (`.ttf`), and style dictionaries (`.xaml`).
* `Dtos/`: Contains the Data Transfer Objects (DTOs) used to structure the data retrieved from the PokeAPI.
* `MainWindow.xaml` and `MainWindow.xaml.cs`: The main window of the application, containing the UI layout and the code-behind logic for handling user interactions and data display.
* `App.xaml` and `App.xaml.cs`: The entry point of the application, where the main window is initialized.
* `PokemonSearchWPF.csproj`: The project file containing the project's configuration, including dependencies and file inclusions.

### `PokeApiNet`

A .NET library project for interacting with the PokeAPI.

* `Models/`: Contains the C# classes that represent the data structures of the PokeAPI.
* `PokeApiClient.cs`: The main client class responsible for making HTTP requests to the local API instance and deserializing the responses into the corresponding C# objects.

## Getting Started

### Prerequisites

* [Docker Desktop](https://www.docker.com/products/docker-desktop/) Required to host the local API instance.
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
* [Visual Studio 2026](https://visualstudio.microsoft.com/vs/) or another preferred IDE for .NET development.

### Installation & Setup

Because the public PokeAPI limits specific request types needed for the search feature, you must set up the backend first.

#### Step 1: Set up the Local PokeAPI

1. Clone the official PokeAPI repository:
```shell
git clone https://github.com/PokeAPI/pokeapi.git
```
2. Navigate to the cloned directory:
```shell
cd pokeapi
```
3. Start the API container:
```shell
docker compose up -d
docker compose exec -T app python manage.py migrate --settings=config.docker-compose
docker compose exec -T app sh -c 'echo "from data.v2.build import build_all; build_all()" | python manage.py shell --settings=config.docker-compose'
```
4. Ensure the API is running at `localhost/api/v2/` or `localhost/api/v2/pokemon/bulbasaur/` on port 80 (or the port specified in your docker config).  

#### Step 2: Set up the WPF Application

1. Clone this repository to your local machine:
```shell
git clone https://github.com/your-username/PokemonSearchWPF.git
```
2. Open the solution file `PokemonSearchWPF/PokemonSearchWPF.sln` in Visual Studio.
3. Restore the NuGet packages by right-clicking on the solution in the Solution Explorer and selecting "Restore NuGet Packages".

## Running the Application

1. Ensure your Docker container from Step 1 is running.
2. In Visual Studio, set `PokemonSearchWPF` as the startup project.
3. Press `F5` or click the "Start" button to build and run the application.

## Dependencies

The `PokemonSearchWPF` project relies on the `PokeApiNet` project within the same solution to fetch data.

## License

This project is licensed under the MIT License. See the [MIT License](https://opensource.org/license/mit) file for more details.
