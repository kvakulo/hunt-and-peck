using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HuntAndPeck.NativeMethods;
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

            keyListener1.HotKey = new HotKey
            {
                Keys = Keys.U,
                Modifier = KeyModifier.Alt
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

            keyListener1.TaskbarHotKey = new HotKey
            {
                Keys = Keys.Y,
                Modifier = KeyModifier.Control
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
