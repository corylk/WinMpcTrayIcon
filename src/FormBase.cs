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

        protected TextBox _textBox;

        protected ListBox _list;

        public FormBase()
        {
        }

        protected void InitComponents(string title, List<MenuItem> menuItems, KeyEventHandler searchFunction)
        {
            _mpc = new MpcClient();

            _textBox = new TextBox
            {
                Size = new Size(796, 25),
                Location = new Point(2, 2)
            };
            _textBox.KeyUp += searchFunction;

            _list = new ListBox
            {
                Size = new Size(796, 422),
                Location = new Point(2, 27),
                MultiColumn = false,
                SelectionMode = SelectionMode.MultiExtended,
                ContextMenuStrip = new ContextMenu(menuItems).ToContextMenuStrip()
            };

            Text = title;
            ClientSize = new Size(800, 438);
            CenterToScreen();
            Controls.Add(_textBox);
            Controls.Add(_list);
        }
    }
}
