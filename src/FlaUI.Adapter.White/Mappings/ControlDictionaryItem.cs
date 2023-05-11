using System;
using FlaUI.Core.Definitions;

namespace TestStack.White.Mappings
{
    public class ControlDictionaryItem
    {
        private readonly Type testControlType;
        private readonly ControlType controlType;        

        public ControlDictionaryItem(Type testControlType, ControlType controlType)
        {
            this.testControlType = testControlType;
            this.controlType = controlType;
        }

        public static ControlDictionaryItem Primary(Type testControlType, ControlType controlType)
        {
            return new ControlDictionaryItem(testControlType, controlType);
        }     

        public static ControlDictionaryItem Secondary(Type testControlType, ControlType controlType)
        {
            return new ControlDictionaryItem(testControlType, controlType);
        }

        public virtual Type TestControlType
        {
            get { return testControlType; }
        }

        public virtual ControlType ControlType
        {
            get { return controlType; }
        }
    }
}
