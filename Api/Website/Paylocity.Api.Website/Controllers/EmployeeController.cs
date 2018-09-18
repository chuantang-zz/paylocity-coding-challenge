namespace Paylocity.Api.Website.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Domain.Interface;
	using Domain.Model;
	using Microsoft.AspNetCore.Mvc;

	/// <summary> This controller deals with the CRUD of the <see cref="EmployeeController"/> page </summary>
	[Produces("application/json")]
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : BaseController
	{
		private readonly IEmployeeDomain _domain;

		/// <summary> This is the constructor of the controller that deals with the dependency injection of the application for <see cref="Employee"/> </summary>
		/// <param name="domain"> <see cref="IEmployeeDomain"/> </param>
		public EmployeeController(IEmployeeDomain domain) { _domain = domain; }

		/// <summary> This will delete a <see cref="Employee"/> </summary>
		/// <returns> <see cref="EmployeeDomainModel"/> </returns>
		/// <param name="model"> <see cref="EmployeeDomainModel"/> </param>
		/// <response code="201"> <see cref="EmployeeDomainModel"/> </response>
		/// <response code="500"> An error happened and was not able to delete a <see cref="Employee"/> </response>
		[HttpDelete]
		[ProducesResponseType(typeof(EmployeeDomainModel), 200)]
		[ProducesResponseType(typeof(void), 400)]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			if (id == Guid.Empty) return BadRequest();

			EmployeeDomainModel deleted = null;
			try { deleted = await _domain.DeleteAsync(id).ConfigureAwait(false); }
			catch (Exception ex) { return BadRequest(ex.InnerException); }

			return Ok(deleted);
		}

		/// <summary> This will get all <see cref="Employee"/> </summary>
		/// <returns> IEnumerable of <see cref="EmployeeDomainModel"/> </returns>
		/// <response code="200"> IEnumerable of <see cref="EmployeeDomainModel"/> </response>
		/// <response code="500"> An error happened and was not able to pull all data </response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<EmployeeDomainModel>), 200)]
		[ProducesResponseType(typeof(void), 500)]
		public async Task<IActionResult> GetAsync()
		{
			IEnumerable<EmployeeDomainModel> models;

			try { models = await _domain.GetAsync().ConfigureAwait(false); }
			catch (Exception ex) { return BadRequest(ex.InnerException); }

			return Ok(models);
		}

		/// <summary> This will get a <see cref="Employee"/> by its id </summary>
		/// <returns> <see cref="EmployeeDomainModel"/> </returns>
		/// <param name="id"> The unique id of the <see cref="Employee"/> </param>
		/// <response code="200"> <see cref="EmployeeDomainModel"/> </response>
		/// <response code="500"> An error happened and was not able to pull a <see cref="Employee"/> by its id </response>
		[HttpGet("{id}", Name = "GetEmployee")]
		[ProducesResponseType(typeof(EmployeeDomainModel), 200)]
		[ProducesResponseType(typeof(void), 500)]
		public async Task<IActionResult> GetAsync(Guid id)
		{
			if (id == Guid.Empty) return BadRequest("Id is empty. Id is required.");

			EmployeeDomainModel model;

			try
			{
				model = await _domain.GetAsync(id).ConfigureAwait(false); 
				if (model == null) return NotFound();
			}
			catch (Exception ex) { return BadRequest(ex.InnerException); }

			return new ObjectResult(model);
		}

		/// <summary> This will insert a <see cref="Employee"/> </summary>
		/// <returns> <see cref="EmployeeDomainModel"/> </returns>
		/// <param name="model"> <see cref="EmployeeDomainModel"/> </param>
		/// <response code="201"> <see cref="EmployeeDomainModel"/> </response>
		/// <response code="500"> An error happened and was not able to create a <see cref="Employee"/> </response>
		[HttpPost]
		[ProducesResponseType(typeof(EmployeeDomainModel), 201)]
		[ProducesResponseType(typeof(void), 400)]
		public async Task<IActionResult> CreateAsync([FromBody] EmployeeDomainModel model)
		{
			if (model == null) return BadRequest("The model that is passed in is empty. Model object is required.");

			EmployeeDomainModel inserted;

			try { inserted = await _domain.InsertAsync(model).ConfigureAwait(false); }
			catch (Exception ex) { return BadRequest(ex.InnerException); }

			return CreatedAtRoute("GetEmployee", new { id = inserted.EmployeeId }, inserted);
		}

		/// <summary> This will update a <see cref="Employee"/> </summary>
		/// <returns> <see cref="EmployeeDomainModel"/> </returns>
		/// <param name="id"> The unique id of the <see cref="Employee"/> </param>
		/// <param name="model"> <see cref="EmployeeDomainModel"/> </param>
		/// <response code="204"> <see cref="EmployeeDomainModel"/> </response>
		/// <response code="400"> Cannot update a null <see cref="Employee"/> model </response>
		/// <response code="500"> An error happened and was not able to update a <see cref="Employee"/> by its id </response>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(void), 204)]
		[ProducesResponseType(typeof(void), 400)]
		[ProducesResponseType(typeof(void), 404)]
		public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EmployeeDomainModel model)
		{
			if (model == null || model.EmployeeId != id) return BadRequest("The model that is passed in is empty. Model object is required.");

			EmployeeDomainModel selected;
			try
			{
				selected = await _domain.GetAsync(id).ConfigureAwait(false);
				if (selected == null) return NotFound("The asset category selected to be updated could not be found.");
			}
			catch (Exception ex) { return BadRequest(ex.InnerException); }

			selected.FirstName = model.FirstName;
			selected.LastName = model.LastName;
			selected.ModifiedDate = DateTime.UtcNow;

			try { await _domain.UpdateAsync(selected).ConfigureAwait(false); }
			catch (Exception ex) { return BadRequest(ex.InnerException); }

			return Ok(selected);
		}
	}
}