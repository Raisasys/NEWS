namespace Service.Web;

[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
public class DevelopmentControllerAttribute : Attribute
{
    public DevelopmentControllerAttribute()
    {
    }
}