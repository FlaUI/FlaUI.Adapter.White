using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;

namespace FlaUI.Adapter.White
{
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

        public static void KeyIn(this AutomationElement element, VirtualKeyShort key)
        {
            Keyboard.Type(key);
        }

        public static Window ModalWindow(this Window parent, string title)
        {
            var cf = parent.ConditionFactory;
            return parent.FindFirstDescendant(cf.ByControlType(ControlType.Window).And(cf.ByName(title))).AsWindow();
        }

        public static ConditionBase AndByText(this ConditionBase condition, string text)
        {
            var newCondition = WhiteAdapter.ConditionFactory.ByName(text);
            return And(condition, newCondition);
        }

        public static ConditionBase AndByClassName(this ConditionBase condition, string className)
        {
            var newCondition = WhiteAdapter.ConditionFactory.ByClassName(className);
            return And(condition, newCondition);
        }

        public static AndCondition And(this ConditionBase condition, ConditionBase newCondition)
        {
            if (condition is AndCondition andCondition)
            {
                andCondition.Conditions.Add(newCondition);
                return andCondition;
            }
            return new AndCondition(condition, newCondition);
        }
    }
}
