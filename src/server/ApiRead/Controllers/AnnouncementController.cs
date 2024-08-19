using Core;
using Microsoft.AspNetCore.Mvc;
using Queries;

namespace ApiRead.Controllers
{
	public class AnnouncementController : AppController
	{
		[HttpGet]
		public async Task<ActionResult<AnnouncementDto>> GetAnnouncementById([FromQuery] long announcementId)
		{
			try
			{
				var oResponse = await QueryProcessor.Execute<GetAnnouncementById, AnnouncementDto>(new GetAnnouncementById { AnnouncementId = announcementId });
				return Ok(oResponse);
			}
			catch (Exception oEx)
			{
				return Problem(oEx.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<AnnouncemenListDto>> GetAnnouncements([FromQuery] GetAnnouncementListDto oQueryString)
		{
			try
			{
				var oResponse = await QueryProcessor.Execute<GetAnnouncementListDto, AnnouncemenListDto>(oQueryString);
				return Ok(oResponse);
			}
			catch (Exception oEx)
			{
				return Problem(oEx.Message);
			}
		}

	}
}
