using FlaUI.Core.AutomationElements;
using FlaUI.Core.Exceptions;
using System.Collections.Generic;

namespace TestStack.White.UIItems.ListViewItems
{
    public class ListViewColumns : List<ListViewColumn>
    {
        public ListViewColumns(IList<AutomationElement> automationElementCollection)
        {
            int i = 0;
            foreach (var element in automationElementCollection)
            {
                Add(new ListViewColumn(element, i++));
            }
        }

        public virtual ListViewColumn this[string text]
        {
            get
            {
                var column = Find(delegate(ListViewColumn obj) { return obj.Name.Equals(text); });
                if (column == null) throw new ElementNotAvailableException($"Cannot find column with text {text}");
                return column;
            }
        }
    }
}