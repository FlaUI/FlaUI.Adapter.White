using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace TestStack.White.UIItems
{    
    public class CheckBox : FlaUI.Core.AutomationElements.CheckBox
    {
        public CheckBox(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public CheckBox(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public bool Checked
        {           
            get
            {
                return IsChecked.HasValue && IsChecked.Value;
            }
            set
            {
                if (IsChecked == value) return;
                this.ClickElement();
            }
        }

        public bool IsSelected
        {
            get
            {
                return IsChecked.HasValue && IsChecked.Value;
            }
            set
            {
                IsChecked = value;
            }            
        }

        public bool Enabled => IsEnabled;

        public bool IsFocussed => FrameworkAutomationElement.HasKeyboardFocus;
        
        public void Select()
        {
            Checked = true;
        }

        public void UnSelect()
        {
            Checked = false;
        }

        public virtual ToggleState State
        {
            get
            { 
                return IsChecked.HasValue && IsChecked.Value ? ToggleState.On : ToggleState.Off; 
            }
            set
            {
                IsChecked = value == ToggleState.On;              
            }
        } 
    }
}
