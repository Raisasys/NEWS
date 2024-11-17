using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace ApiRead.Controllers
{
    public class GroupAnnouncementController : AppController
    {
        [HttpGet]
        public async Task<ActionResult<GroupAnnouncementDto>> GetGroupAnnouncementById([FromQuery] Guid groupAnnouncementId)
        {
            try
            {
                var oResponse = await QueryProcessor.Execute<GroupAnnouncementByIdQuery, GroupAnnouncementDto>(new GroupAnnouncementByIdQuery { GroupAnnouncementId = groupAnnouncementId });
                return Ok(oResponse);
            }
            catch (Exception oEx)
            {
                return Problem(oEx.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<GroupAnnouncementListDto>> GetGroupAnnouncement()
        {
            try
            {
                var oResponse = await QueryProcessor.Execute<GroupAnnouncementListQuery, GroupAnnouncementListDto>(new GroupAnnouncementListQuery());
                return Ok(oResponse);
            }
            catch (Exception oEx)
            {
                return Problem(oEx.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<HaveAccessDto>> GetHaveAccess([FromQuery] GetGroupAnnouncementHaveAccessQuery query)
        {
            var item = await Database.Set<GroupAnnouncement>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == query.Id);

            if (item != null)
            {
                var facade = new GetHaveAccessFacade<GroupAnnouncement>(item);
                var result = await facade.Execute();
                return Ok(result);
            }

            return Ok(null);
        }

        [HttpGet]
        public async Task<ActionResult<GetGroupAnnouncementHaveCommunicationsQuery>> GetHaveCommunications([FromQuery] GetGroupAnnouncementHaveCommunicationsQuery query)
        {
            var item = await Database.Set<GroupAnnouncement>().Include(c => c.Communications).SingleOrDefaultAsync(c => c.Id == query.Id);

            if (item != null)
            {
                var facade = new GetHaveCommunicationsFacade<GroupAnnouncement>(item);
                var result = await facade.Execute();
                return Ok(result);
            }

            return Ok(null);
        }
    }
}
