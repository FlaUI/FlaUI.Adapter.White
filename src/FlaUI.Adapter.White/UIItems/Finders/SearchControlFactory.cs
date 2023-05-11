using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.White.UIItems.TableItems;
using AutomationElement = FlaUI.Core.AutomationElements.AutomationElement;

namespace TestStack.White.UIItems.Finders
{
    public class SearchControlFactory
    {   
        internal static T CreateForControl<T>(AutomationElement element) where T : AutomationElement
        {
            if (element == null)
            {
                return null;
            }
            return (T)Activator.CreateInstance(typeof(T), element.FrameworkAutomationElement);
        }

        internal static IList<T> CreateByControls<T>(IList<AutomationElement> element) where T : AutomationElement
        {
            if (element == null)
            {
                return null;
            }

            if (typeof(T) == typeof(Table))
            {
                var list = element.Select(x => new Table(x.FrameworkAutomationElement)).Cast<T>().ToList();
                return list;
            }

            return element.Cast<T>().ToList();
        } 
    }
}
