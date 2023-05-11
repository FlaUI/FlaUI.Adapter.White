using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace TestStack.White.UIItems
{
    public class RadioButton : FlaUI.Core.AutomationElements.RadioButton
    {
        public RadioButton(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }
        public RadioButton(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public bool IsSelected
        {
            get => IsChecked;
            set => IsChecked = value;
        }

        public bool Enabled => IsEnabled;

        public void Select()
        {
            IsChecked = true;
        }
    }
}
