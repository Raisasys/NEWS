
using Newtonsoft.Json;

namespace Core;
public class EnumResponseJson<TEnum>
{
	[JsonIgnore]
	public static Type Type => typeof(TEnum);

	public string EnumType { get; set; } = Type.Name;

	public int Value { get; set; }
}