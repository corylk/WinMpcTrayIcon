using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
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

            _mpc = new MpcClient();
            _menu = BuildMenu();

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

        private ContextMenuStrip BuildMenu()
        {
            var m = new ContextMenuStrip();

            var modes = new MenuItem("Playback").ToToolStripMenuItem();

            var modeCommands = new List<MenuItem>()
            {
                new MenuItem("Repeat", Command.repeat, false),
                new MenuItem("Random", Command.random, false),
                new MenuItem("Single", Command.single, false),
                new MenuItem("Consume", Command.consume, false),
            };

            AddCommands(modes.DropDownItems, modeCommands);
            m.Items.Add(modes);

            var commands = new List<MenuItem>()
            {
                new MenuItem("Update", Command.update, false),
                new MenuItem("Clear", Command.clear, false),
                new MenuItem("Stop", Command.stop, true),
                new MenuItem("Pause", Command.pause, true),
                new MenuItem("Play", Command.play, true),
                new MenuItem("Next ", Command.next, true),
                new MenuItem("Previous", Command.prev, true),
            };

            AddCommands(m.Items, commands);

            var status = new MenuItem("Status").ToToolStripMenuItem();
            status.Click += (sender, e) => ShowStatus();
            m.Items.Add(status);

            var exit = new MenuItem("Exit").ToToolStripMenuItem();
            exit.Click += (sender, e) => Exit();
            m.Items.Add(exit);

            return m;
        }

        private void AddCommands(ToolStripItemCollection items, List<MenuItem> commands)
        {
            foreach(var command in commands)
            {
                var i = command.ToToolStripMenuItem();
                i.Click += (sender, e) => MpcCommand(command.Command);
                items.Add(i);
            }
        }

        private void MpcCommand(Command cmd)
        {
            _mpc.Cmd(cmd);
        }

        private void ShowStatus()
        {
            _tray.ShowBalloonTip(8000, "mpc status", _mpc.GetInfo(), ToolTipIcon.Info);
        }

        private void OnClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                ShowStatus();
        }

        private void Exit()
        {
            this.ExitThread();
        }
    }
}
