using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using TestStack.White.AutomationElementSearch;

namespace TestStack.White.UIItems.PropertyGridItems
{
    public class PropertyGridElementFinder
    {
        private readonly AutomationElementFinder finder;

        public PropertyGridElementFinder(AutomationElement automationElement)
        {
            finder = new AutomationElementFinder(automationElement);
        }

        public virtual List<AutomationElement> FindRows()
        { 
            return finder.Children(AutomationSearchCondition.ByControlType(ControlType.Table),
                                  AutomationSearchCondition.ByControlType(ControlType.Custom));
        }
    }
}