# Introduction

This is a simple Pokedex App for displaying basic information on various Pokemon .

## IDE

This project was build using Visual Studio Community 2019.

## Code Explaination

This project was written using an MVVM type architecture, using Xamarin.Forms. Currently, it is developed primarily to run on the Android platform, as I do not have access to a Mac, for iOS Development.

### UI Design

Starting with the app icon, it is a simple Pokeball icon. The splash screen is a Pokeball inspire wallpaper.

Moving into the main view the Main view is written using Xamarin's XAML markup, with a codebase behind it. I decided to use this approach, as I did not want to create the view in code, which would have led to a large file, with view logic sprinkled throughout. I chose to make the UI for this app fairly simple, as I wanted to focus on getting the app logic working correctly, but did make a few small choice decisions, to keep it from being a completely flat design.

I chose to use a Collection view to handle the display of the Pokemon data that was loaded, as it allowed me to take advantage of paging/lazy loading, of Pokemon data. The individual cells of the list, were created using a Frame, to encapsulate the view, and give it a more rounded box appearance. When scrolling, I added a small loading indicator below the list, to give the user a visual indicator that more Pokemon data was being loaded, rather than having the user think no other data was available.

An Absoluate Layout was used to handle the popup for displaying additional Pokemon details, as it was easier for me to handle the popup animation used to display the popup more easily. The popup is a small view created using XAML, within the main XAML of the page. This was done to allow me to handle animations from the main views code behind for displaying and hiding the view.

Some small UI choices I chose to make, were adding an image at the top of the view, as it gives a small bit of visual diversity to the page. Another small design choice made, was to give a small rounded box to the Pokemon's type, when viewing it's details. This small box is also given a background color based on the Pokemon's type, as a way to help it standout when viewing Pokemon details.

### Pokemon API Client

To handle api calls to the PokeAPI, I created a PokeClient singleton class to encapsulate the functions needed for the app. I chose to use a singleton for this app, as opposed to something like an IoC container, as I did not feel the ioc container was necessary. This is due to me no needing the functionality of the ioc container, namely the ability to hot swap an object during runtime. As a result of this, I decided that a singleton would handle the needs of the app, as it allows the app to have access to api calls wherever it might be needed in the app.

Another choice I made in the app, was to create a function within the PokeClient class, using generics, to avoid retyping code, which only differed in return type. This simplified the design, and made for more compact code, as a smaller codebase was needed to write the Client object.

A similar approach was taken with handling the api responses in the PokeClient class. A Base response class was created using generics, which allowed me to handle responses containing a list of objects as part of the response. 

### Value Converters

A Single value converter was used in the app. This was done out of a need in the main view's XAML to hide data based on the value of a boolean.

### App Logic

Moving into the main app logic, I created a BaseViewModel class, to handle simple actions used throughout a few view models used in the code, such as updating properties, and handling the property changed events to update the view.

In the main view model for the app, the Pokemon data is stored in an ObservableCollection, of type PokemonModel. The PokemonModel class is used to store the data for the Pokemon we are interested in, such as: Name, ID, Height, Weight, Sprite, etc. These are stored in an ObservableCollection, as opposed to a List, as the ObservableCollection allows us to automatically update the UI without needing to explicitly call manual updates on the view. The Pokemon type filters are also stored in a ObservableCollection.

The CollectionView used to display Pokemon data, gives us access to a command we can bind to a command in our view model. This command was used to load more Pokemon data as the user scrolls through the list of Pokemon. As Pokemon Data is loaded, each set of loaded Pokemon data initiates another call to get more detailed information about the Pokemon, as the primarily call only gives the Pokemon name, and a url to get more info. The additional info is then loaded overtime, and populates the list. the most noticable is the sprite images being loaded in after the Pokemon names are displayed.

In order to handle the loading of additional Pokemon information, the PokemonModel class contains a function to load more Pokemon data, and can be called on those objects as the Pokemon data is being loaded. 

The type filter works by making a call for what Pokemon types are available, and then populating the picker control with the types. For the Pokemon types, I created a class to store the data for it, and overrode the comparison operators, to ensure that the comparisons between types would give the correct results. This allows me to avoid the issue of objects being compared by reference, rather than by value.

Displaying the filtered list of Pokemon was done by storing both the list of all Pokemon we have received from the PokeAPI, as well as a second list that stores only the Pokemon that fit the currently type filter. If the Pokemon filter is set to "All" then the full stored Pokemon list will be assigned to the filtered Pokemon list property. However, if the filter is set to any other value, then the app will loop through the list of currently stored Pokemon, and validate whether or not those Pokemon contain the selected type as one of their types. Once this loop iterates through each Pokemon, it will store the ones that contain the selected type, into the list of filtered Pokemon, which will then be displayed to the user.