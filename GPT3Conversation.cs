using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

class GPT3Conversation : IDisposable
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public GPT3Conversation(string apiKey)
    {
        _client = new HttpClient();
        _apiKey = apiKey;
    }

    public async Task<string> GenerateResponse(string input)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/engines/text-davinci-003/completions");
        request.Headers.Add("Authorization", $"Bearer {_apiKey}");

        var requestJson = JsonConvert.SerializeObject(new
        {
            prompt = input,
            temperature = 0.5f,
            max_tokens = 4000,
            stop = string.Empty,
        });

        request.Content = new StringContent(requestJson, Encoding.UTF8);
        request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _client.SendAsync(request);
        var responseJson = await response.Content.ReadAsStringAsync();

        var responseData = JsonConvert.DeserializeObject<GPT3Response>(responseJson);

        if (responseData == null)
        {
            return $"We're sorry, but we had a problem processing your request. please try again";
        }

        return responseData.Choices[0].Text.Trim();

    }

    public void Dispose()
    {
        _client.Dispose();
    }
}