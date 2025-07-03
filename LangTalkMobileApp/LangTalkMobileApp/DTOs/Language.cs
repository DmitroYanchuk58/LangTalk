using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LangTalkMobileApp.DTOs
{
    public class Language : INotifyPropertyChanged
    {
        private bool _isSelected;
        private bool _isSpoken;
        private string _level = "";

        [JsonPropertyName("languageName")]
        public string LanguageName { get; set; } = null!;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanSelectIsSpoken));
                    OnPropertyChanged(nameof(CanEditLevel));
                }
            }
        }

        [JsonPropertyName("isSpoken")]
        public bool IsSpoken
        {
            get => _isSpoken;
            set
            {
                if (_isSpoken != value)
                {
                    _isSpoken = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanEditLevel));
                }
            }
        }

        [JsonPropertyName("level")]
        public string Level
        {
            get => _level;
            set
            {
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged();
                }
            }
        }

        // Logic properties
        [JsonIgnore]
        public bool CanSelectIsSpoken => IsSelected;

        [JsonIgnore]
        public bool CanEditLevel => IsSelected && !IsSpoken;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        [JsonIgnore]
        public ObservableCollection<string> Levels { get; } = new ObservableCollection<string>()
        {
            "A1", "A2", "B1", "B2", "C1", "C2"
        };

        private bool isChecked;

        [JsonIgnore]
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }
    }
}
