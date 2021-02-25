using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinMpcTrayIcon.Menu;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    public class AppContext : ApplicationContext
    {
        private readonly MpcClient _mpc;

        private readonly NotifyIcon _tray;

        private readonly ContextMenuStrip _menu;

        public AppContext()
        {
            Application.ApplicationExit += this.ApplicationExitHandler;

            var menuItems = GetMenuItems();
            _mpc = new MpcClient();
            _menu = new ContextMenu(menuItems).ToContextMenuStrip();

            _tray = new NotifyIcon
            {
                Text = "WinMpcTrayIcon",
                Icon = new Icon(GetType(), "Icons.ico.pauseplay.ico"),
                ContextMenuStrip = _menu,
                Visible = true
            };

            _tray.MouseDoubleClick += (sender, e) => MpcCommand(Command.toggle);
            _tray.MouseClick += (sender, e) => OnClick(sender, e);
        }

        protected virtual void OnApplicationExit(EventArgs e)
        {
        }

        private void ApplicationExitHandler(object sender, EventArgs e)
        {
            if (_tray != null)
            {
                _tray.Visible = false;
                _tray.Dispose();
            }

            this.OnApplicationExit(e);
        }

        private List<MenuItem> GetMenuItems()
        {
            var commands = new List<MenuItem>()
            {
                new GroupMenuItem("Playback", new List<MenuItem>()
                {
                    new MpcMenuItem("Repeat", Command.repeat, false),
                    new MpcMenuItem("Random", Command.random, false),
                    new MpcMenuItem("Single", Command.single, false),
                    new MpcMenuItem("Consume", Command.consume, false),
                }),
                new MpcMenuItem("Update", Command.update, false),
                new MpcMenuItem("Clear", Command.clear, false),
                new MpcMenuItem("Stop", Command.stop, true),
                new MpcMenuItem("Pause", Command.pause, true),
                new MpcMenuItem("Play", Command.play, true),
                new MpcMenuItem("Next ", Command.next, true),
                new MpcMenuItem("Previous", Command.prev, true),
                new SysMenuItem("Status", new EventHandler(ShowStatus)),
                new SysMenuItem("Exit", new EventHandler(Exit))
            };

            return commands;
        }

        private void MpcCommand(Command cmd)
        {
            _mpc.Cmd(cmd);
        }

        private void ShowStatus(object sender, EventArgs e)
        {
            _tray.ShowBalloonTip(8000, "mpc status", _mpc.GetInfo(), ToolTipIcon.Info);
        }

        private void OnClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                ShowStatus(sender, e);
        }

        private void Exit(object sender, EventArgs e)
        {
            this.ExitThread();
        }
    }
}
