using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using TestStack.White.UIItems.Finders;

namespace TestStack.White.UIItems
{
    public static partial class AdapterExtensions
    {  
        public static void ScrollToRow(this AutomationElement element)
        {    
            ScrollByScrollPattern(element);
            ScrollByScrollBarButtons(element);
        }

        /// <summary>
        /// Check if the element is visible in the parent container
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsElementOffScreen(this AutomationElement element)
        {
            var elementBoundingRect = element.BoundingRectangle;

            if (element.Parent == null || element.IsOffscreen)
            {
                return true;
            }

            var parentBoundingRect = element.Parent.BoundingRectangle;
            var header = element.Parent.FindFirstChild((ConditionFactory cf) => cf.ByName("Top Row")
                                                   .Or(cf.ByControlType(ControlType.Header))
                                                   .Or(cf.ByName(LocalizedStrings.DataGridViewHeader)));

            if (header != null)
            {
                parentBoundingRect.Y += header.BoundingRectangle.Height;
                parentBoundingRect.Height -= header.BoundingRectangle.Height;
            }

            return !parentBoundingRect.Contains(elementBoundingRect);
        }

        private static void ScrollByScrollPattern(AutomationElement element)
        {
            var scrollPattern = element.Patterns?.Scroll?.PatternOrDefault ?? element.Parent?.Patterns?.Scroll?.PatternOrDefault;
            if (scrollPattern == null || !scrollPattern.VerticallyScrollable.Value || !IsElementOffScreen(element))
            {
                return;
            }

            // scroll down
            while (true)
            {
                scrollPattern.Scroll(ScrollAmount.NoAmount, ScrollAmount.SmallIncrement);

                if (!element.IsOffscreen)
                {
                    if (element.FrameworkType == FrameworkType.WinForms && scrollPattern.HorizontalScrollPercent < 100)
                    {
                        scrollPattern.Scroll(ScrollAmount.NoAmount, ScrollAmount.SmallIncrement);
                    }
                    return;
                }

                if (scrollPattern.VerticalScrollPercent == 100)
                {
                    break;
                }
            }

            // scroll up
            while (true)
            {
                scrollPattern.Scroll(ScrollAmount.NoAmount, ScrollAmount.SmallDecrement);

                if (!element.IsOffscreen)
                {
                    if (element.FrameworkType == FrameworkType.WinForms && scrollPattern.HorizontalScrollPercent < 100)
                    {
                        scrollPattern.Scroll(ScrollAmount.NoAmount, ScrollAmount.SmallDecrement);
                    }
                    return;
                }

                if (scrollPattern.VerticalScrollPercent == 0)
                {
                    break;
                }
            }
        }

        private static void ScrollByScrollBarButtons(this AutomationElement element)
        {
            if (!IsElementOffScreen(element))
            {
                return;
            }
            var positionThumb = element.GetScrollBarPositionThumb();
            if(positionThumb == null)
            {
                return;
            }
            // scroll down
            var lineDownButton = element.GetScrollBarLineDownButton();
            if (lineDownButton != null)
            {
                while (true)
                {
                    var beforePositionThumbPoint = positionThumb.BoundingRectangle.Center();
                    lineDownButton.Invoke();
                    if (!IsElementOffScreen(element))
                    {
                        return;
                    }
                    var afterPositionThumbPoint = positionThumb.BoundingRectangle.Center();
                    if (beforePositionThumbPoint.X == afterPositionThumbPoint.X && beforePositionThumbPoint.Y == afterPositionThumbPoint.Y)
                    {
                        break;
                    }
                }
            }           

            // scroll up
            var lineUpButton = element.GetScrollBarLineUpButton();
            if (lineUpButton != null)
            {
                while (true)
                {
                    var beforePositionThumbPoint = positionThumb.BoundingRectangle.Center();
                    lineUpButton.Invoke();
                    if (!IsElementOffScreen(element))
                    {
                        return;
                    }
                    var afterPositionThumbPoint = positionThumb.BoundingRectangle.Center();
                    if (beforePositionThumbPoint.X == afterPositionThumbPoint.X && beforePositionThumbPoint.Y == afterPositionThumbPoint.Y)
                    {
                        break;
                    }
                }
            }           
        }

        private static Button GetScrollBarLineDownButton(this AutomationElement element)
        {
            var verticalScrollBar = element.GetVerticalScrollBar();
            if(verticalScrollBar == null)
            {
                return null;
            }
            return verticalScrollBar.Get<Button>(SearchCriteria.ByControlType(ControlType.Button).AndByText("Line down"));
        }

        private static Button GetScrollBarLineUpButton(this AutomationElement element)
        {
            var verticalScrollBar = element.GetVerticalScrollBar();
            if (verticalScrollBar == null)
            {
                return null;
            }
            return verticalScrollBar.Get<Button>(SearchCriteria.ByControlType(ControlType.Button).AndByText("Line up"));
        }

        private static AutomationElement GetScrollBarPositionThumb(this AutomationElement element)
        {
            var verticalScrollBar = element.GetVerticalScrollBar();
            if (verticalScrollBar == null)
            {
                return null;
            }           
            return verticalScrollBar.Get(SearchCriteria.ByText("Position"));
        }

        private static AutomationElement GetVerticalScrollBar(this AutomationElement element)
        {
            return element.Get(SearchCriteria.ByControlType(ControlType.ScrollBar).AndByText("Vertical Scroll Bar"))
                ?? element.Parent.Get(SearchCriteria.ByControlType(ControlType.ScrollBar).AndByText("Vertical Scroll Bar"))
                ?? element.Get(SearchCriteria.ByControlType(ControlType.Pane).AndByText("Vertical Scroll Bar"))
                ?? element.Parent.Get(SearchCriteria.ByControlType(ControlType.Pane).AndByText("Vertical Scroll Bar"));
        }          
    }
}