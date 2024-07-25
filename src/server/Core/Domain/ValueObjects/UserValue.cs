namespace Core;

public record UserValue(string Email, string FirstName, string LastName)
{
    public UserValue() : this(string.Empty, string.Empty, string.Empty)
    {

    }

    public UserValue(string email, FullName fullName) : this(email, fullName.FirstName, fullName.LastName)
    {

    }


    public string Email { get; init; } = Email;
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    
    [Calculated]
    public string Name => $"{FirstName} {LastName}";
}