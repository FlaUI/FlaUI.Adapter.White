using System.Collections.Generic;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using TestStack.White.UIItems.Finders;
using System.Drawing;
using Mouse = TestStack.White.InputDevices.Mouse;

namespace TestStack.White.UIItems.TableItems
{
    public class TableRow : DataGridViewRow
    {
        private TableCell _cells;      

        public TableRow(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {            
        }

        public TableCell Cells
        {
            get
            {
                if (_cells == null)
                {
                    _cells = new TableCell(this, FrameworkAutomationElement);
                }

                return _cells;
            }
        }

        public string Id => Properties.AutomationId.ValueOrDefault;

        public List<TableCell> CellList => base.Cells.Select(x => new TableCell(x.FrameworkAutomationElement)).ToList();

        public bool Select()
        {
            this.ScrollToRow();
            var clickablePoint = GetRowClickablePoint();
            Mouse.Instance.Click(clickablePoint);       
            return !clickablePoint.IsEmpty;
        }

        public void DoubleClick()
        {
            this.ScrollToRow();
            new TooltipSafeMouse(Mouse.Instance).DoubleClickOutsideToolTip(this);          
        }       

        private Point GetRowClickablePoint()
        {
            var header = this.Get(SearchCriteria.ByControlType(ControlType.Header));

            return header?.BoundingRectangle.Center() ?? BoundingRectangle.Center();         
        }
    }
}
