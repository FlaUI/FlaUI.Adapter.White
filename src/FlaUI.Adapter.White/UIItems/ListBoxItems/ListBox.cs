using System.Collections.Generic;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using TestStack.White.UIItems.Finders;
using FlaUI.Core.Exceptions;
using FlaUI.Core.Definitions;

namespace TestStack.White.UIItems.ListBoxItems
{
    public class ListBox : FlaUI.Core.AutomationElements.ListBox
    {
        public ListBox(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public ListBox(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public ListBoxItem Item(string itemText)
        {
            return Select(itemText);
        }

        public List<ListBoxItem> Items => base.Items.ToList();

        public string SelectedItemText => SelectedItem.Text;

        public virtual bool IsChecked(string itemText)
        {          
            var checkbox = GetCheckboxItem(itemText);
            return checkbox != null && checkbox.ToggleState == ToggleState.On ? true : false;
        }       

        public virtual void Check(string itemText)
        {
            var checkbox = GetCheckboxItem(itemText);
            checkbox.ToggleState = ToggleState.On;
        }

        public virtual void UnCheck(string itemText)
        {
            var checkbox = GetCheckboxItem(itemText);
            checkbox.ToggleState = ToggleState.Off;
        }

        private CheckBox GetCheckboxItem(string itemText)
        {
            if (FrameworkType == FrameworkType.Wpf)
            {
                var checkboxElement = this.Get<AutomationElement>(SearchCriteria.ByControlType(ControlType.CheckBox).AndByText(itemText));
                return new CheckBox(checkboxElement.FrameworkAutomationElement);
            } 
            else if (FrameworkType == FrameworkType.WinForms)
            {          
                var items = FindFirstDescendant(SearchCriteria.ByText(itemText).ToCondition());
                return new CheckBox(items.FrameworkAutomationElement);
            }
            throw new NotSupportedByFrameworkException($"FrameworkType {0} is not support in GetCheckboxItem");
        }
    }
}
