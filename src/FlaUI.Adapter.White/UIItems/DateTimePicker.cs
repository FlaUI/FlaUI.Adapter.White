using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using System;

namespace TestStack.White.UIItems
{   
    public class DateTimePicker : FlaUI.Core.AutomationElements.DateTimePicker
    {
        public DateTimePicker(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement) {
        }

        public DateTimePicker(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public virtual DateTime? Date
        {
            get
            {              
                return Patterns.Value.TryGetPattern(out var valuePattern) && !string.IsNullOrEmpty(valuePattern.Value.Value) ? SelectedDate: null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectedDate = value;
                }           
            }
        }
    }
}
