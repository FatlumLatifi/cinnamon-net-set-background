using System;
using System.Runtime.InteropServices;

namespace Linux.Cinnamon;

public static class WallPaper
{
    [DllImport("libgio-2.0.so.0", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr g_settings_new(string schema);

    [DllImport("libgio-2.0.so.0", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool g_settings_set_string(IntPtr settings, string key, string value);
    


    public static bool SetWallpaper(string imagePath)
    {
        string schema = "org.cinnamon.desktop.background";
        string key = "picture-uri";
        string uri = $"file://{imagePath}";
        // Create a GSettings object
        IntPtr settings = g_settings_new(schema);
        if (settings == IntPtr.Zero)
        {
            throw new Exception("Failed to access GSettings.");
        }
        // Set the wallpaper URI
        bool success = g_settings_set_string(settings, key, uri);
        return success;
    }
}