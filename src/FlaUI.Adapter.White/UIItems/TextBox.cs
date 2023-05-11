using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;

namespace TestStack.White.UIItems
{
    public class TextBox : FlaUI.Core.AutomationElements.TextBox
    {
        public TextBox(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public TextBox(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public FrameworkAutomationElementBase AutomationElement => FrameworkAutomationElement;

        public bool IsFocussed => FrameworkAutomationElement.HasKeyboardFocus; 

        public bool Enabled => IsEnabled;

        public Bitmap VisibleImage => Capture();

        public bool Visible => !IsOffscreen;

        public void SetValue(string text) {
            Enter(text);
        }

        public Rectangle Bounds => BoundingRectangle;

        public string Id => Properties.AutomationId.ValueOrDefault;

        public void ClickAtRightEdge()
        {
            Mouse.Click(Bounds.ImmediateInteriorEast());
        }

        public virtual Color BorderColor
        {
            get { return VisibleImage.GetPixel(0, 0); }
        }

        public virtual Point Location
        {
            get
            {
                return new Point(BoundingRectangle.Left, BoundingRectangle.Top);
            }
        }
    }
}
