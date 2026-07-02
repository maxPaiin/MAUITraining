using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using MAUI_JSON_TEST.GitHubService;
using Pojo;
using Microsoft.Maui.Storage;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace MAUI_JSON_TEST.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly Service _service = new();
        public ObservableCollection<Respond> Repositories { get; } = new();
        public List<string> SortOptions { get; } = new() { "stars", "forks", "help-wanted-issues", "updated" };

        public MainViewModel()
        {
            _selectedSort = Preferences.Get("selected_sort", SortOptions.First());
            SearchText = Preferences.Get("search_text", string.Empty);
            IsShowNoDataMessage = true;
            SearchCommand = new Command(async () => await SearchAsync());
        }

        private string _selectedSort;
        public string SelectedSort
        {
            get => _selectedSort;
            set
            {
                if (_selectedSort == value)
                {
                    return;
                }
                _selectedSort = value;
                Preferences.Set("selected_sort", value);
                OnPropertyChanged(nameof(SelectedSort));
            }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value)
                {
                    return;
                }
                _searchText = value;
                Preferences.Set("search_text", value);
                OnPropertyChanged(nameof(SearchText));
                IsSearchEnabled = !string.IsNullOrWhiteSpace(value) && value.All(c => c <= 127 && c != ' ');
            }
        }

        private bool _isSearchEnabled;
        public bool IsSearchEnabled
        {
            get => _isSearchEnabled;
            set
            {
                if (_isSearchEnabled != value)
                {
                    _isSearchEnabled = value;
                    OnPropertyChanged(nameof(IsSearchEnabled));
                }
            }
        }

        public ICommand SearchCommand { get; }
        private async Task SearchAsync()
        {
            IsLoading = true;
            IsShowNoDataMessage = false;

            Repositories.Clear();
            try
            {
                var repos = await _service.SearchRepositories(SearchText, SelectedSort, new Progress<double>(value => LoadingProgress = value));
                foreach (var repo in repos)
                {
                    Repositories.Add(repo);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Search failed: {ex}");
                var snackbar = Snackbar.Make(
                    "can't connect try again",
                    async () => await SearchAsync(),
                    "Retry",
                    TimeSpan.FromSeconds(10));
                await snackbar.Show();
            }
            finally
            {
                IsLoading = false;
                IsShowNoDataMessage = Repositories.Count == 0;
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private double _loadingProgress;

        public double LoadingProgress
        {
            get => _loadingProgress;
            set
            {
                if (_loadingProgress != value)
                {
                    _loadingProgress = value;
                    OnPropertyChanged(nameof(LoadingProgress));
                }
            }
        }

        private bool _isShowNoDataMessage;

        public bool IsShowNoDataMessage
        {
            get => _isShowNoDataMessage;
            set
            {
                if (_isShowNoDataMessage != value)
                {
                    _isShowNoDataMessage = value;
                    OnPropertyChanged(nameof(IsShowNoDataMessage));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
