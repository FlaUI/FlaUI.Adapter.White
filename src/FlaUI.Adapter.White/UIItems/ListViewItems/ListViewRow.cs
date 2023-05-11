using System.Collections.Generic;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using TestStack.White.UIItems.ListViewItems;
using TestStack.White.WindowsAPI;

namespace TestStack.White.UIItems
{
    public class ListViewRow : DataGridViewRow
    {
        private readonly ListViewHeader _header;

        public ListViewRow(FrameworkAutomationElementBase frameworkAutomationElement, ListViewHeader header) : base(frameworkAutomationElement) 
        {
            _header = header;
        }

        public ListViewRow(AutomationElement automationElement, ListViewHeader header) : base(automationElement.FrameworkAutomationElement)
        {
            _header = header;
        }

        public ListViewRow(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public ListViewCells Cells
        {
            get
            {
                return new ListViewCells(base.Cells.Select(x => (AutomationElement)x).ToList(), _header);                
            }
        }

        public bool IsKeyboardFocusable => Properties.HasKeyboardFocus.ValueOrDefault;

        public void MultiSelect()
        {
            this.HoldKey(KeyboardInput.SpecialKeys.CONTROL);
            Select();
            this.LeaveKey(KeyboardInput.SpecialKeys.CONTROL);
        }

        public void Select()
        {
            this.ClickElement();                     
        }

        public string Id => Properties.AutomationId.ValueOrDefault;

        internal List<ListViewCell> CellList => base.Cells.Select(x => new ListViewCell(x.FrameworkAutomationElement)).ToList();     

        public virtual bool IsSelected
        {
            get {
                var selectedPattern = Patterns.SelectionItem.PatternOrDefault;
                return selectedPattern != null ? selectedPattern.IsSelected : false;
            }           
        }
    }
}
