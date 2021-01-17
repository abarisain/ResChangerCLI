using PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static PInvoke.User32;
using static PInvoke.DEVMODE;

namespace ResChangerCLI
{
    public record DisplaySettings
    {
        public int refreshRate = -1;
        public uint width;
        public uint height;
    } 
    class DisplaySettingsHelper
    {

        [DllImport("user32.dll")]
        static extern int ChangeDisplaySettingsW(ref DEVMODE devMode, int flags);

        public static void apply(DisplaySettings settings)
        {
            var devmode = new DEVMODE();
            devmode.dmSize = (ushort)Marshal.SizeOf(devmode);
            EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devmode);
            FieldUseFlags fields = FieldUseFlags.DM_PELSHEIGHT | FieldUseFlags.DM_PELSWIDTH;
            devmode.dmPelsHeight = settings.height;
            devmode.dmPelsWidth = settings.width;
            if (settings.refreshRate > 0)
            {
                devmode.dmDisplayFrequency = (uint)settings.refreshRate;
                fields |= FieldUseFlags.DM_DISPLAYFREQUENCY;
            }
            devmode.dmFields = fields;
            ChangeDisplaySettingsW(ref devmode, 0);
        }
    }
}
