using BingWallpapers;
using Linux.Cinnamon;

// Checks and creates directory to hold image
const string directoryPath = "~/.local/share/bing-daily-wallpaper";
DirectoryInfo di;
if (Directory.Exists(directoryPath) is false) 
{ di = Directory.CreateDirectory(directoryPath); }
else { di = new(directoryPath); }


using (WallpaperDownloader bingDownloader = new(di))
{
    // FileInfo of the downloaded image
    FileInfo? file = await bingDownloader.DownloadDailyAsync();
    if (file is not null) 
    {
        // Sets the wallpaper
        WallPaper.SetWallpaper(file.FullName);
    }
}