using System;
using System.Drawing;

namespace WinMpcTrayIcon.Mpc
{
    public static class EnumExtensions
    {
        public static Icon GetIcon(this Status status)
        {
            int action = Math.Min(2, 3 - (int)status);

            return new Icon($"Icons/ico/{(Command)action}.ico");
        }
    }
}
