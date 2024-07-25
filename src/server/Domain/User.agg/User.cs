using Core;

namespace Domain;

public class User : StringAggregate, IPersonName
{
    protected User() { }
    public User(Email email)
    {
        Id = email;
    }
    public User(Email email, FullName fullName, PhoneNumber phoneNumber, Address address)
    {
        Id = email;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Address = address;
    }
    public User(Email email, FullName fullName)
    {
        Id = email;
        FullName = fullName;
    }

    public FullName FullName { get; }
    public PhoneNumber PhoneNumber { get; set; }
    public Address Address { get; set; }
    public bool Registered { get; set; }

    public UserValue ToValue() => new (Id, FullName);
}

