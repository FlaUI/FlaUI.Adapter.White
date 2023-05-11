using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using FlaUI.Adapter.White;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Exceptions;
using FlaUI.Core.Tools;
using TestStack.White.Configuration;
using TestStack.White.InputDevices;
using TestStack.White.UIA;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowStripControls;

namespace TestStack.White.UIItems.WindowItems
{
    public class Window : FlaUI.Core.AutomationElements.Window, IDisposable
    {
        private readonly int? _processId;

        public Window(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
            WhiteAdapter.Initialize(Automation);
            _processId = GetCurrentProcessId();
        }

        public Window(AutomationElement automationElement) : base(automationElement.FrameworkAutomationElement)
        {
            WhiteAdapter.Initialize(Automation);
            _processId = GetCurrentProcessId();
        }

        public MenuBar MenuBar
        {
            get
            {             
                var element = FindAllDescendants(cf => cf.Menu()).FirstOrDefault(cf => !"System".Equals(cf.Name, StringComparison.CurrentCultureIgnoreCase));
                if(element == null)
                {
                    return null;
                }
                var menu = element.AsMenu();
                if (menu == null)
                {
                    return null;
                }
                return new MenuBar(menu.FrameworkAutomationElement);
            }
        }

        public IList<MenuItem> MenuBars => MenuBar.Items;

        public Rectangle Bounds => BoundingRectangle;

        public T Get<T>(SearchCriteria searchCriteria) where T : AutomationElement
        {
            var element = this.Get(searchCriteria.AndControlType(typeof(T)));
            return SearchControlFactory.CreateForControl<T>(element);
        }

        public T GetByXPath<T>(string path) where T : AutomationElement
        {
            var element = FindFirstByXPath(path);
            return SearchControlFactory.CreateForControl<T>(element);
        }

        public T GetChild<T>(SearchCriteria searchCriteria) where T : AutomationElement
        {
            var element = FindFirstChild(searchCriteria.AndControlType(typeof(T)).ToCondition());
            return SearchControlFactory.CreateForControl<T>(element);
        }

        public T Get<T>(string automationId) where T : AutomationElement
        {
            return Get<T>(SearchCriteria.ByAutomationId(automationId));
        }

        public IList<T> GetMultiple<T>(SearchCriteria searchCriteria) where T : AutomationElement
        {
            var elements = FindAllDescendants(searchCriteria.ToCondition());
            return SearchControlFactory.CreateByControls<T>(elements);
        }        

        public virtual Keyboard Keyboard => Keyboard.Instance;

        public DisplayState DisplayState
        {
            get => FrameworkAutomationElement.Patterns.Window.Pattern.WindowVisualState.Value.ConvertToDisplayState();
            set => FrameworkAutomationElement.Patterns.Window.Pattern.SetWindowVisualState(value.ConvertToWindowVisualState());
        }

        /// <summary>
        /// Returns a mouse which is associated to this window. 
        /// Any operation performed using the mouse would wait till the window is busy after this operation. 
        /// Before any operation is performed the window is brought to focus.
        /// </summary>
        public virtual Mouse Mouse => Mouse.Instance;

        public bool IsOffScreen => base.IsOffscreen;

        public bool Visible => !base.IsOffscreen;

        public AutomationType AutomationType => base.AutomationType;

        public bool IsClosed
        {
            get
            {
                try
                {
                    return IsOffscreen;
                }
                catch (ElementNotAvailableException)
                {
                    return true;
                }

                catch (InvalidOperationException)
                {
                    return true;
                }
                catch (COMException)
                {
                    return true;
                }
            }
        }      

        public string Id => base.Properties.AutomationId.ValueOrDefault;

        public Window MessageBox(string title)
        {
            var framework = base.ModalWindows.FirstOrDefault(x => x.Name == title)?.FrameworkAutomationElement ??
                this.GetChildren<AutomationElement>(SearchCriteria.ByControlType(ControlType.Window))
                    .FirstOrDefault(x => x.Name == title)?.FrameworkAutomationElement;
            return framework == null ? null : new Window(framework);
        }              

        public virtual void ReloadIfCached()
        {
            CacheRequest.ForceNoCache();           
        }

        internal static Window Instance(FrameworkAutomationElementBase frameworkAutomationElement)
        {
            return new Window(frameworkAutomationElement);
        }

        public void Dispose()
        {
            Close();
        }

        public Window ModalWindow(string title)
        {
            return Retry.WhileNull(() => GetWindow(title), timeout: ConfigurationExtensions.FindWindowTimeout(), throwOnTimeout: false).Result;            
        }

        private Window GetWindow(string title)
        {           
            var desktop = Automation.GetDesktop();
            var foundElement = _processId.HasValue ? desktop.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_processId.Value)).And(cf.ByName(title))) :
                                                    desktop.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName(title)));
            return foundElement != null ? new Window(foundElement) : null;
        }       

        public virtual IList<TabItems.Tab> Tabs { 
            get {
                var tabs = new List<TabItems.Tab>();
                foreach(var tab in FindAllDescendants(SearchCriteria.ByControlType(ControlType.Tab).ToCondition()))
                {
                    tabs.Add(new TabItems.Tab(tab.FrameworkAutomationElement));
                }
                return tabs;
            }
        }        

        public Menu PopupMenu(params string[] paths)
        {            
            var parentMenu = TopPopupMenu;
            for(var i = 0; i < paths.Length; i++)
            {              
                var subMenu = GetSubPopupMenu(parentMenu, paths[i]);
                if(subMenu == null)                {
                    throw new ElementNotAvailableException("Could not find Menu " + paths[i]);
                }
                if(i == paths.Length - 1)
                {
                    return subMenu;
                }

                subMenu.Click();
                parentMenu = subMenu;              
            }
            return parentMenu;
        }

        public Menu TopPopupMenu
        {
            get
            {
                if (Popup == null)
                {
                    throw new ElementNotAvailableException("Could not find Popup Menu");
                }              
                var popupChildren = Popup.FindAllChildren();
                if (popupChildren == null || popupChildren.Length == 0)
                {
                    throw new ElementNotAvailableException("Could not find Popup Menu");
                }
                return popupChildren[0].AsMenu();
            }
        }

        private Menu GetSubPopupMenu(Menu parent, string menuText)
        {
            foreach(var subMenu in parent.Items)
            {
                if(subMenu.Text == menuText)
                {                   
                    return subMenu.AsMenu();
                }
            }
            return null;
        }

        public UIItemContainer MdiChild(SearchCriteria searchCriteria)
        {
            var element = Retry.WhileNull(() => FindFirstDescendant(searchCriteria.ToCondition()), timeout: ConfigurationExtensions.FindWindowTimeout(), throwOnTimeout: false).Result;           
            return element != null ? new UIItemContainer(element.FrameworkAutomationElement) : null;
        }

        public Window ModalWindow(SearchCriteria searchCriteria)
        {
            return Retry.WhileNull(() =>
            {
                return Get<Window>(searchCriteria);
            }, timeout: ConfigurationExtensions.FindWindowTimeout(), throwOnTimeout: false).Result;           
        }

        public virtual bool IsCurrentlyActive
        {
            get
            {          
                return AutomationElementCollectionX.Contains(FindAll(TreeScope.Descendants | TreeScope.Element, TrueCondition.Default), 
                    element => element.FrameworkAutomationElement.HasKeyboardFocus && !element.ControlType.Equals(ControlType.Custom));
            }
        }

        public virtual AutomationElement[] Items => FindAllDescendants();

        public void WaitForWindow()
        {
            var busyTimeout = TimeSpan.FromSeconds(30);
            var windowPattern = Patterns.Window.PatternOrDefault;
            if (windowPattern != null && !windowPattern.WaitForInputIdle((int)busyTimeout.TotalMilliseconds))
            {
                throw new Exception(string.Format("Timeout occured, after waiting for {0} ms", (int)busyTimeout.TotalMilliseconds));
            }
            if (windowPattern == null) return;
            var finalState = Retry.While(
                () => windowPattern.WindowInteractionState,
                windowState => windowState == WindowInteractionState.NotResponding,
                busyTimeout, throwOnTimeout: true);
        }

        private int? GetCurrentProcessId()
        {
            try
            {
                return FrameworkAutomationElement.ProcessId;
            }
            catch 
            {
                return Properties.ProcessId.TryGetValue(out int processId) ? processId : (int?)null;
            }
        } 

        public void WaitForProcess()
        {
            if (!_processId.HasValue)
            {
                return;
            }
            Process.GetProcessById(_processId.Value).WaitForInputIdle();
        }

        public void WaitWhileBusy()
        {
            try
            {
                WaitForProcess();
                WaitForWindow();
            }
            catch (Exception e)
            {
                if (!(e is InvalidOperationException || e is ElementNotAvailableException))
                    throw new FlaUIException(string.Format("Window didn't respond, after waiting for {0} ms"), e);
            }
        }        

        public bool IsReadyForUserInteraction
        {
            get
            {
                var windowPattern = Patterns.Window.PatternOrDefault;       
                return windowPattern != null && windowPattern.WindowInteractionState.ValueOrDefault == WindowInteractionState.ReadyForUserInteraction;
            }   
        }    
    }
}