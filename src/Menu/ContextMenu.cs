using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinMpcTrayIcon.Menu
{
    public class ContextMenu
    {
        public List<MenuItem> Items { get; set; }

        public ContextMenu(List<MenuItem> items)
        {
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
                    { typeof(GroupMenuItem), 1 },
                    { typeof(MpcMenuItem), 2 },
                    { typeof(SysMenuItem), 3 },
                    { typeof(SwitchMenuItem), 4 }
                };

                switch(types[command.GetType()])
                {
                    case 1:
                        i = ((GroupMenuItem)command).ToToolStripMenuItem();
                        Build(i.DropDownItems, ((GroupMenuItem)command).Items);
                        break;
                    case 2:
                        i = ((MpcMenuItem)command).ToToolStripMenuItem();
                        i.Click += ((MpcMenuItem)command).EventHandler;
                        break;
                    case 3:
                        i = ((SysMenuItem)command).ToToolStripMenuItem();
                        i.Click += ((SysMenuItem)command).EventHandler;
                        break;
                    case 4:
                        i = ((SwitchMenuItem)command).ToToolStripMenuItem();
                        i.Click += ((SwitchMenuItem)command).EventHandler;
                        break;
                }

                items.Add(i);
            }
        }
    }
}
