using System.Collections.Generic;
using FlaUI.Core.WindowsAPI;
using TestStack.White.WindowsAPI;
using FlaUIKeyboard = FlaUI.Core.Input.Keyboard;

namespace TestStack.White.InputDevices
{
    public class Keyboard
    {
        public static Keyboard Instance => new Keyboard();
        public static List<KeyboardInput.SpecialKeys> HeldKeys = new List<KeyboardInput.SpecialKeys>();

        public void LeaveAllKeys()
        {
            new List<KeyboardInput.SpecialKeys>(HeldKeys).ForEach(LeaveKey);
        }

        public void LeaveKey(KeyboardInput.SpecialKeys key)
        {
            FlaUIKeyboard.Release((VirtualKeyShort)key);
            HeldKeys.Remove(key);
        }

        public void HoldKey(KeyboardInput.SpecialKeys key)
        {
            FlaUIKeyboard.Press((VirtualKeyShort)key);
            HeldKeys.Add(key);
        }

        public virtual void PressSpecialKey(KeyboardInput.SpecialKeys key)
        {
            FlaUIKeyboard.Type((VirtualKeyShort)key);
        }

        public virtual void Enter(string text)
        {
            FlaUIKeyboard.Type(text);
        }

        public virtual void Type(params VirtualKeyShort[] virtualKeys)
        {
            FlaUIKeyboard.Type(virtualKeys);
        }

        public void PressSpecialKey<T>(KeyboardInput.SpecialKeys key)
        {
            FlaUIKeyboard.Pressing((VirtualKeyShort)key);
        }

        public void Pressing(KeyboardInput.SpecialKeys key)
        {
            FlaUIKeyboard.Pressing((VirtualKeyShort)key);
        }

    }
}