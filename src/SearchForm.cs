using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    public class SearchForm : FormBase
    {
        private readonly TextBox _textBox;

        public SearchForm()
        {
            InitComponents(
                title: "mpc search",
                menuItems: new List<MenuItem>()
                {
                    new MenuItem("Add", new EventHandler(Add)),
                    new MenuItem("Play next", new EventHandler(Insert)),
                    new MenuItem("Crop and play", (sender, e) => AddAnd(Command.crop)),
                    new MenuItem("Clear and play", (sender, e) => AddAnd(Command.clear))
                });

            _textBox = new TextBox
            {
                Location = new Point(2, 2),
                Margin = new Padding(2, 2, 2, 2),
                Anchor = AnchorStyles.Top | AnchorStyles.Right |  AnchorStyles.Left
            };
            _textBox.KeyUp += (sender, e) => Search(sender, e, _textBox.Text);

            Controls.Add(_textBox);
            _list.Height = _list.Parent.Height - 65;
            _textBox.Width = _textBox.Parent.Width - 20;
            _textBox.Focus();
            _textBox.Select();
        }

        private void Add(object sender, EventArgs e)
        {
            var selectedItems = _list.SelectedItems;

            foreach (string item in selectedItems)
            {
                _mpc.Cmd(Command.add, args: $"\"{item}\"");
            }
        }

        private void Insert(object sender, EventArgs e)
        {
            var selectedItems = _list.SelectedItems.Cast<string>().ToList();
            selectedItems.Reverse();

            foreach (string item in selectedItems)
            {
                _mpc.Cmd(Command.insert, args: $"\"{item}\"");
            }
        }

        private void AddAnd(Command cmd)
        {
            var selectedItems = _list.SelectedItems;
            _mpc.Cmd(cmd);

            foreach (string item in selectedItems)
            {
                _mpc.Cmd(Command.add, args: $"\"{item}\"");
            }

            _mpc.Cmd(Command.play); // need to refresh icons
        }

        private void Search(object sender, KeyEventArgs e, string query)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _mpc.Cmd(Command.search, out string info, query);
                var results = info.Split("\r\n");
                _list.Items.Clear();
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
