using Commands;
using Commands.News;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiWrite.Controllers
{
	public class NewsController : AppController
	{
		[HttpPost]
		public async Task<ActionResult<CreateNewsResponse>> CreateByTopImageContent(CreateNewsByTopImageContentCommand command)
		{
			var response = await CommandBus.Send<CreateNewsByTopImageContentCommand, CreateNewsResponse>(command);
			return Ok(response);
		}

	}
}
