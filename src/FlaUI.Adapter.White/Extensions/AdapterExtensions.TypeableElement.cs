using System.Threading;
using FlaUI.Core.AutomationElements;
using TestStack.White.WindowsAPI;
using System.Linq;
using TestStack.White.InputDevices;
using Wait = FlaUI.Core.Input.Wait;

namespace TestStack.White.UIItems
{
    public static partial class AdapterExtensions
    {    
        public static void Enter<T>(this T element, string text) where T : AutomationElement
        {
            EnterData(element, text);
        }        

        public static void Enter(this AutomationElement element, string text)
        {
            EnterData(element, text);
        }

        private static void EnterData(AutomationElement element, string text)
        {
            var textBox = element.AsTextBox();
            if (textBox != null)
            {
                textBox.Enter(text);
                return;
            }

            element.FocusElement();
            var valuePattern = element.Patterns.Value.PatternOrDefault;
            if (valuePattern != null && !valuePattern.IsReadOnly)
            {
                valuePattern.SetValue(string.Empty);
            }
            if (string.IsNullOrEmpty(text)) return;

            string[] strArray = text.Replace("\r\n", "\n").Split('\n');
            Keyboard.Instance.Enter(strArray[0]);
            foreach (string value in strArray.Skip(1))
            {
                Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
                Keyboard.Instance.Enter(value);
            }

            Wait.UntilInputIsProcessed();
        }

        public static void KeyIn<T>(this T element, KeyboardInput.SpecialKeys key) where T : AutomationElement
        {
            if (element.GetType().IsSubclassOf(typeof(AutomationElement)) && key == KeyboardInput.SpecialKeys.DELETE)
            {
                element.FocusElement();
            }
            Keyboard.Instance.Pressing(key);
            Thread.Sleep(5000);
        }
      
        public static void HoldKey<T>(this T element, KeyboardInput.SpecialKeys key) where T : AutomationElement
        {
            Keyboard.Instance.HoldKey(key);
        }
      
        public static void LeaveKey<T>(this T element, KeyboardInput.SpecialKeys key) where T : AutomationElement
        {
            Keyboard.Instance.LeaveKey(key);
        }
    }
}