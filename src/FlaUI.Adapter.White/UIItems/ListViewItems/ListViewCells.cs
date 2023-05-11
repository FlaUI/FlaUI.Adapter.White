using FlaUI.Core.AutomationElements;
using FlaUI.Core.Exceptions;
using System.Collections.Generic;
using TestStack.White.UIItems.ListViewItems;

namespace TestStack.White.UIItems
{
    public class ListViewCells : List<ListViewCell>
    {
        private readonly ListViewHeader _header;

        public ListViewCells(List<AutomationElement> collection, ListViewHeader header)          
        {
            _header = header;
            foreach (var element in collection)
            {
                Add(new ListViewCell(element.FrameworkAutomationElement));
            }               
        }

        public virtual ListViewCell this[string columnName]
        {
            get
            {
                if (_header == null && string.IsNullOrEmpty(columnName)) return this[0];
                if (_header == null) throw new ElementNotAvailableException($"Cannot get cell for {columnName}");
                return this[_header.Column(columnName)];
            }
        }

        internal virtual ListViewCell this[ListViewColumn column]
        {
            get
            {                
                return this[column.Index];
            }
        }
    }
}
