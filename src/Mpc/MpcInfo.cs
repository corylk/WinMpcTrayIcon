using System.Linq;

namespace WinMpcTrayIcon.Mpc
{
    public class MpcInfo
    {
        public string Track { get; set; }

        public string Status { get; set; }

        public string Playmodes { get; set; }

        public MpcInfo(string info)
        {
            if (info.Contains("\r\n"))
            {
                var infoArr = info.Split("\r\n").ToList();

                if (infoArr.Count > 1 && infoArr[1].Contains("["))
                {
                    Track = infoArr[0]; // do more validation on these
                    Status = infoArr[1];
                    Playmodes = infoArr[2];
                }
                else
                {
                    Playmodes = infoArr[0];
                }
            }
            else
            {
                Playmodes = info;
            }
        }
    }
}
