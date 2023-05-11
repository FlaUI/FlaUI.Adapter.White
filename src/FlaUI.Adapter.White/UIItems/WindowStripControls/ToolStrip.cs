using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using TestStack.White.UIItems.Finders;

namespace TestStack.White.UIItems.WindowStripControls
{
    public class ToolStrip : AutomationElement
    {
        public ToolStrip(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public ToolStrip(AutomationElement automationElement) : base(automationElement)
        {
        }
        public MenuItem MenuItem(params string[] path)
        {
            var menu = new MenuBar(FrameworkAutomationElement);
            return menu.MenuItem(path);
        }

        public Label GetLabel(string text)
        {
            return this.Get<Label>(SearchCriteria.ByText(text));
        }
    }
}
