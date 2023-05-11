using System;
using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace TestStack.White.UIItems
{   
    public class Button : FlaUI.Core.AutomationElements.Button
    {
        public Button(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }
        public Button(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public bool Enabled => IsEnabled;

        public Bitmap VisibleImage => Capture();

        public bool Visible => !IsOffscreen;
        
        public bool IsFocussed => FrameworkAutomationElement.HasKeyboardFocus;

        public string Text => Name;

        public ToggleState State
        {
            get => Patterns.Toggle.Pattern.ToggleState.ValueOrDefault;
            set
            {
                for (int i = 0; i < Enum.GetNames(typeof(ToggleState)).Length; i++)
                {
                    if (State == value) {
                        break;
                    }
                    base.Patterns.Toggle.Pattern.Toggle();
                }
            }
        }

        public virtual Rectangle Bounds
        {
            get { return FrameworkAutomationElement.BoundingRectangle; }
        }

        public virtual string AccessKey
        {
            get { return FrameworkAutomationElement.AccessKey; }
        }

        public new void Click(bool moveMouse = false)
        {
            this.ClickElement();
        }

    }
}
