using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace ApiRead
{
	public class MyController : AppController
	{
        
        [HttpGet]
		public async Task<ActionResult<NewsFullDto>> GetNewsById([FromQuery] Guid id)
        {
            try
			{
                var oResponse = await QueryProcessor.Execute<GetMyNewsById, NewsFullDto>(new GetMyNewsById { NewsId = id });
                return Ok(oResponse);
            }
            catch (Exception oEx)
			{
                return Problem(oEx.Message);
            }
		}


        [HttpGet]
        public async Task<ActionResult<NewsFullDto>> GetGroupNewsById([FromQuery] Guid id)
        {
            try
            {
                var oResponse = await QueryProcessor.Execute<GetMyGroupNewsById, NewsListDto>(new GetMyGroupNewsById { GroupNewsId = id });
                return Ok(oResponse);
            }
            catch (Exception oEx)
            {
                return Problem(oEx.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<NewsFullDto>> GetAnnouncementById([FromQuery] Guid id)
        {
            try
            {
                var oResponse = await QueryProcessor.Execute<GetMyAnnounceById, AnnouncementFullDto>(new GetMyAnnounceById { AnnouncementId = id });
                return Ok(oResponse);
            }
            catch (Exception oEx)
            {
                return Problem(oEx.Message);
            }
        }
    }
}
