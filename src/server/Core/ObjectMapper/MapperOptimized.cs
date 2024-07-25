using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class MapperOptimized
    {
        private readonly Dictionary<string, PropertyMap[]> _maps = new Dictionary<string, PropertyMap[]>();

        public void MapTypes(Type source, Type target)
        {
            var key = GetMapKey(source, target);
            if (_maps.ContainsKey(key))
            {
                return;
            }

            var props = GetMatchingProperties(source, target);
            _maps.Add(key, props.ToArray());
        }

        public void Copy(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();

            var key = GetMapKey(sourceType, targetType);
            if (!_maps.ContainsKey(key))
            {
                MapTypes(sourceType, targetType);
            }

            var propMap = _maps[key];

            for (var i = 0; i < propMap.Length; i++)
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

        protected string GetMapKey(Type sourceType, Type targetType)
        {
	        var keyName = "Copy_";
	        keyName += sourceType.FullName.Replace(".", "_").Replace("+", "_");
	        keyName += "_";
	        keyName += targetType.FullName.Replace(".", "_").Replace("+", "_");

	        return keyName;
        }
	}
}