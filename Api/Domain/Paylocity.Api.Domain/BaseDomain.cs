namespace Paylocity.Api.Domain
{
	using System.Collections.Generic;
	using Model;
	using Repository.Model;
	using AutoMapper;

	public class BaseDomain
	{
		private readonly IMapper _mapper;

		protected BaseDomain()
		{
			var configuration = new MapperConfiguration
			(
				config =>
				{
					config.CreateMap<EmployeeDomainModel, Employee>().ReverseMap();
					config.CreateMap<DependentDomainModel, Dependent>().ReverseMap();
				}
			);

			_mapper = configuration.CreateMapper();
		}

		protected TTarget ToModel<TTarget, TSource>(TSource source)
			where TTarget : class
			where TSource : class
		{
			TTarget target = _mapper.Map<TTarget>(source);

			return target;
		}

		protected IList<TTarget> ToListModel<TTarget, TSource>(IList<TSource> source)
			where TTarget : class
			where TSource : class
		{
			IList<TTarget> target = _mapper.Map<IList<TTarget>>(source);

			return target;
		}
	}
}
