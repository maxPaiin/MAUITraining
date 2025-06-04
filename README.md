5.16.2025 update</br>
I’ve made some UI adjustments and added a Snackbar that appears when there’s a network communication issue.

---------------------------------------------------------
A simple .NET MAUI demo app that uses REST APIs and follows the MVVM architecture.  
Built for practice and to explore .NET MAUI capabilities.

Built with Rider on macOS (ARM) using .NET MAUI SDK 8.0.408</br>
Developed on: May 8, 2025</br>

Project Structure</br>

This project is built using the MVVM architecture pattern. Below is the structure and role of each main folder:

MAUITraining/</br>
├── Models/           # Data models (Repository.cs)</br>
├── ViewModels/       # ViewModels responsible for data binding and logic (MainViewModel.cs)</br>
├── Views/            # UI pages (MainPage.xaml) with bindings to ViewModels</br>
├── Services/         # API communication using HttpClient (GitHubApiService.cs)</br>
├── App.xaml.cs       # App lifecycle entry</br>
└── MauiProgram.cs    # DI container and service registration</br>
