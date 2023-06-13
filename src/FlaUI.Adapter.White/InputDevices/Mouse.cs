using FlaUI.Core.WindowsAPI;
using System.Drawing;
using System.Runtime.InteropServices;
using FlaUIMouse = FlaUI.Core.Input.Mouse;

namespace TestStack.White.InputDevices
{
    public class Mouse
    {
        private const int HCURSOR_ARROW_CURSOR = 65541;
        private const int HCURSOR_INPUT_CURSOR = 65543;
        private const int HCURSOR_WAITTING_CURSOR = 65545;        

        public static Mouse Instance => new Mouse();

        public void DoubleClick(Point clickPoint)
        {
            FlaUIMouse.DoubleClick(clickPoint);
        }

        public void RightClick()
        {
            FlaUIMouse.RightClick();
        }

        public void RightClick(Point clickPoint)
        {
            FlaUIMouse.RightClick(clickPoint);
        }

        public void LeftClick()
        {
            FlaUIMouse.LeftClick();
        }

        public void LeftClick(Point clickPoint)
        {
            FlaUIMouse.LeftClick(clickPoint);
        }

        public void Click(Point clickPoint)
        {
            FlaUIMouse.LeftClick(clickPoint);
        }

        public void RightDoubleClick(Point clickPoint)
        {
            FlaUIMouse.RightDoubleClick(clickPoint);
        }

        public void LeftDoubleClick(Point clickPoint)
        {
            FlaUIMouse.LeftDoubleClick(clickPoint);
        }

        public void MoveTo(Point clickPoint)
        {
            FlaUIMouse.MoveTo(clickPoint);
        }

        public virtual Point Location
        {
            get
            {
                return FlaUIMouse.Position;
            }
            set
            {
                FlaUIMouse.Position = value;
            }
        }

        public bool IsCursorShowing(int? clickableX, int? clickableY)
        {
            var cursorInfo = new CURSORINFO();
            cursorInfo.cbSize = Marshal.SizeOf(cursorInfo);
            if (!User32.GetCursorInfo(out cursorInfo))
            {
                if (clickableX.HasValue && clickableY.HasValue)
                {
                    FlaUIMouse.MoveTo(clickableX.Value, clickableY.Value);
                }
                return true;
            }
            return cursorInfo.hCursor.ToInt32() == HCURSOR_WAITTING_CURSOR;
        }
    }
}
