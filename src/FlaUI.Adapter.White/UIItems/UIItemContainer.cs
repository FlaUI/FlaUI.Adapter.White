using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace TestStack.White.UIItems
{   
    public class UIItemContainer : WindowItems.Window
    {
        public UIItemContainer(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public UIItemContainer(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public Bitmap VisibleImage => Capture();
    }
}
