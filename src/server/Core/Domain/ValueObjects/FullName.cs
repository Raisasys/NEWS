namespace Core;

public record FullName(string FirstName, string LastName, string MiddleName = null)
{
    public FullName() : this(null,null)
    {

    }
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public string MiddleName { get; set; } = MiddleName;

    public static FullName Of(string firstName, string lastName, string middleName = null) =>
        new()
        {
            FirstName = firstName,
            LastName= lastName,
            MiddleName = middleName
        };
}