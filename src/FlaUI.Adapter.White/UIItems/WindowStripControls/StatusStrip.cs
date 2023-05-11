using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using TestStack.White.UIItems.Finders;

namespace TestStack.White.UIItems.WindowStripControls
{
    public class StatusStrip : AutomationElement
    {
        public StatusStrip(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public StatusStrip(AutomationElement automationElement) : base(automationElement)
        {
        }

        public Label GetLabel(string text)
        {
            return this.Get<Label>(SearchCriteria.ByText(text));
        }
    }
}
