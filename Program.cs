using System;
using System.Diagnostics;

namespace ResChangerCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Assert(args.Length >= 2);
            DisplaySettings displaySettings = new DisplaySettings
            {
                width = uint.Parse(args[0]),
                height = uint.Parse(args[1]),
                refreshRate = args.Length > 2 ? int.Parse(args[2]) : -1
            };
            DisplaySettingsHelper.apply(displaySettings);
        }
    }
}
