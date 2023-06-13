using FlaUI.Core.AutomationElements;
using FlaUI.Core.Exceptions;
using System.Collections;
using System.Collections.Generic;
using TestStack.White.UIItems.ListViewItems;

namespace TestStack.White.UIItems
{
    public class ListViewRows : List<ListViewRow>
    {
        public ListViewRows(ICollection tees)
        {
            foreach (var t in tees) Add((ListViewRow)t);
        }

        public ListViewRows(List<AutomationElement> collection, ListViewHeader header)
        {
            foreach (var element in collection)
            {
                Add(new ListViewRow(element.FrameworkAutomationElement, header));
            }
        }    

        public virtual ListViewRow Get(int zeroBasedIndex)
        {
            if (Count <= zeroBasedIndex) throw new ElementNotAvailableException($"No row found with index {zeroBasedIndex}");
            return this[zeroBasedIndex];
        }    

        public virtual ListViewRows SelectedRows
        {           
            get { return new ListViewRows(FindAll(obj => obj.IsSelected)); }
        }
    }
}