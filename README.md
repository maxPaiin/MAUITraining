A small .NET MAUI sample/test project that demonstrates basic UI, platform support, and integration scenarios for Android, iOS, macOS (Mac Catalyst), and Windows. This repository is intended for experimentation, learning, and automated tests of MAUI features.
Contents / Intent

    Lightweight sample app used for verifying .NET MAUI setup, building, and running on supported platforms.
    Minimal UI and example pages that exercise navigation, controls, and platform-specific behavior.
    Optionally contains unit/integration tests (if a test project is present).

Supported platforms

    Android
    iOS (device/simulator — requires a macOS build host and Xcode)
    macOS (Mac Catalyst)
    Windows

Prerequisites

    .NET SDK (recommended: current supported .NET version that includes MAUI; install the latest supported SDK)
    .NET MAUI workloads:
        Install via: dotnet workload install maui
    Platform-specific tooling:
        Android: Android SDK & emulator or device (Android SDK, Android SDK Command-line Tools, and an emulator)
        iOS/macOS: Xcode (macOS only)
        Windows: Visual Studio with MAUI support or the appropriate MSVC toolchain
    Recommended IDEs:
        Visual Studio 2022/2023 (Windows) with Mobile development and .NET MAUI workload
        Visual Studio for Mac (macOS)
        Or use the .NET CLI for command-line builds

For full setup instructions and troubleshooting, see the official docs: https://learn.microsoft.com/dotnet/maui
Quick start — using Visual Studio (recommended)

    Open the solution (*.sln) in Visual Studio.
    Restore packages automatically (Visual Studio does this on load).
    Select a target platform (Android emulator, iOS simulator, macOS, or Windows).
    Build and run (F5) or deploy to a device/emulator.

Quick start — using the CLI

    Restore dependencies:
        dotnet restore
    Build:
        dotnet build -c Debug
    To run or deploy, use Visual Studio or platform-specific publish/launch commands. Example publish (Android):
        dotnet publish -f net.0-android -c Release -o ./publish/android Replace <n> with the target .NET major version used by this project (e.g., net7.0 or net8.0) as configured in the project files.

Note: dotnet run is not always supported for all MAUI targets via the CLI; Visual Studio provides the easiest deploy experience.
Running tests

If the repository includes test projects:

    Run unit tests with:
        dotnet test
    Or open the test project in Visual Studio and run tests via Test Explorer.

Project structure (typical)

    src/ or MAUITraining/ — main MAUI app (App.xaml, MainPage.xaml, platform folders)
    tests/ — unit and integration tests (if present)
    README.md — this file

Adjust directories above to match this repo's layout.
Common issues & troubleshooting

    "Missing workload" errors: run dotnet workload install maui.
    Android emulator or device not found: ensure Android SDK and platform tools are installed, and an emulator is running (or a device is connected and authorized).
    iOS/macOS build errors: builds for Apple platforms require Xcode and a macOS environment.
    NuGet package restore failures: verify network access and run dotnet restore --no-cache if needed.

Contributing

    Create issues for bugs or feature requests.
    Open pull requests for fixes or improvements. Keep changes small and include a short description of what and why.
