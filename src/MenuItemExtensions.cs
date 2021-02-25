using System.Drawing;
using System.Windows.Forms;

namespace WinMpcTrayIcon
{
    public static class MenuItemExtensions
    {
        public static ToolStripMenuItem ToToolStripMenuItem(this MenuItem command)
        {
            var i = new ToolStripMenuItem
            {
                Image = command.HasImage ? Image.FromFile($"Icons/png/{command.Command}.png") : null,
                Text = command.Text
            };

            return i;
        }
    }
}
