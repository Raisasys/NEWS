using Commands.News;
using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public async Task<ActionResult<CreateNewsResponse>> CreateBySliderImageContent([FromBody] CreateNewsBySliderImageContentCommand command)
		{
			var response = await CommandBus.Send<CreateNewsBySliderImageContentCommand, CreateNewsResponse>(command);
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
		public async Task<ActionResult<UpdateNewsResponse>> UpdateBySliderImageContent([FromBody] UpdateNewsBySliderImageContentCommand command)
		{
			var response = await CommandBus.Send<UpdateNewsBySliderImageContentCommand, UpdateNewsResponse>(command);
			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult> DeleteNews([FromBody] DeleteNewCommand command)
		{
			await CommandBus.Send<DeleteNewCommand>(command);
			return Ok();
		}


        [HttpPost]
        public async Task<ActionResult> Publish([FromBody] PublishNewsCommand command)
        {
            command.UserId = UserIdentity.User.UserId;
            await CommandBus.Send<PublishNewsCommand>(command);
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> Archive([FromBody] ArchiveNewsCommand command)
        {
            command.UserId = UserIdentity.User.UserId;
            await CommandBus.Send<ArchiveNewsCommand>(command);
            return Ok();
        }
		

        [HttpPost]
		public async Task<ActionResult> UpdateAccess(UpdateHaveAccessNewsCommand command)
		{
			var news = await Database.Set<News>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == command.Id);
			if (news != null)
			{
				var facade = new UpdateHaveAccessScopesAndUsers<News>(news, command.Scopes, command.Users);
				await facade.Execute();
			}

			return Ok();
		}


		[HttpPost]
		public async Task<ActionResult> UpdateIsPublic(UpdateIsGlobalNewsCommand command)
		{
			var news = await Database.Set<News>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == command.Id);
			if (news != null)
			{
				var facade = new UpdateHaveAccessIsGlobal<News>(news, command.IsGlobal);
				await facade.Execute();
			}

			return Ok();
		}

        [HttpPost]
        public async Task<ActionResult> AttachCommunication(AttachCommunicationToNewsCommand command)
        {
            var news = await Database.Set<News>().Include(c => c.Communications).SingleOrDefaultAsync(c => c.Id == command.Id);
            if (news != null)
            {
                var facade = new SetHaveCommunications<News>(news, command.Message);
                await facade.Execute();
            }

            return Ok();
        }
    }
}
