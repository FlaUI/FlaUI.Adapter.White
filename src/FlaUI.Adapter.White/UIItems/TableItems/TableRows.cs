using FlaUI.Core;
using System.Collections;
using System.Collections.Generic;

namespace TestStack.White.UIItems.TableItems
{
    public class TableRows : List<TableRow>
    {
        protected TableRows() {}        

        public TableRows(ICollection rowElements)
        {
            foreach (FrameworkAutomationElementBase automationElement in rowElements)
                Add(new TableRow(automationElement));
        }

        public virtual TableRow Get(string column, string value)
        {
            return Find(obj => obj.Cells[column].Value.Equals(value));
        }

        public virtual TableRows GetMultipleRows(string column, string value)
        {
            return new TableRows(FindAll(obj => obj.Cells[column].Value.Equals(value)));
        }
    }
}