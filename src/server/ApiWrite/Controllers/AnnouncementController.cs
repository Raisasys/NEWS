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
		public async Task<ActionResult> UpdateHaveAccessAnnounce(UpdateHaveAccessAnnounceCommand command)
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
        public async Task<ActionResult> Publish([FromBody] PublishAnnouncementCommand command)
        {
            command.UserId = UserIdentity.User.UserId;
            await CommandBus.Send<PublishAnnouncementCommand>(command);
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> Archive([FromBody] ArchiveAnnouncementCommand command)
        {
            command.UserId = UserIdentity.User.UserId;
            await CommandBus.Send<ArchiveAnnouncementCommand>(command);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate([FromBody] AuthenticatedAnnouncementCommand command)
        {
            command.UserId = UserIdentity.User.UserId;
            await CommandBus.Send<AuthenticatedAnnouncementCommand>(command);
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



        [HttpPost]
        public async Task<ActionResult> AttachCommunication(AttachCommunicationToAnnounceCommand command)
        {
            var announce = await Database.Set<Announcement>().Include(c => c.Communications).SingleOrDefaultAsync(c => c.Id == command.Id);
            if (announce != null)
            {
                var facade = new SetHaveCommunications<Announcement>(announce, command.Message);
                await facade.Execute();
            }

            return Ok();
        }
    }
}
