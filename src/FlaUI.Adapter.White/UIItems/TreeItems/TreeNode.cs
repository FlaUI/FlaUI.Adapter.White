using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace TestStack.White.UIItems.TreeItems
{
    public class TreeNode : TreeItem
    {
        public TreeNode(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement) {
        }

        public TreeNode(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }
    }
}
