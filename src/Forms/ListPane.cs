using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;

namespace WinMpcTrayIcon.Forms
{
    public class ListPane : ListBox
    {
        public ListPane(List<MenuItem> menuItems)
        {
            Location = new Point(2, 27);
            Margin = new Padding(2, 2, 2, 2);
            MultiColumn = false;
            SelectionMode = SelectionMode.MultiExtended;
            ContextMenuStrip = new ContextMenu(menuItems).ToContextMenuStrip();
            Dock = DockStyle.Fill;
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        public void AutoResize()
        {
            Width = Parent.Width - 20;
            Height = Parent.Height - 65;
        }
    }
}
