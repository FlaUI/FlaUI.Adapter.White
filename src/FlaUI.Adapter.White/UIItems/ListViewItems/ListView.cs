using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Exceptions;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListViewItems;
using ControlType = FlaUI.Core.Definitions.ControlType;

namespace TestStack.White.UIItems
{
    public class ListView : DataGridView
    {
        public ListView(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {          
        }

        public ListView(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public new List<ListViewRow> Rows {
            get
            {
                var rows = new List<ListViewRow>();
                var collection = FindAllDescendants(SearchCriteria.ByControlType(ControlType.DataItem).ToCondition());
                foreach (var element in collection) 
                {
                    var row = Header != null ? 
                        new ListViewRow(element, new ListViewHeader(Header.FrameworkAutomationElement)) :
                        new ListViewRow(element);
                    rows.Add(row);
                }
                return rows;              
            }
        }

        public bool Enabled => IsEnabled;

        public Rectangle Bounds => BoundingRectangle;

        public virtual void Select(int zeroBasedRowIndex)
        {
            Rows[zeroBasedRowIndex].Select();
        }

        public virtual ListViewRows SelectedRows
        {
            get {
                var selectedValues = Patterns.Selection.Pattern.Selection.Value;
                return new ListViewRows(selectedValues.Select(x => new ListViewRow(x.FrameworkAutomationElement, new ListViewHeader(Header.FrameworkAutomationElement))).ToArray()); 
            }
        }

        public virtual void Select(string column, string value)
        {
            var row = Rows.FirstOrDefault(obj => obj.Cells[column].Text.Equals(value));           
            if (row == null)
            {
                throw new ElementNotAvailableException($"Can not find the row with column {column} and value {value}");
            }
            row.ScrollToRow();
            row.Select();
        }       

        public virtual ListViewCell Cell(string column, int zeroBasedRowIndex)
        {
            var row = Rows[zeroBasedRowIndex];
            var header = Header.Columns.FirstOrDefault(c => c.Name == column);
            var index = Array.IndexOf(Header.Columns, header);          
            return row.CellList[index];
        }
    }
}
