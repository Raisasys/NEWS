namespace Core;

[AttributeUsage(AttributeTargets.Class)]
public class ResolveCommandFromAttribute(ResolveCommandFromType fromType) : Attribute
{
    public ResolveCommandFromType FromType { get; private set; } = fromType;
}