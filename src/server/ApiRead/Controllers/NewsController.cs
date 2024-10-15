using Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Queries;

namespace ApiRead
{
	public class NewsController : AppController
	{
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

        [HttpGet]
		public async Task<ActionResult<NewsFullDto>> GetById([FromQuery] Guid newsId)
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
		public async Task<ActionResult<List<NewsSimpleDto>>> GetByOwnerId([FromQuery] GetNewsByOwnerId oQueryString)
		{
			try
			{
				return Ok(await QueryProcessor.Execute<GetNewsByOwnerId, NewsSimpleDto>(oQueryString));
			}
			catch (Exception oEx)
			{
				return Problem(oEx.Message);				
			}			
		}

		

		[HttpGet]
		public async Task<ActionResult<NewsListDto>> GetArchived([FromQuery] GetArchivedNewsListDto oQueryString)
		{
			try
			{
				var oResponse = await QueryProcessor.Execute<GetArchivedNewsListDto, NewsListDto>(oQueryString);
				return Ok(oResponse);
			}
			catch (Exception oEx)
			{
				return Problem(oEx.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<NewsListDto>> GetNewsByPages([FromQuery] GetNewsListByPagesDto oQueryString)
		{
			try
			{
				var oResponse = await QueryProcessor.Execute<GetNewsListByPagesDto, NewsListDto>(oQueryString);
				return Ok(oResponse);
			}
			catch (Exception oEx)
			{
				return Problem(oEx.Message);
			}
		}


		[HttpGet]
		public async Task<ActionResult<NewsListDto>> GetMySliderImageNewsById ([FromQuery] GetMySliderImageNewsById oQueryString)
		{
			try
			{
				var oResponse = await QueryProcessor.Execute<GetMySliderImageNewsById, NewsListDto>(oQueryString);
				return Ok(oResponse);
			}
			catch (Exception oEx)
			{
				return Problem(oEx.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<HaveAccessDto>> GetHaveAccess([FromQuery] GetNewsHaveAccessQuery query)
		{
			var item = await Database.Set<News>().Include(c => c.AccessEntityItems).SingleOrDefaultAsync(c => c.Id == query.Id);

			if (item != null)

			{
				var facade = new GetHaveAccessFacade<News>(item);
				var result = await facade.Execute();
				return Ok(result);
			}

			return Ok(null);
		}
	}
}
