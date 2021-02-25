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

            _tray = new NotifyIcon
            {
                Text = GetType().Namespace,
                Icon = new Icon(GetType(), "Icons.ico.pauseplay.ico"),
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
                    new SwitchMenuItem("Repeat", (sender, e) => MpcToggle(Command.repeat), switches.Repeat),
                    new SwitchMenuItem("Random", (sender, e) => MpcToggle(Command.random), switches.Random),
                    new SwitchMenuItem("Single", (sender, e) => MpcToggle(Command.single), switches.Single),
                    new SwitchMenuItem("Consume", (sender, e) => MpcToggle(Command.consume), switches.Consume),
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

        private void MpcCommand(Command cmd)
        {
            _mpc.Cmd(cmd);
        }

        private void MpcToggle(Command cmd)
        {
            _mpc.Cmd(cmd);
            _tray.ContextMenuStrip = GetContextMenu();
        }

        private void ShowStatus(object sender, EventArgs e)
        {
            _tray.ShowBalloonTip(8000, "mpc status", _mpc.GetInfo(), ToolTipIcon.None);
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
