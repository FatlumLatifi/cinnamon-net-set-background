using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BingWallpapers;

public record BingData(
    List<Image> Images,
    Tooltips Tooltips
);

public record Image(
    string StartDate,
    string FullStartDate,
    string EndDate,
    string Url,
    string UrlBase,
    string Copyright,
    string CopyrightLink,
    string Title,
    string Quiz,
    bool Wp,
    string Hsh,
    int Drk,
    int Top,
    int Bot,
    List<string> Hs
);


public record Tooltips(
    string Loading,
    string Previous,
    string Next,
    string Walle,
    string Walls
);

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, // Adjust based on your JSON
    WriteIndented = true)]
[JsonSerializable(typeof(Image))]
[JsonSerializable(typeof(Tooltips))]
[JsonSerializable(typeof(BingData))]
public partial class BingWallpaperJsonContext : JsonSerializerContext
{

} 

