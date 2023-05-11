using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using TestStack.White.UIItems.Finders;

namespace TestStack.White.UIItems.ListViewItems
{
    public class ListViewHeader : DataGridViewHeader
    {        
        public ListViewHeader(FrameworkAutomationElementBase automationElement) : base(automationElement)
        {            
        }

        public virtual ListViewColumns Columns
        {
            get
            {  
                var collection =  this.GetChildren<AutomationElement>(SearchCriteria.ByControlType(FlaUI.Core.Definitions.ControlType.HeaderItem));
                return new ListViewColumns(collection);
            }
        }

        public virtual ListViewColumn Column(string text)
        {
            return Columns.Find(delegate(ListViewColumn column) { return column.Text.Equals(text); });
        }
    }
}