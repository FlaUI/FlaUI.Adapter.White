using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;

namespace FlaUI.Adapter.White
{
    public static class WhiteAdapterInitializer
    {
        public static void Initialize(ConditionFactory cf)
        {
            SearchCriteria.ConditionFactory = cf;
        }
    }

    public static class WhiteAdapterExtensions
    {
        public static T Get<T>(this AutomationElement element, string name) where T : AutomationElement
        {
            return Get<T>(element, element.ConditionFactory.ByName(name));
        }

        public static T Get<T>(this AutomationElement element, ConditionBase condition) where T : AutomationElement
        {
            var foundElement = element.FindFirstDescendant(condition).As<T>();
            return foundElement;
        }
    }

    public static class SearchCriteria
    {
        public static ConditionFactory ConditionFactory { get; set; }

        public static PropertyCondition ByText(string value)
        {
            return ConditionFactory.ByText(value);
        }

        public static PropertyCondition ByAutomationId(string value)
        {
            return ConditionFactory.ByAutomationId(value);
        }
    }
}
