# Unity Mobile Application for use with Shimmer devices
#### Developers: Justin Servis, David Gallagher.

## Introduction
A Unity application primarily designed for mobile devices which can connect to and stream data from a Shimmer IMU (Inertial Measurement Unit).  While the Shimmer IMU was the only IMU available to us at the time of development, the application is structured in such a way that it should be possible to use data from any IMU, although data transformations may need to be performed to achieve this, e.g. unit conversion.

### Cross platform

Primarily developed for use with Android devices, this application can also be run on Windows. Cross platform operation is provided through the use of two separate code libraries specifically developed for this purpose. The application detects the current operating system and loads one of these two dependences at run time. This functionality is achieved through the use of a set of interfaces which define the functionality provided by each library.

#### Plugins

[Android Bluetooth Plugin for Unity](https://github.com/SerjiVutinss/unity-android-bluetooth-plugin)  
* Java plugin for Unity to allow native Android Bluetooth functionality through the Java Native Interface (JNI)
* This plugin allows for native Android calls to be performed in Unity, functionalty not included in Unity itself, e.g. Toast notifictions, Enabling/Disabling device sensors, pairing Bluetooth devices, etc.

[Windows Shimmer RT Plugin](https://github.com/SerjiVutinss/ShimmerRT_Library)
* Shared library which acts as a thin wrapper over the Shimmer C# library provided in the Shimmer Development software suite.
* Used mainly for development in Unity in Windows.

### Functionality

The main functions of the application are:

* Connect to and live stream data from an IMU and visualise this within the application with the intent of making it easy for a user to interpret the data.
* Associate recorded data with a Player/User profile and store this data in a remote database.
* Retrieve any previously stored data from the remote database.
* Playback of recorded or loaded data and visualise in the same way as the live streamed data.
* Click below to view the demonstration recording (youtube link)


[![Recorded Demo Aailable Here](https://img.youtube.com/vi/umR1cIud7es/0.jpg)](https://www.youtube.com/watch?v=umR1cIud7es)


### List of related repositories:

[Blazor Web Application](https://github.com/SerjiVutinss/BlazorSportsDataWebAsm)  
* C# Blazor Web application comprised of a Blazor PWA frontend and a Web API backend using Entity Framework.
* Uses SQL Server as its backend database.
* Currently hosted on Azure web services.
* Accessible from both the Unity and Xamarin applications.
* Frontend is minimal and generally only used for testing, since the CRUD functionality is intended to be performed from the Xamarin Application.


[Xamarin Application](https://github.com/d-gallagher/XamarinFirebaseProject)  
* Auxiliary application to simplify the input of player details without relying on Unity UI to handle this.
* Can be built for both Windows and Android if needed.
* Allows for CRUD operations to and from the Blazor Web Application. 

[Initial Discovery work on Xamarin and Firebase](https://github.com/d-gallagher/XamarinFirebase)
* Preliminary discovery of the viability of using Firebase as a data store for the recorded data.
* Switched over to the Blazor Web API and SQL Server due to the volume of data being transferred.

[Initial Discovery work on Unity and Firebase](https://github.com/d-gallagher/UnityAndroidFirebase)  
* Preliminary discovery of the viability of using Firebase as a data store for the recorded data.
