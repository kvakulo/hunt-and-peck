using System;
using System.Windows;
using UIAutomationClient;

namespace HuntAndPeck.Models
{
    /// <summary>
    /// Represents a Windows UI Automation based focus hint
    /// </summary>
    internal class UiAutomationScrollHint : Hint
    {
        private readonly IUIAutomationScrollPattern _automationElement;
        private int _decile;

        public UiAutomationScrollHint(IntPtr owningWindow, IUIAutomationScrollPattern automationElement, Rect boundingRectangle)
            : base(owningWindow, boundingRectangle)
        {
            _automationElement = automationElement;
        }

        public void SetDecile(int decile)
        {
            _decile = decile;
        }

        public override void Invoke()
        {
            try
            {
                _automationElement.SetScrollPercent(-1, Math.Min(_decile*10, 100));
            }
            catch (InvalidOperationException) 
            {

            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
    }
}
