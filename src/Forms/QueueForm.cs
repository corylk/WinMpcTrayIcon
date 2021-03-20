using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon.Forms
{
    public class QueueForm : Form
    {
        private MpcClient _mpc;

        private ListPane _list;

        private SearchBox _searchBox;

        public QueueForm()
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
        }

        private void InitComponents(
            string title,
            List<MenuItem> menuItems)
        {
            _mpc = new MpcClient();

            Text = title;
            ClientSize = new Size(800, 438);
            Padding = new Padding(2, 2, 2, 2);
            var button = new Button();
            AcceptButton = button;
            CancelButton = button;
            CenterToScreen();

            _list = new ListPane(menuItems);
            _searchBox = new SearchBox();
            _searchBox.KeyUp += (sender, e) => Search(sender, e, _searchBox.Text);

            Controls.Add(_list);
            Controls.Add(_searchBox);

            _list.AutoResize();
            _searchBox.AutoResize();
            _searchBox.Focus();
            _searchBox.Select();

            GetPlaylist();
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
                e.SuppressKeyPress = true;
                _mpc.Cmd(Command.search, out string info, query);
                var results = info.Split("\r\n");
                UpdateList(results, true);
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                _searchBox.Text = null;
                GetPlaylist();
                e.Handled = true;
            }
        }

        private void GetPlaylist()
        {
            _mpc.Cmd(Command.playlist, out string info);
            var results = info.Split("\r\n");
            UpdateList(results, false);
        }

        private void UpdateList(string[] data, bool enabled)
        {
            _list.Items.Clear();
            _list.BeginUpdate();

            foreach (var i in data)
            {
                _list.Items.Add(i);
            }

            _list.EndUpdate();
            _list.Enabled = enabled;
        }
    }
}
