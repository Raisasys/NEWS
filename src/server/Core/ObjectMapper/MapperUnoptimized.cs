using System;

namespace Core
{
    public class MapperUnoptimized
    {
        public void MapTypes(Type source, Type target)
        {
        }

        public void Copy(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            var propMap = GetMatchingProperties(sourceType, targetType);
           
            for (var i = 0; i < propMap.Count; i++)
            {
                var prop = propMap[i];
                var sourceValue = prop.SourceProperty.GetValue(source, null);
                prop.TargetProperty.SetValue(target, sourceValue, null);
            }
        }

        protected IList<PropertyMap> GetMatchingProperties(Type sourceType, Type targetType)
        {
	        var sourceProperties = sourceType.GetProperties();
	        var targetProperties = targetType.GetProperties();

	        var properties = (from s in sourceProperties
		        from t in targetProperties
		        where s.Name == t.Name &&
		              s.CanRead &&
		              t.CanWrite &&
		              s.PropertyType == t.PropertyType
		        select new PropertyMap
		        {
			        SourceProperty = s,
			        TargetProperty = t
		        }).ToList();
	        return properties;
        }

	}
}