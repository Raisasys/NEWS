
using Commands;
using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWrite.Controllers
{
    public class GroupAnnouncementController : AppController
    {

        [HttpPost]
        public async Task<ActionResult<GroupAnnouncementResponse>> CreateGroupAnnouncement([FromBody] CreateGroupAnnouncementCommand command)
        {
            var response = await CommandBus.Send<CreateGroupAnnouncementCommand, GroupAnnouncementResponse>(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GroupAnnouncementResponse>> UpdateGroupAnnouncement([FromBody] UpdateGroupAnnouncementCommand command)
        {
            var response = await CommandBus.Send<UpdateGroupAnnouncementCommand, GroupAnnouncementResponse>(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteGroupAnnouncement ([FromBody] DeleteGroupAnnouncementCommand command)
        {
            await CommandBus.Send<DeleteGroupAnnouncementCommand>(command);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Publish([FromBody] PublishGroupAnnouncementCommand command)
        {
            command.UserId = UserIdentity.User.UserId;
            await CommandBus.Send<PublishGroupAnnouncementCommand>(command);
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> Archive([FromBody] ArchiveGroupAnnouncementCommand command)
        {
            command.UserId = UserIdentity.User.UserId;
            await CommandBus.Send<ArchiveGroupAnnouncementCommand>(command);
            return Ok();
        }

       
        [HttpPost]
        public async Task<ActionResult> UpdateAccess(UpdateHaveAccessGroupAnnouncementCommand command)
        {
            var groupAnnouncement = await Database.Set<GroupAnnouncement>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == command.Id);
            if (groupAnnouncement != null)
            {
                var facade = new UpdateHaveAccessScopesAndUsers<GroupAnnouncement>(groupAnnouncement, command.Scopes, command.Users);
                await facade.Execute();
            }

            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> UpdateIsPublic(UpdateIsGlobalGroupAnnouncementCommand command)
        {
            var groupAnnouncement = await Database.Set<GroupAnnouncement>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == command.Id);
            if (groupAnnouncement != null)
            {
                var facade = new UpdateHaveAccessIsGlobal<GroupAnnouncement>(groupAnnouncement, command.IsGlobal);
                await facade.Execute();
            }

            return Ok();
        }



        [HttpPost]
        public async Task<ActionResult> AttachCommunication(AttachCommunicationToGroupAnnouncementCommand command)
        {
            var groupAnnouncement  = await Database.Set<GroupAnnouncement>().Include(c => c.Communications).SingleOrDefaultAsync(c => c.Id == command.Id);
            if (groupAnnouncement != null)
            {
                var facade = new SetHaveCommunications<GroupAnnouncement>(groupAnnouncement, command.Message);
                await facade.Execute();
            }

            return Ok();
        }

    }
}
