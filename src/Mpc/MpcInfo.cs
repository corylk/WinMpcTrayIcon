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
                    Track = infoArr[0]; // do more validation before assigning these?
                    Status = infoArr[1]; // such as check for [/] here
                    Playmodes = infoArr[2]; // do we even need this string?
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
