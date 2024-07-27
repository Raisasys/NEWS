using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Queries;

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

	}
}
