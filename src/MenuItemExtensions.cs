using System.Drawing;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;

namespace WinMpcTrayIcon
{
    public static class MenuItemExtensions
    {
        public static ToolStripMenuItem ToToolStripMenuItem(this MpcMenuItem command)
        {
            var i = new ToolStripMenuItem
            {
                Image = command.HasImage ? Image.FromFile($"Icons/png/{command.Command}.png") : null,
                Text = command.Text
            };

            return i;
        }

        public static ToolStripMenuItem ToToolStripMenuItem(this GroupMenuItem command)
        {
            var i = new ToolStripMenuItem
            {
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
