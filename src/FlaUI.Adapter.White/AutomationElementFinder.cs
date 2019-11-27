using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;

namespace FlaUI.Adapter.White
{
    public class AutomationElementFinder
    {
        public AutomationElement AutomationElement { get; }

        public AutomationElementFinder(AutomationElement automationElement)
        {
            AutomationElement = automationElement ?? throw new ArgumentNullException(nameof(automationElement));
        }

        public AutomationElement Child(AutomationSearchCondition automationSearchCondition)
        {
            return AutomationElement.FindFirstChild(automationSearchCondition.Condition);
        }

        public List<AutomationElement> Children(AutomationSearchCondition automationSearchCondition)
        {
            return AutomationElement.FindAllChildren(automationSearchCondition.Condition).ToList();
        }

        public AutomationElement Descendant(AutomationSearchCondition automationSearchCondition)
        {
            return AutomationElement.FindFirstDescendant(automationSearchCondition.Condition);
        }

        public List<AutomationElement> Descendants(AutomationSearchCondition automationSearchCondition)
        {
            return AutomationElement.FindAllDescendants(automationSearchCondition.Condition).ToList();
        }
    }
}
