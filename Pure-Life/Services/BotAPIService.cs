using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OpenAI_API;
using Pure_Life.Services;
using Pure_Life.ViewModel;
using System.Net.Http.Headers;
using System.Text;

namespace Pure_Life.Services
{
    public class BotAPIService : IBotAPIService
    {
        private readonly IConfiguration _configuration;

        public BotAPIService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<string>> GenerateContent(ADGenerateRequestModelDTO generateRequestModel)
        {
            var apiKey = _configuration.GetSection("ChatBot:GChatAPIKEY").Value;
            var apiModel = _configuration.GetSection("ChatBot:Model").Value;
            List<string> rq = new List<string>();
            string rs = "";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    var requestBody = new
                    {
                        prompt = generateRequestModel.prompt,
                        model = apiModel,
                        temperature = 0.5,
                        max_tokens = 100,
                        top_p = 1.0,
                        frequency_penalty = 0.0,
                        presence_penalty = 0.0
                    };

                    var json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("https://api.openai.com/v1/your-endpoint", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<OpenApiResponse>(responseContent);

                        foreach (var choice in result.Content)
                        {
                            rs = choice.text;
                            rq.Add(choice.text);
                        }
                    }
                    else
                    {
                        // Handle unsuccessful response
                        // You can throw an exception or handle the error based on the response content
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            return rq;
        }


    }
}