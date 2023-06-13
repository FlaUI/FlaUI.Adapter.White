using System;
using System.Collections.Generic;
using FlaUI.Adapter.White;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Identifiers;
using TestStack.White.Mappings;

namespace TestStack.White.UIItems.Finders
{
    public class SearchCriteria
    {
        private List<ConditionBase> _conditions = new List<ConditionBase>();

        public int Index { get; private set; } = -1;

        public bool IsIndexed => Index >= 0;

        private SearchCriteria()
        {
        }

        private SearchCriteria(ConditionBase condition)
        {
            _conditions.Add(condition);
        }

        public static ConditionFactory ConditionFactory => WhiteAdapter.ConditionFactory;

        public static SearchCriteria All
        {
            get { return new SearchCriteria(TrueCondition.Default); }
        }

        public static SearchCriteria ByName(string value)
        {
            return new SearchCriteria(ConditionFactory.ByName(value));
        }

        public static SearchCriteria ByText(string value)
        {
            return new SearchCriteria(ConditionFactory.ByText(value));
        }

        public static SearchCriteria Indexed(int zeroBasedIndex)
        {
            var criteria = new SearchCriteria();
            criteria.Index = zeroBasedIndex;
            return criteria;
        }

        public static SearchCriteria ByNameOrAutomationId(string value)
        {
            return new SearchCriteria(ConditionFactory.ByAutomationId(value).Or(ConditionFactory.ByName(value)));
        }

        public static SearchCriteria ByAutomationId(string value)
        {
            return new SearchCriteria(ConditionFactory.ByAutomationId(value));
        }

        public static SearchCriteria ByFramework(string framework)
        {
            return new SearchCriteria(ConditionFactory.ByFrameworkId(framework));
        }

        public static SearchCriteria ByControlType(ControlType controlType)
        {
            return new SearchCriteria(ConditionFactory.ByControlType(controlType));
        }

        public static SearchCriteria ByControlType(Type type)
        {
            var controlTypeConditions = new List<ConditionBase>();
            foreach (var controlType in ControlDictionary.Instance.GetControlType(type))
            {
                controlTypeConditions.Insert(0, ConditionFactory.ByControlType(controlType));
            }          
            return new SearchCriteria(new OrCondition(controlTypeConditions));
        }

        public static SearchCriteria ByNativeProperty(PropertyId automationProperty, string value)
        {
            return new SearchCriteria(new PropertyCondition(automationProperty, value));
        }

        public static SearchCriteria ByNativeProperty(PropertyId automationProperty, bool value)
        {
            return new SearchCriteria(new PropertyCondition(automationProperty, value));
        }

        public static SearchCriteria ByNativeProperty(PropertyId automationProperty, object value)
        {
            return new SearchCriteria(new PropertyCondition(automationProperty, value));
        }

        public static SearchCriteria ByClassName(string className)
        {
            return new SearchCriteria(ConditionFactory.ByClassName(className));
        }

        public virtual SearchCriteria AndByText(string text)
        {
            _conditions.Insert(0, ConditionFactory.ByName(text));
            return this;
        }

        public virtual SearchCriteria AndIndex(int zeroBasedIndex)
        {
            Index = zeroBasedIndex;
            return this;
        }

        public virtual SearchCriteria AndByClassName(string className)
        {
            _conditions.Insert(0, ConditionFactory.ByClassName(className));
            return this;
        }

        public virtual SearchCriteria AndOfFramework(string framework)
        {
            _conditions.Insert(0, ConditionFactory.ByFrameworkId(framework));
            return this;
        }

        public virtual SearchCriteria NotIdentifiedByText(string name)
        {
            _conditions.Insert(0, ConditionFactory.ByName(name).Not());
            return this;
        }

        public virtual SearchCriteria AndControlType(ControlType controlType)
        {
            _conditions.Insert(0, ConditionFactory.ByControlType(controlType));
            return this;
        }

        public virtual SearchCriteria AndControlType(Type type)
        {
            var controlTypeConditions = new List<ConditionBase>();
            foreach (var controlType in ControlDictionary.Instance.GetControlType(type))
            {
                controlTypeConditions.Insert(0, ConditionFactory.ByControlType(controlType));
            }
            _conditions.Insert(0, new OrCondition(controlTypeConditions));
            return this;
        }

        public virtual SearchCriteria AndAutomationId(string id)
        {
            _conditions.Insert(0, ConditionFactory.ByAutomationId(id));
            return this;
        }

        public virtual SearchCriteria AndProcessId(int id)
        {
            _conditions.Insert(0, ConditionFactory.ByProcessId(id));
            return this;
        }

        public virtual SearchCriteria AndNativeProperty(PropertyId automationProperty, string value)
        {
            _conditions.Insert(0, new PropertyCondition(automationProperty, value));
            return this;
        }

        public virtual SearchCriteria AndNativeProperty(PropertyId automationProperty, bool value)
        {
            _conditions.Insert(0, new PropertyCondition(automationProperty, value));
            return this;
        }

        public virtual SearchCriteria AndNativeProperty(PropertyId automationProperty, object value)
        {
            _conditions.Insert(0, new PropertyCondition(automationProperty, value));
            return this;
        }

        public ConditionBase ToCondition()
        {
            if (_conditions.Count == 1)
            {
                return _conditions[0];
            }
            if (_conditions.Count == 0)
            {
                return TrueCondition.Default;
            }
            return new AndCondition(_conditions);
        }
    }
}
