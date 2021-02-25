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
                new MenuItem("Update", Command.update, false),
                new MenuItem("Clear", Command.clear, false),
                new MenuItem("Stop", Command.stop, true),
                new MenuItem("Pause", Command.pause, true),
                new MenuItem("Play", Command.play, true),
                new MenuItem("Next ", Command.next, true),
                new MenuItem("Previous", Command.prev, true),
            };

            foreach(var command in commands)
            {
                var i = new ToolStripMenuItem();
                i.Image = command.HasImage ? Image.FromFile($"Icons/png/{command.Command}.png") : null;
                i.Text = command.Text;
                i.Click += (sender, e) => MpcCommand(command.Command);
                m.Items.Add(i);
            }

            var exit = new ToolStripMenuItem();
            exit.Text = "Exit";
            exit.Click += (sender, e) => Exit();
            m.Items.Add(exit);

            return m;
        }

        private void MpcCommand(Command cmd)
        {
            _mpc.Cmd(cmd);
        }

        private void Exit()
        {
            this.ExitThread();
        }
    }
}
