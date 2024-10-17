﻿using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		[HttpGet]
		public async Task<ActionResult<HaveAccessDto>> GetHaveAccess([FromQuery] GetAnnounceHaveAccessQuery query)
		{
			var item = await Database.Set<Announcement>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == query.Id);

			if (item != null)
			{
				var facade = new GetHaveAccessFacade<Announcement>(item);
				var result = await facade.Execute();
				return Ok(result);
			}

			return Ok(null);
		}

	}
}