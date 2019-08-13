# About Romulox

Romulox is a Single Page Application (SPA) for locally managing ROM files.

Romulox uses an ASP.NET Core Web API with Entity Framework Core and SQLite for its backend and VueJS with Vuetify for its frontend.

Romulox supports identifying files by hash using a No-Intro, TOSEC, and Redump dat files or by No-Intro naming conventions if an associated hash is not found or an associated hash is not found in the supplied dat files.

Romulox also supports downloading information asynchronously from the GiantBomb API.

# First Look
![alt text](https://i.imgur.com/6wWHxdw.gif)

# Using the Romulox Source Code

This section covers prerequisites for using the Romulox source code with your editor or IDE of choice.

## Dependencies
  1. Download and install the [.NET Core SDK](https://dotnet.microsoft.com/download) from Microsoft.
  2. Download and install [npm](https://www.npmjs.com/get-npm).
  3. Download and install vue using npm by running `npm install vue` from the terminal.

## Installing Packages Using npm
Using the terminal browse to `Romulox/client-app/` and execute `npm install`.

## Creating the Database
  1. Using the terminal, browse to the `Romulox/Romulox.DataAccess` project directory. 
  2. Using the terminal run `dotnet ef database update -s ../Romulox` to create the `romulox.db` file in the `Romulox/Romulox` folder.
  
## Configuring The `appsettings.json` File
  1. Create a [GiantBomb API Account](https://www.giantbomb.com/api/) to get your API key.
  2. In the `Romulox/Romulox/` folder open the `appsettings.json` file and set the `GiantBombApiKey` entry to your API key.
  3. Save your changes to the `appsettings.json` file.

## Preparing the `wwwroot` Directory

Create directories for each platform that you wish to use ROMs with in the `wwwroot/Platforms` directory. 

Next store any `.dat` files that you wish to use into the `wwwroot/DatFiles` directory.

**Note:** You do not have to restart the application each time you copy over new files. 

You are now ready to use the Romulox source code with your editor or IDE of choice.

# Building for Production

This section will cover building the source for a runnable dotnet `.dll` file.

## Dependencies
  1. Download and install the [.NET Core SDK](https://dotnet.microsoft.com/download) from Microsoft.
  2. Download and install the [Node Package Manager (npm)](https://www.npmjs.com/get-npm).
  3. Download and install vue using npm by running `npm install vue` from the terminal.

## Publishing with `dotnet`
  1. Browse to the `Romulox/Romulox` project directory using the terminal.
  2. Run `dotnet publish -o Published`.
  3. The published project will be placed in the `Published` folder.
  
## Building with `vue`
  1. Browse to the `Romulox/Romulox/client-app/src` directory using the terminal.
  2. Run `vue build`.
  3. By default the built files will be placed in the `dist` folder.
  4. Move or copy and paste the `dist` folder into the `client-app` folder at `Romulox/Romulox/Published/client-app/`

## Creating the Database
  1. Using the terminal, browse to the `Romulox/Romulox.DataAccess` project directory. 
  2. Using the terminal run `dotnet ef database update -s ../Romulox`
  3. Copy the created `romulox.db` file into the `Romulox/Romulox/Published` folder.
  
## Configuring The `appsettings.json` File
  1. Create a [GiantBomb API Account](https://www.giantbomb.com/api/) to get your API key.
  2. In the `Romulox/Romulox/Published` folder open the `appsettings.json` file and set the `GiantBombApiKey` entry to your API key.
  3. Save your changes to the `appsettings.json` file.
  
## Preparing the `wwwroot` folder
  1. In the `Romulox/Romulox/Published/wwwroot` follow the directions from the [Preparing the wwwroot Directory](#preparing-the-wwwroot-directory) section.

## Running `Romulox.dll`
  1. Browse to the `Published` directory using the terminal.
  2. Run `dotnet Romulox.dll`
  3. Navigate your browser to `https://localhost:5001/` and enjoy using Romulox!
  
# Using the Romulox Web Interface

## Handling Platform Data

Upon launch you will land at the platform listing page.

Here you can click the button labeled "Add a Platform" to add your first platform.

### Adding a Platform
  1. Enter your desired display name for the platform (e.g. Genesis, SNES, etc).
  2. Enter your desired display company for the platform (e.g. Sega, Nintendo, etc).
  3. Select the appropriate PlatformType abbreviation from the dropdown box (e.g. GEN for Genesis, SNES for Super Nintendo, etc)
  4. Enter the path to your platform data. This path **must** be in the `wwwroot` directory and **must not** contain `wwwroot` (e.g. `Platforms/Genesis` **_not_** `wwwroot/Platforms/Genesis`
  6. Click Add Platform

**Note:** The GiantBomb API allows for 200 requests per resource per hour. If you have a large amount of games (~200 or more) in your platform directory you will begin to receive 403 errors and metadata will not be downloaded.
 
### Updating a Platform's Games
If you add new game files to your platform's directory you can update those particular games without re-doing the add platform process.

  1. Click the named platform link on the list page to browse the platform's details page.
  2. Click the button titled "Refresh Games".

### Removing a Platform
  1. Click the named platform link on the list page to browse the platform's details page.
  2. Click the button titled "Remove" and confirm your selection on the next page.
  
Note: This will not delete any files on the disk.

## Handling Game Data

### Editing a Game's Data
  1. From the platform page, click on the game you wish to edit.
  2. On the game page, click the button titled "Edit" and edit your desired details.

### Removing a Game
  1. From the platform page, click on the game you wish to remove.
  2. On the game page, click the button titled "Remove" and confirm your selection on the next page.
  
Note: This will not delete any files on the disk.
  
### Downloading a Game
  1. From the platform page, click on the game you wish to download.
  2. On the game page, click the button titled 'Download'.
  
# Features Currently Implemented
  - [x] Identifies files by hash using a No-Intro dat file.
  
  - [x] Identifies files using No-Intro naming conventions if an associated hash is not found or a dat file is not supplied.
  
  - [x] Populates game metadata asynchronously using the GiantBomb API.
  
  - [x] Saves game images for local retrieval.
