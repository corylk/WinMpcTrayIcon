using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    public class FormBase : Form
    {
        protected MpcClient _mpc;

        protected ListBox _list;

        public FormBase()
        {
        }

        protected void InitComponents(string title, List<MenuItem> menuItems)
        {
            _mpc = new MpcClient();
            Text = title;
            ClientSize = new Size(800, 438);
            Padding = new Padding(2, 2, 2, 2);
            CenterToScreen();

            _list = new ListBox
            {
                Location = new Point(2, 27),
                Margin = new Padding(2, 2, 2, 2),
                MultiColumn = false,
                SelectionMode = SelectionMode.MultiExtended,
                ContextMenuStrip = new ContextMenu(menuItems).ToContextMenuStrip(),
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            };

            Controls.Add(_list);
            _list.Width = _list.Parent.Width - 20;
            _list.Height = _list.Parent.Height - 20;
        }

        protected void InitComponents()
        {
        }
    }
}
