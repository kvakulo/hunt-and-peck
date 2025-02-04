using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using HuntAndPeck.Models;
using HuntAndPeck.Services.Interfaces;
using System.Windows.Controls;

namespace HuntAndPeck.ViewModels
{
    internal class OverlayViewModel : NotifyPropertyChanged
    {
        private Rect _bounds;
        private ObservableCollection<HintViewModel> _hints = new ObservableCollection<HintViewModel>();
        private UiAutomationScrollHint _waitingForDecile;

        public OverlayViewModel(
            HintSession session,
            IHintLabelService hintLabelService)
        {
            _bounds = session.OwningWindowBounds;

            var labels = hintLabelService.GetHintStrings(session.Hints.Count());
            for (int i = 0; i < labels.Count; ++i)
            {
                var hint = session.Hints[i];
                _hints.Add(new HintViewModel(hint)
                {
                    Label = labels[i],
                    Active = false
                });
            }
        }

        /// <summary>
        /// Bounds in logical screen coordiantes
        /// </summary>
        public Rect Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
                NotifyOfPropertyChange();
            }
        }

        public ObservableCollection<HintViewModel> Hints
        {
            get
            {
                return _hints;
            }
            set
            {
                _hints = value;
                NotifyOfPropertyChange();
            }
        }

        public Action CloseOverlay { get; set; }

        public string MatchString
        {
            set
            {
                foreach (var x in Hints)
                {
                    x.Active = false;
                }
                
                var matching = Hints.Where(x => x.Label.StartsWith(value, StringComparison.OrdinalIgnoreCase)).ToArray();
                foreach (var x in matching)
                {
                    x.Active = true;
                }

                if (_waitingForDecile != null)
                {
                    if (int.TryParse(value.Last().ToString(), out int decile))
                    {
                        _waitingForDecile.SetDecile(decile);
                        _waitingForDecile.Invoke();
                        CloseOverlay?.Invoke();
                    }
                }

                if (matching.Count() == 1)
                {
                    var matchedHint = matching.First().Hint;

                    if (matchedHint is UiAutomationScrollHint)
                    {
                        _waitingForDecile = matchedHint as UiAutomationScrollHint;
                    }
                    else
                    {
                        matching.First().Hint.Invoke();
                        CloseOverlay?.Invoke();
                    }
                }
            }
        }
    }
}
