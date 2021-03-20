using System.Drawing;
using System.Windows.Forms;

namespace WinMpcTrayIcon.Forms
{
    public class SearchBox : TextBox
    {
        public SearchBox()
        {
            Location = new Point(2, 2);
            Margin = new Padding(2, 2, 2, 2);
            Anchor = AnchorStyles.Top | AnchorStyles.Right |  AnchorStyles.Left;
        }

        public void AutoResize()
        {
            Width = Parent.Width - 20;
        }
    }
}
