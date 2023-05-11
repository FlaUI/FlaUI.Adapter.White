using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace TestStack.White.UIItems
{
    public class Label : FlaUI.Core.AutomationElements.Label
    {
        public Label(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }
        public Label(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }
        public Rectangle Bounds => BoundingRectangle;
        public bool Enabled => IsEnabled;
        public new string Id => Properties.AutomationId.ValueOrDefault;

        public new string Name  { 
            get
            {              
                return !string.IsNullOrEmpty(Properties.Name.ValueOrDefault) ? 
                    Properties.Name.ValueOrDefault : Properties.HelpText.ValueOrDefault;
            }        
        } 
    }
}
