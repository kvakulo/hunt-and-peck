using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using HuntAndPeck.NativeMethods;
using HuntAndPeck.Properties;
using HuntAndPeck.Services.Interfaces;
using Application = System.Windows.Application;

namespace HuntAndPeck.ViewModels
{
    internal class ShellViewModel
    {
        private readonly Action<OverlayViewModel> _showOverlay;
        private readonly Action<DebugOverlayViewModel> _showDebugOverlay;
        private readonly Action<OptionsViewModel> _showOptions;
        private readonly IHintLabelService _hintLabelService;
        private readonly IHintProviderService _hintProviderService;
        private readonly IDebugHintProviderService _debugHintProviderService;

        public ShellViewModel(
            Action<OverlayViewModel> showOverlay,
            Action<DebugOverlayViewModel> showDebugOverlay,
            Action<OptionsViewModel> showOptions,
            IHintLabelService hintLabelService,
            IHintProviderService hintProviderService,
            IDebugHintProviderService debugHintProviderService,
            IKeyListenerService keyListener)
        {
            _showOverlay = showOverlay;
            _showDebugOverlay = showDebugOverlay;
            _showOptions = showOptions;
            _hintLabelService = hintLabelService;
            var keyListener1 = keyListener;
            _hintProviderService = hintProviderService;
            _debugHintProviderService = debugHintProviderService;
            int keyValue = 0x20;
            string hotKeyString = Settings.Default.HotKey.StartsWith("0x") ? Settings.Default.HotKey.Substring(2) : Settings.Default.HotKey;
            int.TryParse(hotKeyString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out keyValue);
            keyListener1.HotKey = new HotKey
            {
                Keys = (Keys)keyValue,
                Modifier = GetKeyModifier(Settings.Default.HotKeyAlt, Settings.Default.HotKeyCtrl, Settings.Default.HotKeyShift, Settings.Default.HotKeyWin)
            };

            keyListener1.LeveledHotKeys = new List<HotKey>
            {
                new HotKey
                {
                    Keys = Keys.Q,
                    Modifier = KeyModifier.Alt,
                    Level = 1
                },
                new HotKey
                {
                    Keys = Keys.W,
                    Modifier = KeyModifier.Alt,
                    Level = 2
                },
                new HotKey
                {
                    Keys = Keys.E,
                    Modifier = KeyModifier.Alt,
                    Level = 3
                },
                new HotKey
                {
                    Keys = Keys.R,
                    Modifier = KeyModifier.Alt,
                    Level = 4
                },
                new HotKey
                {
                    Keys = Keys.T,
                    Modifier = KeyModifier.Alt,
                    Level = 5
                },
                new HotKey
                {
                    Keys = Keys.Y,
                    Modifier = KeyModifier.Alt,
                    Level = 6
                }

            };

            string taskbarHotKeyString = Settings.Default.TaskbarHotKey.StartsWith("0x") ? Settings.Default.TaskbarHotKey.Substring(2) : Settings.Default.TaskbarHotKey;
            int.TryParse(taskbarHotKeyString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out keyValue);
            keyListener1.TaskbarHotKey = new HotKey
            {
                Keys = (Keys)keyValue,
                Modifier = GetKeyModifier(Settings.Default.TaskbarHotKeyAlt, Settings.Default.TaskbarHotKeyCtrl, Settings.Default.TaskbarHotKeyShift, Settings.Default.TaskbarHotKeyWin)
            };

#if DEBUG
            keyListener1.DebugHotKey = new HotKey
            {
                Keys = Keys.Y,
                Modifier = KeyModifier.Alt | KeyModifier.Shift
            };
#endif
            keyListener1.OnHotKeyActivated += _keyListener_OnHotKeyActivated;
            keyListener1.OnHotKeyLeveledActivated += _keyListener_OnHotKeyLeveledActivated;
            keyListener1.OnTaskbarHotKeyActivated += _keyListener_OnTaskbarHotKeyActivated;
            keyListener1.OnDebugHotKeyActivated += _keyListener_OnDebugHotKeyActivated;

            ShowOptionsCommand = new DelegateCommand(ShowOptions);
            ExitCommand = new DelegateCommand(Exit);
        }
        private KeyModifier GetKeyModifier(bool alt, bool ctrl, bool shift, bool windows = false)
        {
            KeyModifier modifier = 0;

        private KeyModifier GetKeyModifier(bool alt, bool ctrl, bool shift, bool windows = false)
        {
            KeyModifier modifier = 0;

            if (alt)
                modifier |= KeyModifier.Alt;

            if (ctrl)
                modifier |= KeyModifier.Control;

            if (shift)
                modifier |= KeyModifier.Shift;

            if (windows)
                modifier |= KeyModifier.Windows;

            return modifier;
        }
        public DelegateCommand ShowOptionsCommand { get; }
        public DelegateCommand ExitCommand { get; }

        private void _keyListener_OnHotKeyActivated(object sender, EventArgs e)
        {
            var session = _hintProviderService.EnumHints();
            if (session != null)
            {
                var vm = new OverlayViewModel(session, _hintLabelService);
                _showOverlay(vm);
            }
        }

        private void _keyListener_OnHotKeyLeveledActivated(object sender, EventArgs e)
        {
            var session = _hintProviderService.EnumHints(((HotKeyEventArgs)e).Level);
            if (session != null)
            {
                var vm = new OverlayViewModel(session, _hintLabelService);
                _showOverlay(vm);
            }
        }

        private void _keyListener_OnTaskbarHotKeyActivated(object sender, EventArgs e)
        {
            var taskbarHWnd = User32.FindWindow("Shell_traywnd", "");
            var session = _hintProviderService.EnumHints(taskbarHWnd, -1);
            if (session != null)
            {
                var vm = new OverlayViewModel(session, _hintLabelService);
                _showOverlay(vm);
            }
        }

        private void _keyListener_OnDebugHotKeyActivated(object sender, EventArgs e)
        {
            var session = _debugHintProviderService.EnumDebugHints();
            if (session != null)
            {
                var vm = new DebugOverlayViewModel(session);
                _showDebugOverlay(vm);
            }
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void ShowOptions()
        {
            var vm = new OptionsViewModel();
            _showOptions(vm);
        }
    }
}
}
