using System.Collections.Immutable;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BingWallpapers
{

public class WallpaperDownloader:IDisposable
{
    private HttpClient _client;

    private DirectoryInfo _downloadDirectory;

    private string _path;

    public WallpaperDownloader(DirectoryInfo directory)
    {
      _client = new() { BaseAddress = new("https://www.bing.com"),  };
      _downloadDirectory = directory;
      _path = Path.Combine(_downloadDirectory.FullName, "0.jpg");
    }

    public async Task<FileInfo> DownloadDailyAsync()
    {
        
        BingData? data;
        try{data = await _client.GetFromJsonAsync("HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US", BingWallpaperJsonContext.Default.BingData); }
        catch (Exception ex) { return await Task.FromException<FileInfo>(ex);}
         
        if (data is null) { return await Task.FromException<FileInfo>(new Exception(new("bing data came null"))); }
        data.Deconstruct(out List<Image> images, out _);
                if (images is null) { return await Task.FromException<FileInfo>(new Exception(new("images are null"))); }
        Stream ss = await _client.GetStreamAsync(images[0].Url);
        using (FileStream fs = File.Open(_path, FileMode.Create))
        { await ss.CopyToAsync(fs); }
        return new FileInfo(_path);
    }


    /// <summary>
    /// In my case more than 8 dont do anything.
    /// </summary>
    /// <param name="count">Count of images to download from bing</param>
    /// <returns>Return the fileinfo of the images downloaded </returns>
    /// <exception cref="Exception"></exception>
     public async Task<ImmutableList<FileInfo>> DownloadAsync(int count = 1)
    {
       { BingData? data = await _client.GetFromJsonAsync<BingData?>($"HPImageArchive.aspx?format=js&idx=0&n={count}&mkt=en-US");
        if (data is null) { throw new Exception("No data"); }
        if (data.Images.Count == 0) { throw new Exception("No images"); }
        List<FileInfo> files = new();

        for(int i = 0; i < data.Images.Count; i++)
        {
            string path = Path.Combine(_downloadDirectory.FullName, $"{i}.jpg");
            Stream ss = await _client.GetStreamAsync(data.Images[i].Url);
            using (FileStream fs = File.Open(path, FileMode.Create))
            { await ss.CopyToAsync(fs); }
            files.Add(new(path));
        }

        return files.ToImmutableList();
        
        
       }
    }
    


    public void Dispose()
    {

    }
}
}