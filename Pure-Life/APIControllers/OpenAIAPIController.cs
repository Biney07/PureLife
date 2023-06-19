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
		[HttpGet("use-chat")]
		public async Task<IActionResult> UseChatGPT(string query)
		{
			string outputResult = "";
			var openai = new OpenAIAPI("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");
			CompletionRequest completionRequest = new CompletionRequest();
			completionRequest.Prompt = query+" , te mjeku i ciles lemi duhet te shkoj?";
			completionRequest.Model = OpenAI_API.Models.Model.DavinciText;

			int promptTokens = CountTokens(query);
			int maxCompletionTokens = 4096;

			completionRequest.MaxTokens = maxCompletionTokens -150;

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
