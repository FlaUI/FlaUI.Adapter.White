
using FlaUI.Core.AutomationElements;
using System.Drawing;
using ControlType = FlaUI.Core.Definitions.ControlType;

namespace TestStack.White.UIItems
{
    public class ToolTip : AutomationElement
    {     
        public ToolTip(AutomationElement automationElement) : base(automationElement) {}

        public virtual string Text
        {
            get { return Name; }
        }

        public ToolTip GetFrom(Point point)
        {
            var automationElement = FrameworkAutomationElement.Automation.FromPoint(point);
            return automationElement.ControlType.Equals(ControlType.ToolTip) ? new ToolTip(automationElement) : null;
        }

        public virtual Point LeftOutside(Rectangle rect)
        {
            return new Point(BoundingRectangle.Left - 1, rect.Y);
        }
    }
}