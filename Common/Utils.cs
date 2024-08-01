using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Common;
public static class Utils
{
    private const string SessionCookieParameter = "SessionCookie";
    private const string YearParameter = "Year";

    public static async Task<string> GetInput(int day)
    {
        var cachePath = $"./cache/{day.ToString("00")}.txt";

        if (File.Exists(cachePath))
            return await File.ReadAllTextAsync(cachePath);

        var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var configuration = configurationBuilder.Build();

        using var client = new HttpClient();
        int year = int.Parse(configuration[YearParameter] 
            ?? throw new ConfigurationErrorsException($"Error while loading the parameter {YearParameter} from the configuration file."));
        string sessionCookieValue = configuration[SessionCookieParameter] 
            ?? throw new ConfigurationErrorsException($"Error while loading the parameter {SessionCookieParameter} from the configuration file.");

        var url = $"https://adventofcode.com/{year}/day/{day}/input";
            
        client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookieValue}");
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var cacheFolder = $"./cache";
        if (!Directory.Exists(cacheFolder))
            Directory.CreateDirectory(cacheFolder);

        await File.WriteAllTextAsync(cachePath, content);

        return content;
}
}

