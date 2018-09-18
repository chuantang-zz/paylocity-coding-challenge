namespace Paylocity.Api.Website
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	public class BaseController : ControllerBase
	{
		internal ILogger Logger;

		internal BaseController() { }
	}
}
