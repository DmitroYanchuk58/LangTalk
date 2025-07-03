using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfAppDiplomaDemo.MVVM.Model;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class MessageViewModel : INotifyPropertyChanged
     {
        private MessageModel _model;

        public MessageViewModel(MessageModel model)
        {
            _model = model;
        }

        public Guid IdMessage
        {
            get => _model.IdMessage;
            set
            {
                if (_model.IdMessage != value)
                {
                    _model.IdMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Text
        {
            get => _model.Text;
            set
            {
                if (_model.Text != value)
                {
                    _model.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime TimeSend
        {
            get => _model.TimeSend;
            set
            {
                if (_model.TimeSend != value)
                {
                    _model.TimeSend = value;
                    OnPropertyChanged();
                }
            }
        }

        public Guid IdUser
        {
            get => _model.IdUser;
            set
            {
                if (_model.IdUser != value)
                {
                    _model.IdUser = value;
                    OnPropertyChanged();
                }
            }
        }

        public string UserName
        {
            get => _model.UserName;
            set
            {
                if (_model.UserName != value)
                {
                    _model.UserName = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
