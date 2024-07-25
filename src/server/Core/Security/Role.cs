namespace Core;

public enum Role
{
    CompanyPersonnel = 0,
    CompanyOwner = 10,
    Operator = 30,
    Consultant = 50,
    Admin = 100,
}

public static class Roles
{
    public static string CompanyPersonnel = nameof(Role.CompanyPersonnel);
    public static string CompanyOwner = nameof(Role.CompanyOwner);
    public static string Operator = nameof(Role.Operator);
    public static string Consultant = nameof(Role.Consultant);
    public static string Admin = nameof(Role.Admin);
}
