using System.Drawing;
using System.Windows.Forms;

namespace WinMpcTrayIcon.Forms
{
    public class SearchButton : Button
    {
        public SearchButton()
        {
            Location = new Point(738, 2);
            Margin = new Padding(2, 2, 2, 2);
            Width = 60;
            Text = "Search";
        }
    }
}
