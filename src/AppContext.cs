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

        public AppContext()
        {
            Application.ApplicationExit += this.ApplicationExitHandler;

            _mpc = new MpcClient();
            _mpc.Cmd(Command.status, out Status status);

            _tray = new NotifyIcon
            {
                Text = GetType().Namespace,
                Icon = GetIcon(status),
                ContextMenuStrip = GetContextMenu(),
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
            var switches = _mpc.GetToggles();

            var commands = new List<MenuItem>()
            {
                new GroupMenuItem("Playback", new List<MenuItem>()
                {
                    new MpcMenuItem("Repeat", (sender, e) => MpcToggle(Command.repeat), switches.Repeat),
                    new MpcMenuItem("Random", (sender, e) => MpcToggle(Command.random), switches.Random),
                    new MpcMenuItem("Single", (sender, e) => MpcToggle(Command.single), switches.Single),
                    new MpcMenuItem("Consume", (sender, e) => MpcToggle(Command.consume), switches.Consume),
                }),
                new MpcMenuItem("Update", (sender, e) => MpcCommand(Command.update)),
                new MpcMenuItem("Clear", (sender, e) => MpcCommand(Command.clear)),
                new MpcMenuItem("Stop", (sender, e) => MpcCommand(Command.stop), Command.stop),
                new MpcMenuItem("Pause", (sender, e) => MpcCommand(Command.pause), Command.pause),
                new MpcMenuItem("Play", (sender, e) => MpcCommand(Command.play), Command.play),
                new MpcMenuItem("Next ", (sender, e) => MpcCommand(Command.next), Command.next),
                new MpcMenuItem("Previous", (sender, e) => MpcCommand(Command.prev), Command.prev),
                new SysMenuItem("Status", new EventHandler(ShowStatus)),
                new SysMenuItem("Exit", new EventHandler(Exit))
            };

            return commands;
        }

        private ContextMenuStrip GetContextMenu()
        {
            var menuItems = GetMenuItems();
            return new ContextMenu(menuItems).ToContextMenuStrip();
        }

        private Icon GetIcon(Status status)
        {
            int action = Math.Min(2, 3 - (int)status);

            return new Icon(GetType(), $"Icons.ico.{(Command)action}.ico");
        }

        private void MpcCommand(Command cmd)
        {
            if (cmd == Command.play || cmd == Command.pause || cmd == Command.stop)
                _tray.Icon = GetIcon((Status)cmd);

            if (cmd == Command.toggle)
            {
                _mpc.Cmd(Command.toggle, out Status status);
                _tray.Icon = GetIcon(status);
            }
            else
            {
                _mpc.Cmd(cmd);
            }
        }

        private void MpcToggle(Command cmd)
        {
            _mpc.Cmd(cmd);
            _tray.ContextMenuStrip = GetContextMenu();
        }

        private void ShowStatus(object sender, EventArgs e)
        {
            _mpc.Cmd(Command.status, out string info);
            _tray.ShowBalloonTip(8000, "mpc status", info, ToolTipIcon.None);
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
