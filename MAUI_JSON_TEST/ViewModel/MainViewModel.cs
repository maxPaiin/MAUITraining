using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MAUI_JSON_TEST.GitHubService;
using Pojo;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Storage;

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
            // SelectedSort = SortOptions.First();
            SelectedSort = Preferences.Get("selected_sort",SortOptions.First());
            SearchText = Preferences.Get("search_text",string.Empty);
            SearchCommand = new Command(async () => await SearchAsync());
            // if (string.IsNullOrWhiteSpace(SearchText)) return; 
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
                // OnPropertyChanged(nameof(SearchText));
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
            var repos = await _service.SearchRepositoriesAsync(SearchText, SelectedSort);
            Repositories.Clear();
            foreach (var repo in repos)
            {
                Repositories.Add(repo);
            }
            // Console.WriteLine($"keyword: {SearchText}, sort: {SelectedSort}");
            // Console.WriteLine($"respond count: {repos.Count}");
        }
        
        private bool _isTouch;
        public bool isTouch
        {
            get => _isTouch;
            set
            {
                if (_isTouch != value)
                {
                    _isTouch = value;
                    OnPropertyChanged(nameof(isTouch));
                    OnPropertyChanged(nameof(BorderColor));
                }
            }
        }

         public Color BorderColor => isTouch ? Colors.Blue : Colors.LightGray;
         
         // public Color BorderColor
         // {
         //     get
         //     {
         //         if (isTouch) 
         //         {
         //             return Colors.Blue;
         //         }else
         //         {
         //             return Colors.LightGray;
         //         }
         //     }
         // }

         public ICommand BordTouch => new Command(()=> isTouch = true );
         // public ICommand BordRelease => new Command(() => isTouch = false );
         
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
