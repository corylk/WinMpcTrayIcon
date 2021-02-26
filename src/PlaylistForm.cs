using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    public class PlaylistForm : FormBase
    {
        public PlaylistForm()
        {
            InitComponents(
                title: "mpc playlist",
                menuItems: new List<MenuItem>(),
                searchFunction: Search);
        }

        private void Search(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _mpc.Cmd(Command.playlist, out string info);
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
