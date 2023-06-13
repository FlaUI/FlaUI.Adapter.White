using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace TestStack.White.UIItems.WindowItems
{
    public class TitleBar : FlaUI.Core.AutomationElements.TitleBar
    {
        public TitleBar(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement) {
        }

        public TitleBar(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }
    }
}
