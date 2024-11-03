
using Commands.GroupNews;
using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWrite.Controllers
{
    public class GroupNewsController : AppController
    {

        [HttpPost]
        public async Task<ActionResult<GroupNewsResponse>> CreateGroupNews([FromBody] GroupNewsCommand command)
        {
            var response = await CommandBus.Send<GroupNewsCommand, GroupNewsResponse>(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GroupNewsResponse>> UpdateGroupNews([FromBody] GroupNewsUpdateCommand command)
        {
            var response = await CommandBus.Send<GroupNewsUpdateCommand, GroupNewsResponse>(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteGroupNews ([FromBody] DeleteGroupNewsCommand command)
        {
            await CommandBus.Send<DeleteGroupNewsCommand>(command);
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
