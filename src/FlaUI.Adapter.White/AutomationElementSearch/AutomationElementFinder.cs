using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Exceptions;

namespace TestStack.White.AutomationElementSearch
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
        public virtual List<AutomationElement> Children(params AutomationSearchCondition[] automationSearchConditions)
        {
            return FindAll(AutomationElement, automationSearchConditions);
        }

        public AutomationElement Descendant(AutomationSearchCondition automationSearchCondition)
        {
            return AutomationElement.FindFirstDescendant(automationSearchCondition.Condition);
        }

        public List<AutomationElement> Descendants(AutomationSearchCondition automationSearchCondition)
        {
            return AutomationElement.FindAllDescendants(automationSearchCondition.Condition).ToList();
        }

        private List<AutomationElement> FindAll(AutomationElement startElement, AutomationSearchCondition[] searchConditions)
        {
            var currentElement = startElement;
            for (int i = 0; i < searchConditions.Length; i++)
            {
                var currentFinder = new AutomationElementFinder(currentElement);
                if (i == searchConditions.Length - 1)
                {
                    return currentFinder.Children(searchConditions[i]);
                }

                var childElement = currentFinder.Child(searchConditions[i]);
                currentElement = childElement;
                if (childElement == null)
                {
                    return null;
                }
            }

            throw new FlaUIException("Something wrong in logic here");
        }
    }
}
