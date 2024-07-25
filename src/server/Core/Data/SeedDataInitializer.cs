namespace Core;

public interface ISeedDataInitializerBase { }

public abstract class SeedDataInitializer: ISeedDataInitializerBase
{
    public abstract Task Initialize(IUowDatabase database);
}

public abstract class SeedDevDataInitializer : ISeedDataInitializerBase
{
    public abstract Task Initialize(IUowDatabase database);
}