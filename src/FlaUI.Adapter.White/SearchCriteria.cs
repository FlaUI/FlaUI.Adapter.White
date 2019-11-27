using FlaUI.Core.Conditions;

namespace FlaUI.Adapter.White
{
    public static class SearchCriteria
    {
        public static ConditionFactory ConditionFactory => WhiteAdapter.ConditionFactory;

        public static PropertyCondition ByText(string value)
        {
            return ConditionFactory.ByText(value);
        }

        public static PropertyCondition ByAutomationId(string value)
        {
            return ConditionFactory.ByAutomationId(value);
        }

        public static PropertyCondition ByClassName(string className)
        {
            return ConditionFactory.ByClassName(className);
        }
    }
}
