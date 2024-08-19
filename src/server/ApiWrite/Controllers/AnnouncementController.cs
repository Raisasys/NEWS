using Commands.Announcement;
using Commands.News;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiWrite.Controllers
{
	public class AnnouncementController : AppController
	{
		[HttpPost]
		public async Task<ActionResult<CreateAnnouncementResponse>> CreateAnnouncement([FromBody] CreateAnnouncementCommand command)
		{
			var response = await CommandBus.Send<CreateAnnouncementCommand, CreateAnnouncementResponse>(command);
			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<UpdateAnnouncResponse>> UpdateAnnouncement([FromBody] UpdateAnnouncementCommand command)
		{
			var response = await CommandBus.Send<UpdateAnnouncementCommand, UpdateAnnouncResponse>(command);
			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult> DeleteAnnounce([FromBody] DeleteAnnouncementCommand command)
		{
			await CommandBus.Send<DeleteAnnouncementCommand>(command);
			return Ok();
		}

	}
}
