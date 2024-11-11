using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace ApiRead.Controllers
{
    public class GroupNewsController : AppController
    {
        [HttpGet]
        public async Task<ActionResult<GroupNewsDto>> GetGroupNewsById([FromQuery] Guid groupNewsId)
        {
            try
            {
                var oResponse = await QueryProcessor.Execute<GroupNewsByIdQuery, GroupNewsDto>(new GroupNewsByIdQuery { GroupNewsId = groupNewsId });
                return Ok(oResponse);
            }
            catch (Exception oEx)
            {
                return Problem(oEx.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<GroupNewsListDto>> GetGroupNews()
        {
            try
            {
                var oResponse = await QueryProcessor.Execute<GetGroupNewsListQuery, GroupNewsListDto>(new GetGroupNewsListQuery());
                return Ok(oResponse);
            }
            catch (Exception oEx)
            {
                return Problem(oEx.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<HaveAccessDto>> GetHaveAccess([FromQuery] GetGroupNewsHaveAccessQuery query)
        {
            var item = await Database.Set<GroupNews>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == query.Id);

            if (item != null)
            {
                var facade = new GetHaveAccessFacade<GroupNews>(item);
                var result = await facade.Execute();
                return Ok(result);
            }

            return Ok(null);
        }

        [HttpGet]
        public async Task<ActionResult<GetGroupNewsHaveCommunicationsQuery>> GetHaveCommunications([FromQuery] GetGroupNewsHaveCommunicationsQuery query)
        {
            var item = await Database.Set<GroupNews>().Include(c => c.Communications).SingleOrDefaultAsync(c => c.Id == query.Id);

            if (item != null)
            {
                var facade = new GetHaveCommunicationsFacade<GroupNews>(item);
                var result = await facade.Execute();
                return Ok(result);
            }

            return Ok(null);
        }
    }
}
