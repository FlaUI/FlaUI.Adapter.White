using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace TestStack.White.UIItems.TableItems
{
    public class Table : DataGridView
    {
        private List<TableRow> _rows;
        public Table(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public Table(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public bool Enabled => IsEnabled;

        public bool Visible => !IsOffscreen;

        public Bitmap VisibleImage => Capture();

        public new List<TableRow> Rows
        {
            get
            {
                if(_rows == null)
                {
                    _rows = base.Rows.Select(row => new TableRow(row.FrameworkAutomationElement)).ToList();
                }
                return _rows;
            }
        }

        public void Refresh()
        {
            _rows = null;
        }

        public virtual TableRow Row(string column, string value)
        {        
            return Rows.FirstOrDefault(obj => obj.Cells[column].Value.Equals(value));
        }

        public virtual TableRow Get(string column, string value)
        {
            return Rows.FirstOrDefault(obj => obj.Cells[column].Value.Equals(value));
        }

        public virtual List<TableRow> GetMultipleRows(string column, string value)
        {
            return Rows.Where(obj => obj.Cells[column].Value.Equals(value)).ToList();
        }      
    }
}
