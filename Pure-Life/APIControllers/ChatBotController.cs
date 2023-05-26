using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Pure_Life.APIControllers
{
    [ApiController]
    [Route("api/chatbot")]
    public class ChatBotController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatBotController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ChatBotRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

                var completionRequest = new
                {
                    model = "text-davinci-003",
                    prompt = request.Question,
                    temperature = 0.7f,
                    max_tokens = 50
                };
                var json = JsonSerializer.Serialize(completionRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.openai.com/v1/completions", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("An error occurred while processing the request.");
                }

                var completionResponse = JsonSerializer.Deserialize<ChatBotResponse>(responseContent);
                var answer = completionResponse.choices[0].text;

                return Ok(new { answer });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }

    public class ChatBotRequest
    {
        public string Question { get; set; }
    }

    public class ChatBotResponse
    {
        public Choice[] choices { get; set; }
    }

    public class Choice
    {
        public string text { get; set; }
        public int index { get; set; }
        public string finish_reason { get; set; }
    }
}
