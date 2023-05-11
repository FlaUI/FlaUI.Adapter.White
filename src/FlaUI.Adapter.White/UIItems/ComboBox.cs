using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using TestStack.White.UIItems.Finders;
using ControlType = FlaUI.Core.Definitions.ControlType;

namespace TestStack.White.UIItems.ListBoxItems
{ 
    public class ComboBox : FlaUI.Core.AutomationElements.ComboBox
    {
        private readonly bool _isEditable;
        public ComboBox(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public ComboBox(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
        }

        public ComboBox(AutomationElement automationElement, bool isEditable) : base(automationElement.FrameworkAutomationElement)
        {
            _isEditable = isEditable;
        }

        public bool Enabled => IsEnabled;

        public bool IsFocussed => Properties.HasKeyboardFocus.ValueOrDefault;

        public string SelectedItemText {
            get
            {
                if(SelectedItem != null)
                {
                    return SelectedItem.Text;
                }
                else
                {
                    var valuePattern = Patterns.Value.PatternOrDefault;                  
                    if(valuePattern != null)
                    {
                        return valuePattern.Value;
                    }
                    if (!string.IsNullOrEmpty(Name))
                    {
                        return Name;
                    }                    
                    return HelpText;
                }
            }
        } 

        public Bitmap VisibleImage => Capture();

        public List<ComboBoxItem> Items {
            get
            {
                Expand();
                var items = FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                if (!items.Any())
                {
                    var comboLBoxElement = Retry.WhileNull(() =>
                    {                       
                        Expand();
                        var element =  Automation.GetDesktop().FindFirstChild(SearchCriteria.ByControlType(ControlType.List).AndByClassName("ComboLBox").ToCondition());
                        return element;
                    }, timeout: TimeSpan.FromSeconds(2));                 
                    if(comboLBoxElement.Result != null)
                    {
                        items = comboLBoxElement.Result.FindAllChildren(SearchCriteria.ByControlType(ControlType.ListItem).ToCondition());
                    }    
                }
                return items.Select(x => new ComboBoxItem(x.FrameworkAutomationElement)).ToList();
            }            
        }      
       
        public void SetValue(object value)
        {
            Select(value.ToString());
        }        

        public new ComboBoxItem Select(int index)
        {
            var foundItem = Items[index];
            foundItem.Select();
            return foundItem;
        }
       
        public new ComboBoxItem Select(string textToFind)
        {
            var foundItem = Items.FirstOrDefault(item => item.Text.Equals(textToFind));
            foundItem?.Select();
            return foundItem;
        }

        public new ComboBoxItem[] SelectedItems
        {
            get
            {
                // In WinForms, there is no selection pattern, so search the items which are selected.
                if (SelectionPattern == null)
                {
                    return Items.Where(x => x.IsSelected).ToArray();
                }
                return SelectionPattern.Selection.Value.Select(x => new ComboBoxItem(x.FrameworkAutomationElement)).ToArray();
            }
        }
        
        public new ComboBoxItem SelectedItem => SelectedItems?.FirstOrDefault();

        public new void Expand()
        {
            if (!Properties.IsEnabled.Value || ExpandCollapseState != FlaUI.Core.Definitions.ExpandCollapseState.Collapsed)
            {
                return;
            }

            if (FrameworkType == FrameworkType.WinForms)
            {
                if (_isEditable)
                {
                    var centerPoint = BoundingRectangle.Center();
                    centerPoint.X = BoundingRectangle.Right - 7;
                    Mouse.Click(centerPoint);
                }
                else
                {
                    // WinForms
                    var openButton = FindFirstChild(cf => cf.ByControlType(ControlType.Button));
                    openButton.ClickElement();
                }
            }
            else
            {
                // WPF
                var ecp = Patterns.ExpandCollapse.PatternOrDefault;
                ecp?.Expand();
            }
            // Wait a bit in case there is an open animation
            Thread.Sleep(AnimationDuration);
        }        
    }
}
