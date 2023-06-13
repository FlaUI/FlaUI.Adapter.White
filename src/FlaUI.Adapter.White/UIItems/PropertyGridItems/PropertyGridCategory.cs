using FlaUI.Core;
using System.Collections.Generic;
using AutomationElement = FlaUI.Core.AutomationElements.AutomationElement;

namespace TestStack.White.UIItems.PropertyGridItems
{
    public class PropertyGridCategory : AutomationElement
    {
        private readonly PropertyGridElementFinder finder;
        public PropertyGridCategory(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
            finder = new PropertyGridElementFinder(this);
        }

        public PropertyGridCategory(AutomationElement automationElement) : base(automationElement)
        {
        }
        public virtual string Text
        {
            get { return Name; }
        }

        public virtual PropertyGridProperty GetProperty(string text)
        {
            return Properties.Find(property => property.Text.Equals(text));
        }

        public virtual List<PropertyGridProperty> Properties
        {
            get
            {
                var properties = new List<PropertyGridProperty>();
                List<AutomationElement> rows = finder.FindRows();

                foreach (var element in rows)
                {
                    properties.Add(new PropertyGridProperty(element.FrameworkAutomationElement));
                }

                return properties;
            }
        }

    }
}
