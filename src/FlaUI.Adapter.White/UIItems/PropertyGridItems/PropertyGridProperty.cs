using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace TestStack.White.UIItems.PropertyGridItems
{
    public class PropertyGridProperty : AutomationElement
    {
        public PropertyGridProperty(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public PropertyGridProperty(AutomationElement automationElement) : base(automationElement)
        {
        }

        public virtual string Text
        {
            get { return Name; }
        } 
    }
}
