using System;
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
                Keys = Keys.T,
                Modifier = KeyModifier.Alt
            };

            keyListener1.HotKeyLevel1 = new HotKey
            {
                Keys = Keys.U,
                Modifier = KeyModifier.Alt
            };

            keyListener1.HotKeyLevel2 = new HotKey
            {
                Keys = Keys.I,
                Modifier = KeyModifier.Alt
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
            keyListener1.OnHotKeyLevel1Activated += _keyListener_OnHotKeyLevel1Activated;
            keyListener1.OnHotKeyLevel2Activated += _keyListener_OnHotKeyLevel2Activated;
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
            var key = ((HotKeyEventArgs)e).Key;
            if (session != null)
            {
                var vm = new OverlayViewModel(session, _hintLabelService);
                _showOverlay(vm);
            }
        }

        private void _keyListener_OnHotKeyLevel1Activated(object sender, EventArgs e)
        {
            var session = _hintProviderService.EnumHints(1);
            if (session != null)
            {
                var vm = new OverlayViewModel(session, _hintLabelService);
                _showOverlay(vm);
            }
        }

        private void _keyListener_OnHotKeyLevel2Activated(object sender, EventArgs e)
        {
            var session = _hintProviderService.EnumHints(2);
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
