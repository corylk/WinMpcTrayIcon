namespace WinMpcTrayIcon.Mpc
{
    public class MpcPlaymodes
    {
        public bool Repeat { get; set; }

        public bool Random { get; set; }

        public bool Single { get; set; }

        public bool Consume { get; set; }

        public MpcPlaymodes(string info)
        {
            if (info.Contains("repeat:"))
                Repeat = info?.Split("repeat: ")[1]?.Split(" ")[0] == "on";

            if (info.Contains("random:"))
                Random = info?.Split("random: ")[1]?.Split(" ")[0] == "on";

            if (info.Contains("single:"))
                Single = info?.Split("single: ")[1]?.Split(" ")[0] == "on";

            if (info.Contains("consume:"))
                Consume = info?.Split("consume: ")[1].TrimEnd() == "on";
        }
    }
}
