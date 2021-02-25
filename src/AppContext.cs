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
            _mpc = new MpcClient();
            _menu = BuildMenu();

            _tray = new NotifyIcon
            {
                Text = "Test", //_mpc.GetInfo();
                Icon = new Icon(GetType(), "Icons.ico.pauseplay.ico"),
                ContextMenuStrip = _menu,
                Visible = true
            };

            _tray.MouseDoubleClick += (sender, e) => MpcCommand(Command.toggle);
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                this._tray.Dispose();
            }
            base.Dispose( disposing );
        }

        private ContextMenuStrip BuildMenu()
        {
            var m = new ContextMenuStrip();

            var commands = new List<MenuItem>()
            {
                new MenuItem("Update", null, (sender, e) => MpcCommand(Command.update)),
                new MenuItem("Clear", null, (sender, e) => MpcCommand(Command.clear)),
                new MenuItem("Stop", Image.FromFile($"Icons/png/{Command.stop}.png"), (sender, e) => MpcCommand(Command.stop)),
                new MenuItem("Pause", Image.FromFile($"Icons/png/{Command.pause}.png"), (sender, e) => MpcCommand(Command.pause)),
                new MenuItem("Play", Image.FromFile($"Icons/png/{Command.play}.png"), (sender, e) => MpcCommand(Command.play)),
                new MenuItem("Next ", Image.FromFile($"Icons/png/{Command.next}.png"), (sender, e) => MpcCommand(Command.next)),
                new MenuItem("Previous", Image.FromFile($"Icons/png/{Command.prev}.png"), (sender, e) => MpcCommand(Command.prev)),
                new MenuItem("Exit", null, (sender, e) => Exit()),
            };

            foreach(var command in commands)
            {
                m.Items.Add(command.ToToolStripMenuItem());
            }

            return m;
        }

        private void MpcCommand(Command cmd)
        {
            _mpc.Cmd(cmd.ToString());
        }

        private void Exit()
        {
            this.ExitThread();
        }
    }
}
