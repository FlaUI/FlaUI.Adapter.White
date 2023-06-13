using System.Threading;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using TestStack.White.UIItems.Finders;

namespace TestStack.White.UIItems.WindowStripControls
{
    public class MenuBar : Menu
    {
        public MenuBar(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        public bool Enabled => IsEnabled;

        public MenuItem MenuItem(params string[] path)
        {
            var subMenus = Items;
            for (var i = 0; i < path.Length; i++)
            {               
                var targetMenuItem = subMenus.Find(x => x.Properties.Name == path[i]);
                if (targetMenuItem == null)
                {
                    break;
                }
                if (i == path.Length - 1)
                {
                    return targetMenuItem;
                }
                targetMenuItem.ClickElement();
                Thread.Sleep(250);
                subMenus = targetMenuItem.Items;
            }
            return null;
        }

        public virtual Menu MenuItemBy(SearchCriteria searchCriteria)
        {   
            return this.Get<Menu>(searchCriteria);
        }

    }
}
