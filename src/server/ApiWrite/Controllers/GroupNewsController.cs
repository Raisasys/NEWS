
using Commands;
using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWrite.Controllers
{
    public class GroupNewsController : AppController
    {

        [HttpPost]
        public async Task<ActionResult<GroupNewsResponse>> CreateGroupNews([FromBody] CreateGroupNewsCommand command)
        {
            var response = await CommandBus.Send<CreateGroupNewsCommand, GroupNewsResponse>(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GroupNewsResponse>> UpdateGroupNews([FromBody] UpdateGroupNewsCommand command)
        {
            var response = await CommandBus.Send<UpdateGroupNewsCommand, GroupNewsResponse>(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteGroupNews ([FromBody] DeleteGroupNewsCommand command)
        {
            await CommandBus.Send<DeleteGroupNewsCommand>(command);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Publish([FromBody] PublishGroupNewsCommand command)
        {
            command.UserId = UserIdentity.User.UserId;
            await CommandBus.Send<PublishGroupNewsCommand>(command);
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> Archive([FromBody] ArchiveGroupNewsCommand command)
        {
            command.UserId = UserIdentity.User.UserId;
            await CommandBus.Send<ArchiveGroupNewsCommand>(command);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate([FromBody] AuthenticatedGroupNewsCommand command)
        {
            command.UserId = UserIdentity.User.UserId;
            await CommandBus.Send<AuthenticatedGroupNewsCommand>(command);
            return Ok();
        }
        

        [HttpPost]
        public async Task<ActionResult> UpdateAccess(UpdateHaveAccessGroupNewsCommand command)
        {
            var groupNews = await Database.Set<GroupNews>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == command.Id);
            if (groupNews != null)
            {
                var facade = new UpdateHaveAccessScopesAndUsers<GroupNews>(groupNews, command.Scopes, command.Users);
                await facade.Execute();
            }

            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> UpdateIsPublic(UpdateIsGlobalGroupNewsCommand command)
        {
            var groupNews = await Database.Set<GroupNews>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == command.Id);
            if (groupNews != null)
            {
                var facade = new UpdateHaveAccessIsGlobal<GroupNews>(groupNews, command.IsGlobal);
                await facade.Execute();
            }

            return Ok();
        }



        [HttpPost]
        public async Task<ActionResult> AttachCommunication(AttachCommunicationToGroupNewsCommand command)
        {
            var groupNews  = await Database.Set<GroupNews>().Include(c => c.Communications).SingleOrDefaultAsync(c => c.Id == command.Id);
            if (groupNews != null)
            {
                var facade = new SetHaveCommunications<GroupNews>(groupNews, command.Message);
                await facade.Execute();
            }

            return Ok();
        }

    }
}
