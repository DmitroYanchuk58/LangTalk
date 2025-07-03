using LangTalkMobileApp.DTOs;
using LangTalkMobileApp.Token;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Dispatching;

namespace LangTalkMobileApp.MVVM.ViewModel
{
    public partial class SortPeopleParametrsViewModel : ObservableObject
    {
        private readonly TokenService _tokenService;

        public SortPeopleParametrsViewModel(TokenService tokenService)
        {
            _tokenService = tokenService;
            LoadAllDataAsync();
        }

        [ObservableProperty]
        private ObservableCollection<Language> languages;

        [ObservableProperty]
        private ObservableCollection<Hobby> hobbies;

        [ObservableProperty]
        private ObservableCollection<MbtiType> mbtiTypes;

        [ObservableProperty]
        private ObservableCollection<Country> countries;

        [ObservableProperty]
        private ObservableCollection<Gender> genders;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string errorMessage;

        private async void LoadAllDataAsync()
        {
            IsBusy = true;
            ErrorMessage = null;

            try
            {
                var countriesTask = HttpCommands.Command.GetAllCountries();
                var mbtiTypesTask = HttpCommands.Command.GetAllMbtiTypes();
                var hobbiesTask = HttpCommands.Command.GetAllHobbies();
                var languagesTask = HttpCommands.Command.GetAllLanguages();
                var gendersTask = HttpCommands.Command.GetAllGenders();

                await Task.WhenAll(countriesTask, mbtiTypesTask, hobbiesTask, languagesTask, gendersTask);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Countries = new ObservableCollection<Country>(
                        countriesTask.Result.Select(name => new Country { Name = name, IsChecked = false }));

                    MbtiTypes = new ObservableCollection<MbtiType>(
                        mbtiTypesTask.Result.Select(type => new MbtiType { Type = type.Type, IsChecked = false }));

                    Hobbies = new ObservableCollection<Hobby>(
                        hobbiesTask.Result.GetRange(0,15).Select(name => new Hobby { Name = name, IsChecked = false }));

                    Languages = new ObservableCollection<Language>(
                        languagesTask.Result.Select(name => new Language { LanguageName = name, IsChecked = false }));

                    Genders = new ObservableCollection<Gender>(
                        gendersTask.Result.Select(name => new Gender { Name = name, IsChecked = false }));
                });
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load data: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ICommand SearchPeopleCommand => new Command(async () =>
        {
            IsBusy = true;
            ErrorMessage = null;

            try
            {
                var idUser = _tokenService.IdUser;
                var sortParameters = CreateSortParametersDto();
                await ChangeViewCommand.ChangeViewCommand.ChangeOnSelectPeopleView(sortParameters);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Search failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        });

        private SortPeopleParameters CreateSortParametersDto()
        {
            return new SortPeopleParameters
            {
                SelectedCountries = Countries?.Where(c => c.IsChecked).Select(c => c.Name).ToList(),
                SelectedMbtiTypes = MbtiTypes?.Where(m => m.IsChecked).Select(m => m.Type).ToList(),
                SelectedHobbies = Hobbies?.Where(h => h.IsChecked).Select(h => h.Name).ToList(),
                SelectedLanguages = Languages?.Where(l => l.IsChecked).Select(l => l.LanguageName).ToList(),
                SelectedGenders = Genders?.Where(g => g.IsChecked).Select(g => g.Name).ToList()
            };
        }
    }
}