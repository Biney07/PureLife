using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace ChatGPT_CSharp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OpenAIAPIController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public OpenAIAPIController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpGet("use-chat")]
		public async Task<IActionResult> UseChatGPT(string query)
		{
			string outputResult = "";
			var openai = new OpenAIAPI(_configuration["OpenAI:APIKey"]);
			CompletionRequest completionRequest = new CompletionRequest();
			completionRequest.Prompt = query+ " , te mjeku i ciles lemi duhet te shkoj zgjedh nga keto dhe tregoni arsyen: Pediatri\r\nKardiologji\r\nGinekologji\r\nDermatologji\r\nNefrologji\r\nEndokrinologji\r\nNeurologji\r\nOnkologji\r\nHematologji\r\nRadiologji\r\nKirurgji\r\nAnesteziologji\r\nPsikiatri\r\nOrtopedi\r\nOftalmologji\r\nUrologji\r\nPulmonologji\r\nKardiokirurgji\r\nReumatologji\r\nInfektive\r\n?";
			completionRequest.Model = OpenAI_API.Models.Model.DavinciText;

			int promptTokens = CountTokens(query);
			int maxCompletionTokens = 4096;

			completionRequest.MaxTokens = maxCompletionTokens -400;

			var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

			foreach (var completion in completions.Completions)
			{
				outputResult += completion.Text;
			}

			return Ok(outputResult);
		}

		private int CountTokens(string text)
		{
			// Replace this logic with your own tokenization method or library
			string[] tokens = text.Split(' ');
			return tokens.Length;
		}





	}
}
