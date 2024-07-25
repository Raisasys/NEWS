

namespace Core;

public class BusinessRuleValidationException(IBusinessRule brokenRule) : CoreException(brokenRule.Message)
{
    public IBusinessRule BrokenRule { get; } = brokenRule;
    public string Details { get; } = brokenRule.Message;

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }
}