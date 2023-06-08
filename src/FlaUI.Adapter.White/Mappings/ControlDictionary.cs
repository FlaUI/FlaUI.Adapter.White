using System;
using System.Linq;
using FlaUI.Core.Definitions;
using FlaUI.Core.Exceptions;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;

namespace TestStack.White.Mappings
{
    public class ControlDictionary
    {
        public static readonly ControlDictionary Instance = new ControlDictionary();
        private readonly ControlDictionaryItems items = new ControlDictionaryItems();

        private ControlDictionary()
        {
            items.AddPrimary(typeof(Button), ControlType.Button);
            items.AddPrimary(typeof(CheckBox), ControlType.CheckBox);
            items.AddPrimary(typeof(ListBox), ControlType.List);
            items.AddPrimary(typeof(Tree), ControlType.Tree);
            items.AddPrimary(typeof(RadioButton), ControlType.RadioButton);
            items.AddPrimary(typeof(Table), ControlType.Table);
            items.AddPrimary(typeof(Tab), ControlType.Tab);
            items.AddPrimary(typeof(ListView), ControlType.DataGrid);
            items.AddPrimary(typeof(ToolStrip), ControlType.ToolBar);           
            items.AddPrimary(typeof(TextBox), ControlType.Edit);
            items.AddPrimary(typeof(MenuBar), ControlType.MenuBar);
            items.AddPrimary(typeof(Panel), ControlType.Pane);
            items.AddPrimary(typeof(DateTimePicker), ControlType.Edit);
            items.AddPrimary(typeof(ComboBox), ControlType.ComboBox);
            items.AddPrimary(typeof(Label), ControlType.Text);
            items.AddPrimary(typeof(TextBox), ControlType.Document);
            items.AddSecondary(typeof(TableRow), ControlType.Custom);
            items.AddSecondary(typeof(ListViewRow), ControlType.DataItem);
            items.AddSecondary(typeof(Window), ControlType.Window);
           
        }

        public virtual ControlType[] GetControlType(Type testControlType)
        {
            var controlDictionaryItem = items.FindBy(testControlType);
            if (controlDictionaryItem == null)
                throw new ElementNotAvailableException($"Cannot find {testControlType.Name} for {nameof(ControlDictionary)}.");
            return controlDictionaryItem.Select(c => c.ControlType).ToArray();
        }
    }
}
