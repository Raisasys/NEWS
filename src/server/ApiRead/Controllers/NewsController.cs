using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities.Encoders;
using Queries;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApiRead
{
	public class NewsController : AppController
	{
		[HttpGet]
		public async Task<ActionResult<NewsFullDto>> GetById([FromQuery] long newsId)
        {
            try
			{
                var oResponse = await QueryProcessor.Execute<GetNewsById, NewsFullDto>(new GetNewsById { NewsId = newsId });
                return Ok(oResponse);
            }
            catch (Exception oEx)
			{
                return Problem(oEx.Message);
            }
		}

		[HttpGet]
		public async Task<ActionResult<List<NewsSimpleDto>>> GetByOwnerID([FromQuery] GetNewsByOwnerID oQueryString)
		{
			try
			{
				return Ok(await QueryProcessor.Execute<GetNewsByOwnerID, NewsSimpleDto>(oQueryString));
			}
			catch (Exception oEx)
			{
				return Problem(oEx.Message);				
			}			
		}

		[HttpGet]
		public async Task<ActionResult<NewsListDto>> GetNews([FromQuery] GetNewsListDto oQueryString)
		{
			try
			{
				var oResponse = await QueryProcessor.Execute<GetNewsListDto, NewsListDto>(oQueryString);
                return Ok(oResponse);
			}
			catch (Exception oEx)
			{
				return Problem(oEx.Message);
			}
		}
	}
}
