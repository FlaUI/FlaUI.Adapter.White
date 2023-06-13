using System.Drawing;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Exceptions;

namespace TestStack.White.UIItems.TableItems
{
    public class TableCell : DataGridViewCell
    {
        private readonly TableRow _tableRow;
        public TableCell(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }
        public TableCell(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public TableCell(TableRow tableRow, FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
            _tableRow = tableRow;
        }

        public TableCell this[string columnName]
        {
            get
            {
                if (_tableRow == null && string.IsNullOrEmpty(columnName))
                {
                    return this;
                }

                if (_tableRow == null)
                {
                    throw  new ElementNotAvailableException($"Cannot get cell for {columnName}");
                }

                var frameworkAutomation = _tableRow.CellList.FirstOrDefault(x => x.Properties.Name.ValueOrDefault != null && x.Properties.Name.ValueOrDefault.Contains(columnName));
                return frameworkAutomation != null ? new TableCell(frameworkAutomation) : null;
            }
        }

        public TableCell this[int columnIndex]
        {
            get
            {                
                if (_tableRow == null)
                {
                    throw new ElementNotAvailableException($"Cannot get cell for {columnIndex}");
                }

                return _tableRow.CellList[columnIndex];
            }
        }

        public bool IsFocussed => Properties.HasKeyboardFocus.ValueOrDefault;

        public Rectangle Bounds => BoundingRectangle;

        public int Count => _tableRow.CellList.Count;

        public object Value
        {
            get
            {
                return base.Value == "(null)" ? string.Empty : base.Value;
            }
            set
            {
                base.Value = value.ToString();
            }        
        }  
    }
}
