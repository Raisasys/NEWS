namespace Core;

public record Address
{
    // EF
    private Address() { }

    public string Country { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string Detail { get; private set; } = default!;
    public string PostalCode { get; private set; } = default!;

    public static Address Empty => new();

    public static Address Of(string country, string city, string detail, string postalCode)
    {
        var address = new Address
        {
            Country = country,
            City = city,
            Detail = detail,
            PostalCode = postalCode
        };

        return address;
    }
}
