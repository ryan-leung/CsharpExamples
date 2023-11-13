using System.Text.Json;
using System.Xml.Linq;

string url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd";
static async Task<string> FetchTask(string url)
{
    HttpClient client = new HttpClient();
    try
    {
        HttpResponseMessage msg = client.GetAsync(url).Result;
        string content = await msg.Content.ReadAsStringAsync();
        return content;
    }
    catch (Exception ex)
    {
        // Handle any other exceptions
        Console.WriteLine("An error occurred: " + ex.Message);
        return "";
    }
}

static string ExtractTask(string content)
{
    JsonDocument doc = JsonDocument.Parse(content);
    JsonElement root = doc.RootElement;
    return root.GetRawText();
}

static async Task WriteTask(string data)
{
    string filePath = "data.ndjson";
    try
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine(data);

        }
        Console.WriteLine("Data has been written to the file.");
    }
    catch (Exception ex)
    {
        // Handle any other exceptions
        Console.WriteLine("An error occurred: " + ex.Message);
    }
}

static async Task Run(string url)
{
    DateTime now = DateTime.Now;
    Console.WriteLine(String.Format("[{0}] Starting jobs, press Enter to exit...", now.ToString()));
    string text = await FetchTask(url);
    string result = Convert.ToString(now.ToFileTimeUtc()) + text;
    await WriteTask(result);
}

TimerCallback timerCallback = async (state) =>
{
    await Run(url);
};
Timer timer = new Timer(timerCallback, null, 0, 10000);
Console.ReadLine();