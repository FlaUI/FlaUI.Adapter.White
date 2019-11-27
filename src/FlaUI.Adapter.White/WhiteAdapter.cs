using FlaUI.Core;
using FlaUI.Core.Conditions;

namespace FlaUI.Adapter.White
{
    public static class WhiteAdapter
    {
        public static AutomationBase Automation { get; private set; }

        public static ConditionFactory ConditionFactory => Automation?.ConditionFactory;

        public static void Initialize(AutomationBase automation)
        {
            Automation = automation;
        }
    }
}
