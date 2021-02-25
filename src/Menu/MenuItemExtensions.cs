using System.Drawing;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;

namespace WinMpcTrayIcon.Menu
{
    public static class MenuItemExtensions
    {
        public static ToolStripMenuItem ToToolStripMenuItem(this MpcMenuItem command)
        {
            var i = new ToolStripMenuItem
            {
                Image = command.Image != null ? Image.FromFile(command.Image) : null,
                Text = command.Text
            };

            return i;
        }

        public static ToolStripMenuItem ToToolStripMenuItem(this SwitchMenuItem command)
        {
            var i = new ToolStripMenuItem
            {
                Image = command.Image != null ? Image.FromFile(command.Image) : null,
                Text = command.Text
            };

            return i;
        }

        public static ToolStripMenuItem ToToolStripMenuItem(this MenuItem command)
        {
            var i = new ToolStripMenuItem
            {
                Text = command.Text
            };

            return i;
        }
    }
}
