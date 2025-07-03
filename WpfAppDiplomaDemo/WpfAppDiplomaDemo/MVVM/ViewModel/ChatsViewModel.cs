using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.Core;
using WpfAppDiplomaDemo.MVVM.Model;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class ChatsViewModel : ObservableObject
    {
        private ChatsModel _model;
        public MainViewModel MainVM { get; set; }

        public BottomMenuViewModel BottomMenuVM => new BottomMenuViewModel(MainVM);

        public ChatsViewModel(ChatsModel model, MainViewModel mainViewModel)
        {
            _model = model;
            SmallChats = new ObservableCollection<SmallChatViewModel>(
                _model.SmallChats?.Select(m => new SmallChatViewModel(mainViewModel, m)) ?? new List<SmallChatViewModel>()
            );
            MainVM = mainViewModel;
        }

        private ObservableCollection<SmallChatViewModel> _smallChats = new ObservableCollection<SmallChatViewModel>();
        public ObservableCollection<SmallChatViewModel> SmallChats
        {
            get => _smallChats;
            set
            {
                if (_smallChats != value)
                {
                    _smallChats = value;
                    OnPropertyChanged(nameof(SmallChats));
                }
            }
        }
    }
}
