# About Romulox

Romulox is a Single Page Application (SPA) for managing ROM files on a local computer or network.

Romulox uses an ASP.NET Core Web API with Entity Framework Core and SQLite for its backend and VueJS with Vuetify for its frontend.

Romulox supports identifying files by hash using a No-Intro dat file or by No-Intro naming conventions if an associated hash is not found or a dat file is not supplied.

Romulox also supports downloading information asynchronously from the GiantBomb API.

# Using the Romulox Source Code

## Installing Packages Using NPM
Using the terminal browse to `Romulox/client-app/` and execute `npm install`.

## Configuring The `appsettings.json` File
Create a [GiantBomb API Account](https://www.giantbomb.com/api/) and set the `GiantBombApiKey` entry to your API key.

## Creating and Updating the Database
After configuring your `appsettings.json` file you must update the database using the provided migrations. 

Using the terminal, browse to the `Romulox.DataAccess` project directory. 

Using the `dotnet` CLI run the following command:

`dotnet ef database update -s ../Romulox`

Now you are ready to build and run Romulox.

# Using the Romulox Web Interface

## Handling Platform Data

### Preparing the wwwroot directory

First create directories in the `Romulox/wwwroot/` directory to hold your ROM and `.dat` files. 

For example: you may want to create a `Platforms` directory with subdirectories for each specific platform such as `wwwroot/Platforms/Genesis` or `wwwroot/Roms/N64` which contain your ROM files. Be sure to copy over the ROM files that you wish to use to your chosen directories.

Next store any `.dat` files that you wish to use. You may put them in the same directory as the ROM files, such as `wwwroot/Roms/N64/N64.dat`, or in a different directory entirely such as `wwwroot/Dat/Gen.dat`. Be sure to copy the `.dat` files you wish to use into your chosen directories.

**Note:** You do not have to restart the application each time you copy over new files. 

Upon launch you will land at the platform listing page.

Here you can click the button labeled "Add a Platform" to add your first platform.

### Adding a Platform
  1. Enter your desired display name for the platform (e.g. Genesis, SNES, etc).
  2. Enter your desired display company for the platform (e.g. Sega, Nintendo, etc).
  3. Select the appropriate PlatformType abbreviation from the dropdown box (e.g. GEN for Genesis, SNES for Super Nintendo, etc)
  4. Enter the path to your platform data. This path **must** be in the `wwwroot` directory and **must not** contain `wwwroot` (e.g. `Platforms/Genesis` **_not_** `wwwroot/Platforms/Genesis`
  5. Enter the path to the desired No-Intro dat file if you wish to use one for naming. Be sure to **exclude** `wwwroot` in the path.
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

### Remove a Game
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
  
# Features Planned for Implmentation
  - [ ] Peristent Processing Queue for scheduling API calls.
  
  - [ ] Support for the [IGDB API](https://www.igdb.com/api).

