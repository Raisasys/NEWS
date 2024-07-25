using System.Diagnostics;
using System.Reflection;

namespace Core
{
	public static class Mapper
	{
		private static readonly MapperOptimized Profile = new MapperOptimized();
		
		public static void CreateMap<TSource, TDestination>()
			where TSource : Entity
			where TDestination : IDto, new()
		{
			var sourceType = typeof(TSource);
			var targetType = typeof(TDestination);
			Profile.MapTypes(sourceType,targetType);
		}

		public static TDestination Map<TSource, TDestination>(this TSource source) 
			where TSource : Entity
			where TDestination : IDto, new()
		{
			var target = new TDestination();
			Profile.Copy(source, target);
			return target;
		}

		public static IEnumerable<TDestination> Map<TSource, TDestination>(this IEnumerable<TSource> sources)
			where TSource : Entity
			where TDestination : IDto, new()
		{
			var targets = new List<TDestination>();
			foreach (var source in sources) 
				targets.Add(Map<TSource, TDestination>(source));
			return targets;
		}

	}
}