using FlaUI.Core.AutomationElements;
using System;
using System.Linq;

namespace TestStack.White.UIA
{
    public static class AutomationElementCollectionX
    {
        public static bool Contains(this AutomationElement[] collection, Predicate<AutomationElement> predicate)
        {
            return collection.Any(element => predicate(element));
        }
    }
}