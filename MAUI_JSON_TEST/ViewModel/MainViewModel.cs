using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MAUI_JSON_TEST.GitHubService;
using Pojo;
using System.Collections.ObjectModel;
using System.Windows.Input;
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
        private string _selectedSort;
        
        public MainViewModel()
        {
            SelectedSort = Preferences.Get("selected_sort",SortOptions.First());
            SearchText = Preferences.Get("search_text",string.Empty);
            isShowNoDataMessage = true;
            SearchCommand = new Command(async () => await SearchAsync());
        }
        
        public string SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;
                Preferences.Set("selected_sort", value);
                OnPropertyChanged(nameof(SelectedSort));
            }
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Preferences.Set("search_text",value);
                isSearchEnabled = !string.IsNullOrWhiteSpace(value) && value.All(c => c <= 127 && c != ' ');
            }
        }

        private bool _isSearchEnabled;
        public bool isSearchEnabled
        {
            get => _isSearchEnabled;
            set
            {
                if (_isSearchEnabled != value)
                {
                    _isSearchEnabled = value;
                    OnPropertyChanged(nameof(isSearchEnabled));
                }
            }
        }

        public ICommand SearchCommand { get; }
        private async Task SearchAsync()
        {
            IsLoading = true;
            if (IsLoading)
            {
                isShowNoDataMessage = false;
            }
            
            Repositories.Clear();
            try
            {
                var repos = await _service.SearchRepositories(SearchText, SelectedSort, new Progress<double>(value => LoadingProgress = value));
                foreach (var repo in repos)
                {
                    Repositories.Add(repo);
                }
            }
            catch
            {
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
                if (!IsLoading || Repositories.Count == 0)
                {
                    isShowNoDataMessage = true;
                }
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
            get=>_loadingProgress;
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

        public bool isShowNoDataMessage
        {
            get => _isShowNoDataMessage;
            set
            {
                if (_isShowNoDataMessage != value)
                {
                    _isShowNoDataMessage = value;
                    OnPropertyChanged(nameof(isShowNoDataMessage));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
