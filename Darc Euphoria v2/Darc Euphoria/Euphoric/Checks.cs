namespace Darc_Euphoria.Euphoric
{
    public static class Checks
    {
        public static bool Upd(this int value)
        {
            if (value != gvar.RefreshID)
            {
                value = gvar.RefreshID;
                return true;
            }

            return false;
        }
    }
}