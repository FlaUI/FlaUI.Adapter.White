using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.Definitions;

namespace TestStack.White.Mappings
{
    public class ControlDictionaryItems : List<ControlDictionaryItem>
    {
        public virtual void AddPrimary(Type testControlType, ControlType controlType)
        {
            Add(ControlDictionaryItem.Primary(testControlType, controlType));
        }

        public virtual void AddSecondary(Type testControlType, ControlType controlType)
        {
            Add(ControlDictionaryItem.Secondary(testControlType, controlType));
        }

        public virtual void AddPrimary(Type testControlType, ControlType controlType, bool hasPrimaryChildren)
        {
            Add(ControlDictionaryItem.Primary(testControlType, controlType));
        } 

        public virtual ControlDictionaryItem[] FindBy(Type testControlType)
        {
            return this.Where(obj => testControlType == obj.TestControlType).ToArray();
        }
    }
}
