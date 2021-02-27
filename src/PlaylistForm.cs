using System;
using System.Collections.Generic;
using System.Drawing;
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
                menuItems: new List<MenuItem>());

            GetPlaylist();
            _list.Location = new Point(2, 2);
            _list.Height = _list.Parent.Height - 40;
            _list.Enabled = false;
        }

        private void GetPlaylist()
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
