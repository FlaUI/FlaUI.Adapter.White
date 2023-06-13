using FlaUI.Core.Definitions;
using TestStack.White.UIItems.WindowItems;

namespace TestStack.White.UIItems
{    public static partial class WhiteAdapterExtensions
    {
        public static DisplayState ConvertToDisplayState(this WindowVisualState windowVisualState)
        {
            DisplayState displayState;
            switch (windowVisualState)
            {
                case WindowVisualState.Maximized:
                    displayState = DisplayState.Maximized;
                    break;
                case WindowVisualState.Minimized:
                    displayState = DisplayState.Minimized;
                    break;
                default:
                    displayState = DisplayState.Restored;
                    break;
            }

            return displayState;
        }

        public static WindowVisualState ConvertToWindowVisualState(this DisplayState displayState)
        {
            WindowVisualState windowVisualState;
            switch (displayState)
            {
                case DisplayState.Maximized:
                    windowVisualState = WindowVisualState.Maximized;
                    break;
                case DisplayState.Minimized:
                    windowVisualState = WindowVisualState.Minimized;
                    break;
                default:
                    windowVisualState = WindowVisualState.Normal;
                    break;
            }

            return windowVisualState;
        }
    }
}
