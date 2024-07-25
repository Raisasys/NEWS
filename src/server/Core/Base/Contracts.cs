using System.Reflection.Emit;

namespace Core;

public interface IContract { }

public interface IPersonName : IContract
{
    FullName FullName { get; }
    public string DisplayName => FullName.ToString();
}