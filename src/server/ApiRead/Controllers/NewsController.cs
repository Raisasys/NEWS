using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Queries;
using System.Net;

namespace ApiRead
{
	public class NewsController : AppController
	{
		[HttpGet]
		public async Task<ActionResult<NewsSimpleDto>> GetById([FromQuery] GetNewsById query)
		{
			var response = await QueryProcessor.Execute<GetNewsById, NewsSimpleDto>(query);
			return Ok(response);
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
	}
}
