using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace FlaUI.Adapter.White
{
    public class UIItem : AutomationElement
    {
        public UIItem(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public UIItem(AutomationElement automationElement) : base(automationElement)
        {
        }
    }
}
