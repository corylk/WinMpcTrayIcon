using System.Drawing;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;

namespace WinMpcTrayIcon.Menu
{
    public static class MenuItemExtensions
    {
        public static ToolStripMenuItem ToToolStripMenuItem(this MenuItem command)
        {
            var i = new ToolStripMenuItem();
            i.Text = command.Text;

            if (command.GetType() == typeof(MpcMenuItem))
                i.Image = ((MpcMenuItem)command).Image != null ? Image.FromFile(((MpcMenuItem)command).Image) : null;

            return i;
        }
    }
}
