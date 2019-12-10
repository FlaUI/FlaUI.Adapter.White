using System.Linq;
using FlaUI.Core;
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

        public static T Get<T>(this AutomationElement element, SearchCriteria searchCriteria) where T : AutomationElement
        {
            return Get(element, searchCriteria).As<T>();
        }

        public static T Get<T>(this AutomationElement element, ConditionBase condition) where T : AutomationElement
        {
            return Get(element, condition).As<T>();
        }

        public static AutomationElement Get(this AutomationElement element, SearchCriteria searchCriteria)
        {
            var condition = searchCriteria.ToCondition();
            if (searchCriteria.IsIndexed)
            {
                var allItems = element.FindAllDescendants(condition);
                if (allItems.Length >= searchCriteria.Index)
                {
                    return allItems[searchCriteria.Index];
                }
                return null;
            }
            return Get(element, condition);
        }

        public static AutomationElement Get(this AutomationElement element, ConditionBase condition)
        {
            var foundElement = element.FindFirstDescendant(condition);
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

        public static Window GetWindow(this Application application, string title)
        {
            return application.GetAllTopLevelWindows(WhiteAdapter.Automation).FirstOrDefault(x => x.Title == title);
        }
    }
}
