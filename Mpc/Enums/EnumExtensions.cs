namespace WinMpcTrayIcon.Mpc
{
    public static class EnumExtensions
    {
        public static string AsArg(this Command cmd)
        {
            return cmd.ToString().ToLower();
        }
    }
}