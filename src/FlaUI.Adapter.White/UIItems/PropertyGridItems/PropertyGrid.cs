using System.Collections.Generic;
using FlaUI.Core;
using TestStack.White.UIItems.Finders;
using AutomationElement = FlaUI.Core.AutomationElements.AutomationElement;

namespace TestStack.White.UIItems.PropertyGridItems
{
    public class PropertyGrid : AutomationElement
    {
        private readonly PropertyGridElementFinder finder;
        public PropertyGrid(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
            finder = new PropertyGridElementFinder(this);
        }

        public PropertyGrid(AutomationElement automationElement) : base(automationElement)
        {
            finder = new PropertyGridElementFinder(automationElement);
        }

        public AutomationElement GetElement(SearchCriteria searchCriteria)
        {
            return this.Get(searchCriteria);
        }

        /// <summary>
        /// 
        /// Provides a list of categories in the property grid.
        /// </summary>
        public virtual List<PropertyGridCategory> Categories
        {
            get
            {
                var categories = new List<PropertyGridCategory>();
                List<AutomationElement> rows = finder.FindRows();
                foreach (AutomationElement element in rows)
                {
                    categories.Add(new PropertyGridCategory(element.FrameworkAutomationElement));
                }

                return categories;
            }
        }
        public virtual PropertyGridCategory Category(string name)
        {
            return Categories.Find(category => category.Name == name);
        }

        public bool Enabled => base.IsEnabled;
    }
}