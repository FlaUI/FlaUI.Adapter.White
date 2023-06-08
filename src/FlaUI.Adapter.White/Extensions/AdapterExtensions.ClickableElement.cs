using FlaUI.Core.AutomationElements;
using FlaUI.Core.Exceptions;
using FlaUI.Core.Logging;
using FlaUI.Core.Tools;
using System;
using System.Drawing;
using TestStack.White.InputDevices;
using Wait = FlaUI.Core.Input.Wait;

namespace TestStack.White.UIItems
{
    public static partial class AdapterExtensions
    {
        public static Mouse AttachedMouse = Mouse.Instance;
        public static void ClickElement(this AutomationElement element)
        {
            if (element.FrameworkAutomationElement.IsKeyboardFocusable && !element.FrameworkAutomationElement.HasKeyboardFocus)
            {
                element.FocusElement();
            }

            var point = GetClickablePoint(element);
            if (!point.IsEmpty)
            {
                AttachedMouse.Click(point);
                element.WaitUntilInputIsProcessed();
            }
        }

        public static void RightClickElement(this AutomationElement element)
        {
            if (element.FrameworkAutomationElement.IsKeyboardFocusable && !element.FrameworkAutomationElement.HasKeyboardFocus)
            {
                element.FocusElement();
            }

            var point = GetClickablePoint(element);
            if (!point.IsEmpty)
            {
                AttachedMouse.RightClick(point);
                element.WaitUntilInputIsProcessed();
            }
        }

        public static void LeftClickElement(this AutomationElement element)
        {
            if (element.FrameworkAutomationElement.IsKeyboardFocusable && !element.FrameworkAutomationElement.HasKeyboardFocus)
            {
                element.FocusElement();
            }

            var point = GetClickablePoint(element);
            if (!point.IsEmpty)
            {
                AttachedMouse.LeftClick(point);
                element.WaitUntilInputIsProcessed();
            }
        }

        public static void DoubleClickElement(this AutomationElement element)
        {
            if (element.FrameworkAutomationElement.IsKeyboardFocusable && !element.FrameworkAutomationElement.HasKeyboardFocus)
            {
                element.FocusElement();
            }

            var point = GetClickablePoint(element);
            if (!point.IsEmpty)
            {
                AttachedMouse.DoubleClick(point);
                element.WaitUntilInputIsProcessed();
            }
        }

        public static void RightDoubleClickElement(this AutomationElement element)
        {
            if (element.FrameworkAutomationElement.IsKeyboardFocusable && !element.FrameworkAutomationElement.HasKeyboardFocus)
            {
                element.FocusElement();
            }

            var point = GetClickablePoint(element);
            if (!point.IsEmpty)
            {
                AttachedMouse.RightDoubleClick(point);
                element.WaitUntilInputIsProcessed();
            }
        }

        public static void LeftDoubleClickElement(this AutomationElement element)
        {
            if (element.FrameworkAutomationElement.IsKeyboardFocusable && !element.FrameworkAutomationElement.HasKeyboardFocus)
            {
                element.FocusElement();
            }

            var point = GetClickablePoint(element);
            if (!point.IsEmpty)
            {
                AttachedMouse.LeftDoubleClick(point);
                element.WaitUntilInputIsProcessed();
            }
        }

        public static Point GetClickablePoint(this AutomationElement element)
        {
            if (element == null)
            {
                throw new ElementNotAvailableException("No element to click.");
            }
            var point = element.BoundingRectangle.Center();
            if (point.IsEmpty)
            {
                point = element.GetClickablePoint();
            }
            return point;
        }

        public static Point TryGetClickablePoint(this AutomationElement element)
        {
            try
            {
                return GetClickablePoint(element);
            }
            catch (Exception exception)
            {
                Logger.Default.Debug($"Could not get clicked points: {exception}");
            }
            return Point.Empty;
        }

        public static void WaitUntilInputIsProcessed(this AutomationElement element)
        {
            Wait.UntilInputIsProcessed(TimeSpan.FromMilliseconds(500));            
            var clickedPoint = element.TryGetClickablePoint();
            var clickedPointX = !clickedPoint.IsEmpty ? clickedPoint.X : 0;
            var clickedPointY = !clickedPoint.IsEmpty ? clickedPoint.Y : 0;
            Retry.WhileTrue(() => AttachedMouse.IsCursorShowing(clickedPointX, clickedPointY), TimeSpan.FromMinutes(15));
        }
    }
}