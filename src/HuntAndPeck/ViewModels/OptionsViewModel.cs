using HuntAndPeck.Properties;
using System;
using System.ComponentModel;
using System.Windows;

namespace HuntAndPeck.ViewModels
{
    internal class OptionsViewModel : INotifyPropertyChanged
    {
        public OptionsViewModel()
        {
            DisplayName = "Options";
            FontSize = Settings.Default.FontSize;
            HotKey = Settings.Default.HotKey;
            HotKeyAlt = Settings.Default.HotKeyAlt;
            HotKeyCtrl = Settings.Default.HotKeyCtrl;
            HotKeyShift = Settings.Default.HotKeyShift;
            Settings.Default.PropertyChanged += OnSettingsPropertyChanged;
        }

        public string DisplayName { get; set; }

        private string _fontSize;
        public string FontSize
        // Assign the font size value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    OnPropertyChanged("FontSize");
                    Settings.Default.FontSize = value;
                    Settings.Default.Save();
                }
            }
        }

        private string _hotKey;
        public string HotKey
        // Assign the hotkey value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _hotKey; }
            set
            {
                if (_hotKey != value)
                {
                    _hotKey = value;
                    OnPropertyChanged("HotKey");
                    Settings.Default.HotKey = value;
                    Settings.Default.Save();
                }
            }
        }

        private bool _hotKeyAlt;
        public bool HotKeyAlt
        // Assign the hotkey Alt value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _hotKeyAlt; }
            set
            {
                if (_hotKeyAlt != value)
                {
                    _hotKeyAlt = value;
                    OnPropertyChanged("HotKeyAlt");
                    Settings.Default.HotKeyAlt = value;
                    Settings.Default.Save();
                }
            }
        }

        private bool _hotKeyCtrl;
        public bool HotKeyCtrl
        // Assign the hotkey Ctrl value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _hotKeyCtrl; }
            set
            {
                if (_hotKeyCtrl != value)
                {
                    _hotKeyCtrl = value;
                    OnPropertyChanged("HotKeyCtrl");
                    Settings.Default.HotKeyCtrl = value;
                    Settings.Default.Save();
                }
            }
        }

        private bool _hotKeyShift;
        public bool HotKeyShift
        // Assign the hotkey Shift value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _hotKeyShift; }
            set
            {
                if (_hotKeyShift != value)
                {
                    _hotKeyShift = value;
                    OnPropertyChanged("HotKeyShift");
                    Settings.Default.HotKeyShift = value;
                    Settings.Default.Save();
                }
            }
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FontSize")
            {
                FontSize = Settings.Default.FontSize;
            }

            if (e.PropertyName == "HotKey")
            {
                HotKey = Settings.Default.HotKey;
            }

            if (e.PropertyName == "HotKeyAlt")
            {
                HotKeyAlt = Settings.Default.HotKeyAlt;
            }

            if (e.PropertyName == "HotKeyCtrl")
            {
                HotKeyCtrl = Settings.Default.HotKeyCtrl;
            }

            if (e.PropertyName == "HotKeyShift")
            {
                HotKeyShift = Settings.Default.HotKeyShift;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}