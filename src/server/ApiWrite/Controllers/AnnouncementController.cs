using Commands.Announcement;
using Commands.News;
using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public async Task<ActionResult<UpdateAnnouncementResponse>> UpdateAnnouncement([FromBody] UpdateAnnouncementCommand command)
		{
			var response = await CommandBus.Send<UpdateAnnouncementCommand, UpdateAnnouncementResponse>(command);
			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult> DeleteAnnounce([FromBody] DeleteAnnouncementCommand command)
		{
			await CommandBus.Send<DeleteAnnouncementCommand>(command);
			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult> UpdateAccess(UpdateHaveAccessAnnounceCommand command)
		{
			var announce = await Database.Set<Announcement>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == command.Id);
			if (announce != null)
			{
				var facade = new UpdateHaveAccessScopesAndUsers<Announcement>(announce, command.Scopes, command.Users);
				await facade.Execute();
			}

			return Ok();
		}


		[HttpPost]
		public async Task<ActionResult> UpdateIsPublic(UpdateIsGlobalAnnounceCommand command)
		{
			var announce = await Database.Set<Announcement>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == command.Id);
			if (announce != null)
			{
				var facade = new UpdateHaveAccessIsGlobal<Announcement>(announce, command.IsGlobal);
				await facade.Execute();
			}

			return Ok();
		}
	}
}
