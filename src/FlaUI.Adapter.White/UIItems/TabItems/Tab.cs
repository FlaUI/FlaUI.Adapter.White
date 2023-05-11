using System.Collections.Generic;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace TestStack.White.UIItems.TabItems
{
    public class Tab : FlaUI.Core.AutomationElements.Tab
    {
        public Tab(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public Tab(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public int TabCount => TabItems.Length ;

        public List<TabItem> Pages => TabItems.ToList();

        public void SelectTabPage(string tabName)
        {
            SelectTabItem(tabName);
        }

        public void SelectTabPage(int index)
        {
            SelectTabItem(index);
        }

        public TabItem SelectedTab => SelectedTabItem;

        public bool Enabled => IsEnabled;
    }
}
