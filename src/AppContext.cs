using System;
using System.Collections.Generic;
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
                Icon = status.GetIcon(),
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
            var playModeStatus = _mpc.GetPlaymodeStatus();

            var menuItems = new List<MenuItem>()
            {
                new MenuItem("Playback", new List<MenuItem>()
                {
                    new MenuItem("Repeat", (sender, e) => MpcCommand(Command.repeat), playModeStatus.Repeat),
                    new MenuItem("Random", (sender, e) => MpcCommand(Command.random), playModeStatus.Random),
                    new MenuItem("Single", (sender, e) => MpcCommand(Command.single), playModeStatus.Single),
                    new MenuItem("Consume", (sender, e) => MpcCommand(Command.consume), playModeStatus.Consume),
                }),
                new MenuItem("Update", (sender, e) => MpcCommand(Command.update)),
                new MenuItem("Clear", (sender, e) => MpcCommand(Command.clear)),
                new MenuItem("Stop", (sender, e) => MpcCommand(Command.stop), Command.stop),
                new MenuItem("Pause", (sender, e) => MpcCommand(Command.pause), Command.pause),
                new MenuItem("Play", (sender, e) => MpcCommand(Command.play), Command.play),
                new MenuItem("Next ", (sender, e) => MpcCommand(Command.next), Command.next),
                new MenuItem("Previous", (sender, e) => MpcCommand(Command.prev), Command.prev),
                new MenuItem("Status", new EventHandler(ShowInfo)),
                new MenuItem("Exit", new EventHandler(Exit))
            };

            return menuItems;
        }

        private ContextMenuStrip GetContextMenu()
        {
            var menuItems = GetMenuItems();
            return new ContextMenu(menuItems).ToContextMenuStrip();
        }

        private void MpcCommand(Command cmd)
        {
            _mpc.Cmd(cmd, out Status status);
            _tray.Icon = status.GetIcon();
            _tray.ContextMenuStrip = GetContextMenu();
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            _mpc.Cmd(Command.status, out string info);
            _tray.ShowBalloonTip(8000, "mpc status", info, ToolTipIcon.None);
        }

        private void OnClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                ShowInfo(sender, e);
        }

        private void Exit(object sender, EventArgs e)
        {
            this.ExitThread();
        }
    }
}
