using FlaUI.Core.AutomationElements;
namespace TestStack.White.UIItems.ListViewItems
{
    public class ListViewColumn : DataGridViewHeaderItem
    {
        private readonly int _index;     

        public ListViewColumn(AutomationElement automationElement, int index) : base(automationElement.FrameworkAutomationElement)
        {
            this._index = index;
        }

        public virtual int Index
        {
            get { return _index; }
        }

        public virtual string Text
        {
            get { return Name; }
        }
    }
}