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
            HotKeyWin = Settings.Default.HotKeyWin;
            TaskbarHotKey = Settings.Default.TaskbarHotKey;
            TaskbarHotKeyAlt = Settings.Default.TaskbarHotKeyAlt;
            TaskbarHotKeyCtrl = Settings.Default.TaskbarHotKeyCtrl;
            TaskbarHotKeyShift = Settings.Default.TaskbarHotKeyShift;
            TaskbarHotKeyWin = Settings.Default.TaskbarHotKeyWin;
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

        private bool _hotKeyWin;
        public bool HotKeyWin
        // Assign the hotkey Win value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _hotKeyWin; }
            set
            {
                if (_hotKeyWin != value)
                {
                    _hotKeyWin = value;
                    OnPropertyChanged("HotKeyWin");
                    Settings.Default.HotKeyWin = value;
                    Settings.Default.Save();
                }
            }
        }

        private string _taskbarHotKey;
        public string TaskbarHotKey
        // Assign the hotkey value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _taskbarHotKey; }
            set
            {
                if (_taskbarHotKey != value)
                {
                    _taskbarHotKey = value;
                    OnPropertyChanged("TaskbarHotKey");
                    Settings.Default.TaskbarHotKey = value;
                    Settings.Default.Save();
                }
            }
        }

        private bool _taskbarHotKeyAlt;
        public bool TaskbarHotKeyAlt
        // Assign the hotkey Alt value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _taskbarHotKeyAlt; }
            set
            {
                if (_taskbarHotKeyAlt != value)
                {
                    _taskbarHotKeyAlt = value;
                    OnPropertyChanged("TaskbarHotKeyAlt");
                    Settings.Default.TaskbarHotKeyAlt = value;
                    Settings.Default.Save();
                }
            }
        }

        private bool _taskbarHotKeyCtrl;
        public bool TaskbarHotKeyCtrl
        // Assign the hotkey Ctrl value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _taskbarHotKeyCtrl; }
            set
            {
                if (_taskbarHotKeyCtrl != value)
                {
                    _taskbarHotKeyCtrl = value;
                    OnPropertyChanged("TaskbarHotKeyCtrl");
                    Settings.Default.TaskbarHotKeyCtrl = value;
                    Settings.Default.Save();
                }
            }
        }

        private bool _taskbarHotKeyShift;
        public bool TaskbarHotKeyShift
        // Assign the hotkey Shift value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _taskbarHotKeyShift; }
            set
            {
                if (_taskbarHotKeyShift != value)
                {
                    _taskbarHotKeyShift = value;
                    OnPropertyChanged("TaskbarHotKeyShift");
                    Settings.Default.TaskbarHotKeyShift = value;
                    Settings.Default.Save();
                }
            }
        }

        private bool _taskbarHotKeyWin;
        public bool TaskbarHotKeyWin
        // Assign the hotkey Win value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _taskbarHotKeyWin; }
            set
            {
                if (_taskbarHotKeyWin != value)
                {
                    _taskbarHotKeyWin = value;
                    OnPropertyChanged("TaskbarHotKeyWin");
                    Settings.Default.TaskbarHotKeyWin = value;
                    Settings.Default.Save();
                }
            }
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FontSize":
                    FontSize = Settings.Default.FontSize;
                    break;
                case "HotKey":
                    HotKey = Settings.Default.HotKey;
                    break;
                case "HotKeyAlt":
                    HotKeyAlt = Settings.Default.HotKeyAlt;
                    break;
                case "HotKeyCtrl":
                    HotKeyCtrl = Settings.Default.HotKeyCtrl;
                    break;
                case "HotKeyShift":
                    HotKeyShift = Settings.Default.HotKeyShift;
                    break;
                case "HotKeyWin":
                    HotKeyWin = Settings.Default.HotKeyWin;
                    break;
                case "TaskbarHotKey":
                    TaskbarHotKey = Settings.Default.TaskbarHotKey;
                    break;
                case "TaskbarHotKeyAlt":
                    TaskbarHotKeyAlt = Settings.Default.TaskbarHotKeyAlt;
                    break;
                case "TaskbarHotKeyCtrl":
                    TaskbarHotKeyCtrl = Settings.Default.TaskbarHotKeyCtrl;
                    break;
                case "TaskbarHotKeyShift":
                    TaskbarHotKeyShift = Settings.Default.TaskbarHotKeyShift;
                    break;
                case "TaskbarHotKeyWin":
                    TaskbarHotKeyWin = Settings.Default.TaskbarHotKeyWin;
                    break;
                default:
                    break;
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