using Core;

namespace Domain;

public interface IOrbitDomainService
{
    Orbit CreateOrbit(string title, OwnerCompany company, int startYear, Month startMonth,int yearDuration);
}

internal class OrbitDomainService : DomainService, IOrbitDomainService
{
    public Orbit CreateOrbit(string title , OwnerCompany company, int startYear, Month startMonth, int yearDuration)
    {
        var orbit = new Orbit(title, company, startYear, startMonth, yearDuration);
        //orbit.SetYears();
        Database.Add(orbit);
        return orbit;
    }

    
}

