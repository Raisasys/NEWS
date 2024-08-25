using Core;
using Microsoft.AspNetCore.Mvc;
using Queries;

namespace ApiRead.Controllers
{
	public class AnnouncementController : AppController
	{
		[HttpGet]
		public async Task<ActionResult<AnnouncementDto>> GetAnnouncementById([FromQuery] Guid announcementId)
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
		public async Task<ActionResult<GetAnnouncementListDto>> GetAnnouncements([FromQuery] GetAnnouncementListDto oQueryString)
		{
			try
			{
				var oResponse = await QueryProcessor.Execute<GetAnnouncementListDto, AnnouncementListDto>(oQueryString);
				return Ok(oResponse);
			}
			catch (Exception oEx)
			{
				return Problem(oEx.Message);
			}
		}

	}
}
