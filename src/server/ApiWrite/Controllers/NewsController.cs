using Commands;
using Commands.News;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiWrite.Controllers
{
	public class NewsController : AppController
	{
		[HttpPost]
		public async Task<ActionResult<CreateNewsResponse>> CreateByTopImageContent([FromBody] CreateNewsByTopImageContentCommand command)
		{
			var response = await CommandBus.Send<CreateNewsByTopImageContentCommand, CreateNewsResponse>(command);
			return Ok(response);
		}


		[HttpPost]
		public async Task<ActionResult<CreateNewsResponse>> CreateByTopBottomImageContent([FromBody] CreateNewsByTopBottomImageContentCommand command)
		{
			var response = await CommandBus.Send<CreateNewsByTopBottomImageContentCommand, CreateNewsResponse>(command);
			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<CreateNewsResponse>> CreateByBottomImageContent([FromBody] CreateNewsByBottomImageContentCommand command)
		{
			var response = await CommandBus.Send<CreateNewsByBottomImageContentCommand, CreateNewsResponse>(command);
			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<UpdateNewsResponse>> UpdateByTopImageContent(UpdateNewsByTopImageContentCommand command)
		{
			var response = await CommandBus.Send<UpdateNewsByTopImageContentCommand, UpdateNewsResponse>(command);
			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<UpdateNewsResponse>> UpdateByBottomImageContent([FromBody] UpdateNewsByBottomImageContentCommand command)
		{
			var response = await CommandBus.Send<UpdateNewsByBottomImageContentCommand, UpdateNewsResponse>(command);
			return Ok(response);
		}


		[HttpPost]
		public async Task<ActionResult<UpdateNewsResponse>> UpdateByTopBottomImageContent([FromBody] UpdateNewsByTopBottomImageContentCommand command)
		{
			var response = await CommandBus.Send<UpdateNewsByTopBottomImageContentCommand, UpdateNewsResponse>(command);
			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult> DeleteNews([FromBody] DeleteNewCommand command)
		{
			await CommandBus.Send<DeleteNewCommand>(command);
			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult> UpdateActivationNews([FromBody] UpdateActivationCommand command)
		{
			await CommandBus.Send<UpdateActivationCommand>(command);
			return Ok();
		}
	}
}
