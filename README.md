# MAUI_JSON_TEST — GitHub Repository Search

A .NET MAUI sample app that searches GitHub repositories and renders the results in a GitHub-dark-themed list. Built as a training project for exploring MAUI fundamentals: MVVM data binding, calling a REST API, JSON deserialization with `System.Text.Json`, and app-wide theming with XAML resource dictionaries.

## What it does

- Search GitHub repositories by keyword via the [GitHub Search API](https://docs.github.com/rest/search/search#search-repositories) (no authentication; subject to the unauthenticated rate limit of 10 search requests/minute).
- Sort results by `stars`, `forks`, `help-wanted-issues`, or `updated` (top 10 results shown).
- Tap a result to open the repository in the system browser.
- Search keyword and sort choice persist across app restarts (`Preferences`).
- Download progress bar while fetching; Snackbar with a Retry action on network failure.
- GitHub Dark UI throughout, using the [Primer](https://primer.style) color palette (`#0d1117` canvas, `#161b22` cards, `#58a6ff` accent, `#238636` buttons). The app forces dark mode regardless of system theme.

## Project structure

```
MAUI_JSON_TEST/
├── MainPage.xaml(.cs)          # Single page: search bar, sort picker, results list
├── ViewModel/
│   └── MainViewModel.cs        # MVVM view model (INotifyPropertyChanged, ICommand)
├── Model/
│   ├── GitHubService/
│   │   └── Service.cs          # HttpClient call to the GitHub Search API
│   └── Pojo/
│       ├── Respond.cs          # Repository item DTO ([JsonPropertyName] mapping)
│       └── Owner.cs            # Repository owner DTO
├── Resources/Styles/
│   ├── Colors.xaml             # Color palette incl. GitHub Dark (Primer) colors
│   └── Styles.xaml             # Implicit control styles (dark values use the palette)
└── Platforms/                  # Android / iOS / Mac Catalyst / Windows bootstrap
```

There is no test project.

## Tech stack

- .NET 8 (SDK `8.0.408` pinned in `global.json`)
- .NET MAUI, single-project targeting `net8.0-android`, `net8.0-ios`, `net8.0-maccatalyst`, and `net8.0-windows10.0.19041.0` (Windows target only when building on Windows)
- [CommunityToolkit.Maui](https://github.com/CommunityToolkit/Maui) — Snackbar
- `System.Text.Json` for deserialization

## Getting started

### Prerequisites

- .NET SDK 8.0.4xx
- MAUI workloads: `dotnet workload install maui`
- Platform tooling:
  - **Android** — Android SDK plus an emulator or device
  - **iOS / Mac Catalyst** — macOS with full Xcode installed (Command Line Tools alone are not enough; check `xcode-select -p`)
  - **Windows** — Visual Studio 2022 with the .NET MAUI workload

### Build & run

```bash
dotnet restore

# Android
dotnet build MAUI_JSON_TEST/MAUI_JSON_TEST.csproj -f net8.0-android
dotnet build MAUI_JSON_TEST/MAUI_JSON_TEST.csproj -f net8.0-android -t:Run   # deploy to a running emulator/device

# Mac Catalyst (requires Xcode)
dotnet build MAUI_JSON_TEST/MAUI_JSON_TEST.csproj -f net8.0-maccatalyst -t:Run

# Windows (on Windows)
dotnet build MAUI_JSON_TEST/MAUI_JSON_TEST.csproj -f net8.0-windows10.0.19041.0 -t:Run
```

Or open `MAUI_JSON_TEST.sln` in Visual Studio / Rider, pick a target, and run.

## Known limitations

- The Search button is disabled for queries containing spaces or non-ASCII characters, so multi-word and CJK searches are not possible.
- The search query is not URL-encoded before being appended to the request URL; characters like `&` or `#` will break the query.
- The progress bar only advances when the API response includes a `Content-Length` header; GitHub often streams chunked responses, in which case it stays at 0 until completion.
- Results are fixed at 10 per search with no paging.

## Troubleshooting

- `Missing workload` errors → `dotnet workload install maui`
- Apple targets fail with *"Could not find a valid Xcode app bundle"* → install full Xcode and run `sudo xcode-select -s /Applications/Xcode.app`
- No results plus a Retry Snackbar right after several searches → you likely hit the unauthenticated GitHub rate limit; wait a minute and retry
