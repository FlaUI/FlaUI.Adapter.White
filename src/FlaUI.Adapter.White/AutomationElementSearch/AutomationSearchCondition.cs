using System.Collections.Generic;
using FlaUI.Adapter.White;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;

namespace TestStack.White.AutomationElementSearch
{
    public class AutomationSearchCondition
    {
        private readonly List<ConditionBase> _conditions = new List<ConditionBase>();
        public static ConditionFactory ConditionFactory => WhiteAdapter.ConditionFactory;

        public virtual ConditionBase Condition
        {
            get
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

        public AutomationSearchCondition()
        {
        }

        public AutomationSearchCondition(ConditionBase condition)
        {
            Add(condition);
        }

        public void Add(ConditionBase condition)
        {
            _conditions.Add(condition);
        }

        public AutomationSearchCondition OfName(string name)
        {
            _conditions.Add(ConditionFactory.ByName(name));
            return this;
        }

        public AutomationSearchCondition OfControlType(ControlType controlType)
        {
            _conditions.Add(ConditionFactory.ByControlType(controlType));
            return this;
        }

        public AutomationSearchCondition WithAutomationId(string id)
        {
            _conditions.Add(ConditionFactory.ByAutomationId(id));
            return this;
        }

        public virtual AutomationSearchCondition WithProcessId(int processId)
        {
            _conditions.Add(ConditionFactory.ByProcessId(processId));
            return this;
        }

        public static AutomationSearchCondition ByName(string name)
        {
            var automationSearchCondition = new AutomationSearchCondition();
            automationSearchCondition.OfName(name);
            return automationSearchCondition;
        }

        public static AutomationSearchCondition ByControlType(ControlType controlType)
        {
            var automationSearchCondition = new AutomationSearchCondition();
            automationSearchCondition.OfControlType(controlType);
            return automationSearchCondition;
        }

        public static AutomationSearchCondition ByAutomationId(string id)
        {
            var automationSearchCondition = new AutomationSearchCondition();
            automationSearchCondition.WithAutomationId(id);
            return automationSearchCondition;
        }

        public static AutomationSearchCondition ByClassName(string className)
        {
            var asc = new AutomationSearchCondition();
            asc._conditions.Add(ConditionFactory.ByClassName(className));
            return asc;
        }

        public static AutomationSearchCondition All
        {
            get
            {
                var asc = new AutomationSearchCondition();
                asc._conditions.Add(TrueCondition.Default);
                return asc;
            }
        }
    }
}
