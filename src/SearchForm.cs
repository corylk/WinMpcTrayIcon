using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    public class SearchForm : Form
    {
        private MpcClient _mpc;

        private TextBox _textBox;

        private ListBox _list;

        public SearchForm()
        {
            InitComponents();
        }

        private void InitComponents()
        {
            _mpc = new MpcClient();

            Text = "mpc search";
            ClientSize = new Size(800, 438);
            CenterToScreen();

            var menuItems = new List<MenuItem>()
            {
                new MenuItem("Add", new EventHandler(Add))
            };

            _textBox = new TextBox
            {
                Size = new Size(796, 25),
                Location = new Point(2, 2)
            };
            _textBox.KeyUp += (sender, e) => Search(sender, e, _textBox.Text);

            _list = new ListBox
            {
                Size = new Size(796, 422),
                Location = new Point(2, 27),
                MultiColumn = false,
                SelectionMode = SelectionMode.MultiExtended,
                ContextMenuStrip = new ContextMenu(menuItems).ToContextMenuStrip()
            };

            this.Controls.Add(_textBox);
            this.Controls.Add(_list);
        }

        private void Add(object sender, EventArgs e)
        {
            var selectedItems = _list.SelectedItems;

            foreach (string item in selectedItems)
            {
                _mpc.Cmd(Command.add, out string info, $"\"{item}\"");
            }
        }

        private void Search(object sender, KeyEventArgs e, string query)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _mpc.Cmd(Command.search, out string info, query);
                var results = info.Split("\r\n");
                _list.BeginUpdate();
                foreach (var result in results)
                {
                    _list.Items.Add(result);
                }
                _list.EndUpdate();
            }
        }
    }
}
