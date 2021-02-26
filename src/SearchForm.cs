using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    public class SearchForm : FormBase
    {
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
                },
                searchFunction: (sender, e) => Search(sender, e, _textBox.Text));
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

            _mpc.Cmd(Command.play); // need to update icons
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
