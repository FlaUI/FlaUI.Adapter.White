using System.Collections.Generic;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace TestStack.White.UIItems.TreeItems
{
    public class Tree : FlaUI.Core.AutomationElements.Tree
    {
        public Tree(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement) {
        }
        public Tree(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }
        public IList<TreeNode> Nodes => Items.Select(x => new TreeNode(x)).ToList();         
    }
}
