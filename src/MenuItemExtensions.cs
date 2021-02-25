using System.Windows.Forms;

namespace WinMpcTrayIcon
{
    public static class MenuItemExtensions
    {
        public static ToolStripMenuItem ToToolStripMenuItem(this MenuItem item)
        {
            var i = new ToolStripMenuItem();
            i.Image = item.Image;
            i.Text = item.Text;
            i.Click += item.Command;
            return i;
        }
    }
}
