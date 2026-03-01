using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://api.adviceslip.com/advice";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    string advice = doc.RootElement
                                       .GetProperty("slip")
                                       .GetProperty("advice")
                                       .GetString();

                    Console.WriteLine("Conselho de Hoje:");
                    Console.WriteLine(advice);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao consumir a API:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
