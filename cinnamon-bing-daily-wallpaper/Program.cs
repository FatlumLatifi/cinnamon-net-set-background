using BingWallpapers;
using Linux.Cinnamon;

const string directoryPath = "~/.local/share/bing-daily-wallpaper";
DirectoryInfo di;
if (Directory.Exists(directoryPath) is false) { di = Directory.CreateDirectory(directoryPath); }
else { di = new(directoryPath); }

using (WallpaperDownloader bingDownloader = new(di))
{
    FileInfo? file = await bingDownloader.DownloadDailyAsync();
    if (file is not null) 
    {
        WallPaper.SetWallpaper(file.FullName);
    }
}