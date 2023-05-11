using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Input;
using FlaUI.Core.Logging;
using FlaUI.Core.Tools;
using TestStack.White.UIItems.Finders;
using Window = TestStack.White.UIItems.WindowItems.Window;

namespace TestStack.White.UIItems
{
    public static partial class AdapterExtensions
    {
        public static IList<T> GetChildren<T>(this AutomationElement element, SearchCriteria searchCriteria) where T : AutomationElement
        {
            return SearchControlFactory.CreateByControls<T>(GetChildren(element, searchCriteria));
        }

        public static IList<T> GetDescendants<T>(this AutomationElement element, SearchCriteria searchCriteria) where T : AutomationElement
        {
            return SearchControlFactory.CreateByControls<T>(GetDescendants(element, searchCriteria));
        }

        public static T Cast<T>(this AutomationElement element) where T : AutomationElement
        {
            return SearchControlFactory.CreateForControl<T>(element);
        }

        public static T Get<T>(this AutomationElement element, string automationId) where T : AutomationElement
        {
            return Get<T>(element, element.ConditionFactory.ByAutomationId(automationId));
        }
       
        public static T Get<T>(this AutomationElement element, SearchCriteria searchCriteria) where T : AutomationElement
        {
            return SearchControlFactory.CreateForControl<T>(Get(element, searchCriteria));
        }

        public static T Get<T>(this AutomationElement element, ConditionBase condition) where T : AutomationElement
        {
            return SearchControlFactory.CreateForControl<T>(Get(element, condition));
        }
       
        public static T Get<T>(this AutomationElement element) where T : AutomationElement
        {
            return SearchControlFactory.CreateForControl<T>(Get(element, SearchCriteria.All.AndControlType(typeof(T))));
        }

        public static AutomationElement Get(this AutomationElement element, SearchCriteria searchCriteria)
        {
            AutomationElement GetAutomationElement()
            {
                var condition = searchCriteria.ToCondition();
                if (searchCriteria.IsIndexed)
                {
                    var allItems = element.FindAllDescendants(condition);
                    if (allItems.Length >= searchCriteria.Index)
                    {
                        return allItems[searchCriteria.Index];
                    }

                    return null;
                }

                return Get(element, condition);
            }

            element.FocusWindow();
            var result = GetAutomationElement();

            return result;
        }

        private static IList<AutomationElement> GetChildren(this AutomationElement element, SearchCriteria searchCriteria)
        {
            element.FocusWindow();
            var condition = searchCriteria.ToCondition();
            if (searchCriteria.IsIndexed)
            {
                var allItems = element.FindAllChildren(condition);
                if (allItems.Length >= searchCriteria.Index)
                {
                    return new List<AutomationElement> { allItems[searchCriteria.Index] };
                }
                return null;
            }
            return element.FindAllChildren(condition);
        }

        private static IList<AutomationElement> GetDescendants(this AutomationElement element, SearchCriteria searchCriteria)
        {
            element.FocusWindow();
            var condition = searchCriteria.ToCondition();
            if (searchCriteria.IsIndexed)
            {
                var allItems = element.FindAllDescendants(condition);
                if (allItems.Length >= searchCriteria.Index)
                {
                    return new List<AutomationElement> { allItems[searchCriteria.Index] };
                }
                return null;
            }
            return element.FindAllDescendants(condition);
        }

        public static AutomationElement GetElement(this AutomationElement element, SearchCriteria searchCriteria)
        {
            return element.Get(searchCriteria);
        }

        public static AutomationElement Get(this AutomationElement element, ConditionBase condition)
        {
            AutomationElement GetAutomationElement()
            {
                var foundElement = element.FindFirstDescendant(condition);
                return foundElement;
            }

            element.FocusWindow();
            return Retry.WhileNull(() =>
            {
                try
                {
                    return GetAutomationElement();
                }
                catch (COMException ex)
                {
                    // ignore exception in case timeout
                    Console.WriteLine(ex);
                    return null;
                }
            }).Result;
        }

        public static bool Exists<T>(this AutomationElement element, string automationId) where T : AutomationElement
        {
            AutomationElement elementResult;
            try
            {
                elementResult = Get<T>(element, element.ConditionFactory.ByAutomationId(automationId));
            }
            catch
            {
                elementResult = null;
            }
            return elementResult != null;
        }      

        public static AutomationElement[] GetMultiple(this AutomationElement parent, SearchCriteria searchCriteria)
        {
            return parent.FindAllDescendants(searchCriteria.ToCondition());
        }

        public static void FocusWindow(this AutomationElement element)
        {

            if (element.GetType() != typeof(Window))
            {
                return;
            }

            FocusElement(element);
        }

        public static void FocusElement(this AutomationElement element)
        {
            try
            {
                Wait.UntilInputIsProcessed(TimeSpan.FromMilliseconds(500));
                element.Focus();
            }
            catch
            {
                Logger.Default.Debug($"Could not set focus on {element.Properties.AutomationId.ValueOrDefault}");
            }
        }

        public static bool WaitUntilElementEnabled(this AutomationElement element)
        {
            element.WaitUntilInputIsProcessed();
            if (!element.Properties.IsEnabled.IsSupported)
            {
                return false;
            }
            var isEnabled = false;
            Retry.WhileFalse(() =>
            {
                element.Properties.IsEnabled.TryGetValue(out isEnabled);
                return isEnabled;
            }, timeout: TimeSpan.FromSeconds(60), throwOnTimeout: false);
            return isEnabled;
        }

        public static string ErrorProviderMessage(this AutomationElement element, Window window)
        {
            var elementFromPoint = element.Automation.FromPoint(element.BoundingRectangle.ImmediateExteriorEast());
            if (elementFromPoint == null) return null;
            var errorProviderBounds = elementFromPoint.BoundingRectangle;
            if (element.BoundingRectangle.Right != errorProviderBounds.Left) return null;
            Mouse.MoveTo(errorProviderBounds.Center());
            var toolTipElement = Retry.WhileNull(() => window.FindFirstDescendant(SearchCriteria.ByControlType(FlaUI.Core.Definitions.ControlType.ToolTip).ToCondition()), TimeSpan.FromSeconds(5)).Result;
            return toolTipElement != null ? toolTipElement.Name : string.Empty;
        }

        public static bool Visible(this AutomationElement element)
        {           
            return element != null && !element.IsOffscreen;
        }
    }
}