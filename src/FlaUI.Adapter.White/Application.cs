using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using FlaUI.Adapter.White;
using FlaUI.Core;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using FlaUI.Core.Exceptions;
using TestStack.White.UIItems;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using TestStack.White.Configuration;
using FlaUI.Core.Logging;

namespace TestStack.White
{
    public class Application : FlaUI.Core.Application
    {    
        private AutomationBase _automationBase;
        private Process _process;

        public Application(int processId, AutomationBase automationBase, bool isStoreApp = false) : base(processId, isStoreApp)
        {
            _automationBase = automationBase;
            _process = Process.GetProcessById(ProcessId);
            WhiteAdapter.Initialize(_automationBase);
        }

        public Application(Process process, AutomationBase automationBase, bool isStoreApp = false) : base(process, isStoreApp)
        {
            _automationBase = automationBase;
            _process = Process.GetProcessById(ProcessId);
            WhiteAdapter.Initialize(_automationBase);
        }

        public Process Process
        {
            get
            {
                if(_process == null)
                {
                    _process = Process.GetProcessById(ProcessId);
                }
                return _process;
            }   
        } 

        public virtual Window GetWindow(string title)
        {
            return Retry.WhileNull(() =>
            {
                var desktop = _automationBase.GetDesktop();
                var foundElement = desktop.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(ProcessId)).And(cf.ByName(title)));
                if (foundElement == null)
                {
                    return null;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
                foundElement.FocusElement();
                return new Window(foundElement);               
            }, timeout: ConfigurationExtensions.FindWindowTimeout(), throwOnTimeout: false, ignoreException: true).Result;           
        }

        public virtual Window GetWindow(SearchCriteria searchCriteria)
        {
            return Retry.WhileNull(() =>
            {
                var desktop = _automationBase.GetDesktop();
                if (desktop == null)
                {
                    return null;
                }
                var elementWindow = desktop.FindFirstDescendant(searchCriteria.AndProcessId(ProcessId).ToCondition());               
                return elementWindow != null ? new Window(elementWindow.FrameworkAutomationElement) : null;
            }, timeout: ConfigurationExtensions.FindWindowTimeout(), throwOnTimeout: false, ignoreException: true).Result;                     
        }

        public virtual Window GetWindow()
        {
            return Retry.WhileNull(() =>
            {
                var window = GetMainWindow(_automationBase);                
                return window != null ? new Window(window) : null;
            }, timeout: ConfigurationExtensions.FindWindowTimeout(), throwOnTimeout: false, ignoreException: true).Result;           
        }  

        public virtual IList<Window> GetWindows()
        {
            var desktop = _automationBase.GetDesktop();
            var foundElements = desktop.FindAllDescendants(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(ProcessId)));           
            return foundElements.Any() ? foundElements.Select(element => new Window(element)).ToList() : new List<Window>();
        } 

        public static Application Launch(ProcessStartInfo processStartInfo, AutomationBase automationBase)
        {
            var app = Launch(processStartInfo);
            return Instance(app, automationBase);
        }

        public static Application Launch(string executable, AutomationBase automationBase)
        {
            var app = Launch(executable);
            return Instance(app, automationBase);
        }

        public static Application Attach(Process process, AutomationBase automationBase)
        {
            var app = Attach(process);
            return Instance(app, automationBase);
        }

        public static Application Attach(string executable, AutomationBase automationBase)
        {  
            var processes = Process.GetProcessesByName(executable);
            if (processes.Length == 0) throw new FlaUIException("Could not find process named: " + executable);
            return Attach(processes[0], automationBase);
        }

        private static Application Instance(FlaUI.Core.Application app, AutomationBase automationBase)
        {
            return new Application(app.ProcessId, automationBase);
        }

        public new void Kill()
        {      
            try
            {
                Retry.WhileFalse(() => Close(killIfCloseFails: true), throwOnTimeout: false, ignoreException: true);
                if (Process == null || Process.HasExited)
                {
                    return;
                }
                Process.Kill();
                Process.WaitForExit();
                Process.Dispose();
            }
            catch (Exception exception)
            {
                Logger.Default.Error($"Failed Closing application '{GetType().FullName}'", exception);
            }
        }     
    }
}
