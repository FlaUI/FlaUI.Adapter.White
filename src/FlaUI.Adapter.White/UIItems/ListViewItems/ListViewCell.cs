using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Exceptions;

namespace TestStack.White.UIItems
{
    public class ListViewCell : DataGridViewCell
    {
        private readonly ListViewRow _listViewRow;

        public ListViewCell(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement) {
        }

        public ListViewCell(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public ListViewCell(ListViewRow listViewRow, FrameworkAutomationElementBase frameworkAutomationElement)
            : base(frameworkAutomationElement) 
        {
            _listViewRow = listViewRow;
        }

        public ListViewCell this[string text]
        {
            get
            {
                if (_listViewRow == null && string.IsNullOrEmpty(text))
                {
                    return this;
                }

                if (_listViewRow == null)
                {
                    throw new ElementNotAvailableException($"Cannot get cell for {text}");
                }
                var frameworkAutomation = _listViewRow.CellList.FirstOrDefault(x => x.Properties.Name.ValueOrDefault  != null && x.Properties.Name.ValueOrDefault.Contains(text))?.FrameworkAutomationElement;
                return frameworkAutomation != null ? new ListViewCell(frameworkAutomation) : null;
            }
        }

        public ListViewCell this[int columnIndex]
        {
            get
            {

                if (_listViewRow == null)
                {
                    throw new ElementNotAvailableException($"Cannot get cell for {columnIndex}");
                }

                return _listViewRow.Cells[columnIndex];
            }
        }

        public string Text => base.Properties.Name.ValueOrDefault;

        public int Count => _listViewRow.CellList.Count;

        public List<string> Select(Func<ListViewCell, string> selector) {
            return _listViewRow.CellList.Select(selector).ToList();
        }
    }
}
