using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using System.Threading;
using TestStack.White.InputDevices;

namespace TestStack.White.UIItems
{
    internal class TooltipSafeMouse
    {
        private readonly Mouse mouse;

        public TooltipSafeMouse(Mouse mouse)
        {
            this.mouse = mouse;
        }

        public virtual void RightClickOutsideToolTip(AutomationElement uiItem)
        {          
            ToolTip toolTip = GetToolTip(uiItem);
            if (toolTip == null)
            {
                //Because mouse has already been moved
                mouse.RightClick();
            }
            else
            {               
                mouse.RightClick(toolTip.LeftOutside(uiItem.BoundingRectangle));
            }
        }

        public virtual void DoubleClickOutsideToolTip(AutomationElement uiItem)
        {         
            ToolTip toolTip = GetToolTip(uiItem);
            if (toolTip == null)
                mouse.DoubleClick(uiItem.BoundingRectangle.Center());
            else
            {               
                mouse.DoubleClick(toolTip.LeftOutside(uiItem.BoundingRectangle));
            }
        }

        public virtual void ClickOutsideToolTip(AutomationElement uiItem)
        {
            ToolTip toolTip = GetToolTip(uiItem);
            if (toolTip == null)
                mouse.Click(uiItem.BoundingRectangle.Center());
            else
            {                
                mouse.Click(toolTip.LeftOutside(uiItem.BoundingRectangle));
            }
        }

        private ToolTip GetToolTip(AutomationElement uiItem)
        {
            mouse.Click(uiItem.BoundingRectangle.Center());
            Thread.Sleep(3000);
            return new ToolTip(uiItem).GetFrom(uiItem.BoundingRectangle.Center());
        }
    }
}