using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon.Menu
{
    public class ContextMenu
    {
        private readonly MpcClient _mpc;

        public List<MenuItem> Items { get; set; }

        public ContextMenu(List<MenuItem> items)
        {
            _mpc = new MpcClient();
            Items = items;
        }

        public ContextMenuStrip ToContextMenuStrip()
        {
            var m = new ContextMenuStrip();
            this.Build(m.Items);

            return m;
        }

        public void Build(ToolStripItemCollection items, List<MenuItem> commands = null)
        {
            if (commands == null)
                commands = Items;

            foreach(var command in commands)
            {
                var i = new ToolStripMenuItem();

                var types = new Dictionary<Type, int>()
                {
                    { typeof(MpcMenuItem), 1 },
                    { typeof(GroupMenuItem), 2 },
                    { typeof(SysMenuItem), 3 },
                };

                switch(types[command.GetType()])
                {
                    case 1:
                        i = ((MpcMenuItem)command).ToToolStripMenuItem();
                        i.Click += (sender, e) => MpcCommand(((MpcMenuItem)command).Command);
                        break;
                    case 2:
                        i = ((GroupMenuItem)command).ToToolStripMenuItem();
                        Build(i.DropDownItems, ((GroupMenuItem)command).Items);
                        break;
                    case 3:
                        i = ((SysMenuItem)command).ToToolStripMenuItem();
                        i.Click += ((SysMenuItem)command).Command;
                        break;
                }

                items.Add(i);
            }
        }

        private void MpcCommand(Command cmd)
        {
            _mpc.Cmd(cmd);
        }
    }
}
