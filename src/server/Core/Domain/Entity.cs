using System.Reflection;
using System.Text;
using static System.String;

namespace Core;


public abstract class Entity : IEntity
{
    protected internal abstract void InitializeKey();
    protected internal virtual void Initialize() { }
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public string CreatedBy { get; set; }

    public DateTimeOffset? LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; }


    public abstract object GetId();

    public override int GetHashCode() => GetId().GetHashCode();

    protected virtual Task ValidateProperties() => Task.CompletedTask;
    protected virtual Task ValidateObject(IReadonlyDatabase database) => Task.CompletedTask;

    public async Task Validate(IReadonlyDatabase database)
    {
        if (IsDeleted) throw new CoreException(CoreExceptionType.Deleted);
        await ValidateProperties();
        await ValidateObject(database);
    }

    public virtual IEntity Clone()
    {
        var result = (Entity)MemberwiseClone();
        return result;
    }
    
    public override bool Equals(object @object) => Equals(@object as Entity);

    public abstract bool Equals(Entity @object);

    public static bool operator ==(Entity left, object right)
    {
        if (right == null) return left == null;

        if (right is Entity rightEntity)
            return left == rightEntity;

        if (right is Task)
            throw new Exception("You cannot compare an entity object with a Task object. Await the task before comparing.");

        return false;
    }

    public static bool operator !=(Entity left, object right) => !(left == right);

    public static bool operator ==(Entity left, Entity right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right) => !(left == right);


    public virtual int CompareTo(object other)
    {
        return other == null ? 1 : Compare(ToString(), other.ToString(), StringComparison.OrdinalIgnoreCase);
    }


    #region ToString(format)

    static Dictionary<Type, PropertyInfo[]> PrimitiveProperties = new Dictionary<Type, PropertyInfo[]>();
    static object PrimitivePropertiesSyncLock = new object();

    PropertyInfo[] GetPrimitiveProperties()
    {
        var myType = GetType();

        if (PrimitiveProperties.ContainsKey(myType))
        {
            // Already cached:
            return PrimitiveProperties[myType];
        }
        else
        {
            lock (PrimitivePropertiesSyncLock)
            {
                if (PrimitiveProperties.ContainsKey(myType))
                    return PrimitiveProperties[myType];

                var result = ExtractPrimitiveProperties(myType);
                PrimitiveProperties.Add(myType, result);
                return result;
            }
        }
    }

    static PropertyInfo[] ExtractPrimitiveProperties(Type type)
    {
        var result = new List<PropertyInfo>();
        var primitiveTypes = new[] { typeof(string), typeof(int), typeof(int?), typeof(double), typeof(double?), typeof(decimal), typeof(decimal?), typeof(DateTime), typeof(DateTime?) };

        foreach (var p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead).Where(p => primitiveTypes.Contains(p.PropertyType)))
        {
            if (p.Name == nameof(IsDeleted)) continue;
            if (p.Name == nameof(DeletedAt)) continue;
            if (p.PropertyType.Implements<IEntity>()) continue;
            if (p.IsCalculated()) continue;
            result.Add(p);
        }

        return result.ToArray();
    }

    public virtual string ToString(string format)
    {
        if (format == "F")
        {
            var r = new StringBuilder();

            foreach (var p in GetPrimitiveProperties())
            {
                try
                {
                    r.Append(p.GetValue(this)?.ToString() + " ");
                }
                catch
                {
                    // We don't want this method to throw an exception even if some properties cannot be read.
                    // No logging is needed
                }
            }

            return r.ToString();
        }
        else
            return ToString();
    }

    #endregion
    
}

public abstract class Entity<T> : Entity, IEntity<T>
{
    [PrimaryKey]
    public virtual T Id { get; set; }

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(Entity other)
    {
        if (ReferenceEquals(this, other)) return true;

        if (other is not Entity<T> typed) return false;

        if (GetType() != other.GetType()) return false;

        return Id.Equals(typed.Id);
    }

    public override object GetId() => Id;
}

public abstract class IntEntity : Entity<int>
{
    protected IntEntity()
    {
        //InitializeKey();
    }

    protected IntEntity(int id)
    {
        Id = id;
        InitializeKey();
    }

    protected internal override void InitializeKey()
    {
        if (GetType().IsAutoGeneratedKey())
        {
            if (Id != default)
                throw new CoreException(CoreExceptionType.AutoGeneratingKeyType);
        }
        else
        {
            if (Id == default)
                throw new CoreException(CoreExceptionType.EmptyKey);
        }
    }

    public static bool operator !=(IntEntity entity, int? id) => entity?.Id != id;

    public static bool operator ==(IntEntity entity, int? id) => entity?.Id == id;

    public static bool operator !=(IntEntity entity, int id) => entity?.Id != id;

    public static bool operator ==(IntEntity entity, int id) => entity?.Id == id;

    public static bool operator !=(int? id, IntEntity entity) => entity?.Id != id;

    public static bool operator ==(int? id, IntEntity entity) => entity?.Id == id;

    public static bool operator !=(int id, IntEntity entity) => entity?.Id != id;

    public static bool operator ==(int id, IntEntity entity) => entity?.Id == id;

    public override bool Equals(Entity other) => GetType() == other?.GetType() && Id == (other as IntEntity)?.Id;

    public override bool Equals(object other) => Equals(other as Entity);

    public override int GetHashCode() => Id.GetHashCode();
}

public abstract class LongEntity : Entity<long>
{
    protected LongEntity()
    {
        //InitializeKey();
    }

    protected LongEntity(int id)
    {
        Id = id;
        InitializeKey();
    }

    protected internal override void InitializeKey()
    {
        if (GetType().IsAutoGeneratedKey())
        {
            if (Id != default)
                throw new CoreException(CoreExceptionType.AutoGeneratingKeyType);
        }
        else
        {
            if (Id == default)
                throw new CoreException(CoreExceptionType.EmptyKey);
        }
    }

    public static bool operator !=(LongEntity entity, int? id) => entity?.Id != id;

    public static bool operator ==(LongEntity entity, int? id) => entity?.Id == id;

    public static bool operator !=(LongEntity entity, int id) => entity?.Id != id;

    public static bool operator ==(LongEntity entity, int id) => entity?.Id == id;

    public static bool operator !=(int? id, LongEntity entity) => entity?.Id != id;

    public static bool operator ==(int? id, LongEntity entity) => entity?.Id == id;

    public static bool operator !=(int id, LongEntity entity) => entity?.Id != id;

    public static bool operator ==(int id, LongEntity entity) => entity?.Id == id;

    public override bool Equals(Entity other) => GetType() == other?.GetType() && Id == (other as LongEntity)?.Id;

    public override bool Equals(object other) => Equals(other as Entity);

    public override int GetHashCode() => Id.GetHashCode();
}

public abstract class GuidEntity : Entity<Guid>
{
    protected GuidEntity()
    {
        //InitializeKey();
    }

    protected GuidEntity(Guid id)
    {
        Id = id;
        InitializeKey();
    }

    protected internal override void InitializeKey()
    {
        if (GetType().IsAutoGeneratedKey())
        {
            if (Id != default)
                throw new CoreException(CoreExceptionType.AutoGeneratingKeyType);
            else
                Id = Guid.NewGuid();
        }
        else
        {
            if (Id == default)
                throw new CoreException(CoreExceptionType.EmptyKey);
        }
    }

    public override IEntity Clone()
    {
        var result = base.Clone() as GuidEntity;
        result.Id = Id;
        return result;
    }

    public static bool operator !=(GuidEntity entity, Guid? id) => entity?.Id != id;

    public static bool operator ==(GuidEntity entity, Guid? id) => entity?.Id == id;

    public static bool operator !=(GuidEntity entity, Guid id) => entity?.Id != id;

    public static bool operator ==(GuidEntity entity, Guid id) => entity?.Id == id;

    public static bool operator !=(Guid? id, GuidEntity entity) => entity?.Id != id;

    public static bool operator ==(Guid? id, GuidEntity entity) => entity?.Id == id;

    public static bool operator !=(Guid id, GuidEntity entity) => entity?.Id != id;

    public static bool operator ==(Guid id, GuidEntity entity) => entity?.Id == id;

    public override bool Equals(Entity other) => Id == (other as GuidEntity)?.Id;

    public override bool Equals(object other) => Id == (other as GuidEntity)?.Id;

    public override int GetHashCode() => Id.GetHashCode();
}

public abstract class StringEntity : Entity<string>
{
    protected StringEntity()
    {
        //InitializeKey();
    }

    protected StringEntity(string id)
    {
        Id = id;
        InitializeKey();
    }


    protected internal override void InitializeKey()
    {
        if (GetType().IsAutoGeneratedKey())
        {
            if (Id != default)
                throw new CoreException(CoreExceptionType.AutoGeneratingKeyType);
        }
        else
        {
            if (Id == default)
                throw new CoreException(CoreExceptionType.EmptyKey);
        }
    }

    public static bool operator !=(StringEntity entity, string id)
    {
        if (id is null) return !(entity is null);
        return entity?.Id != id;
    }

    public static bool operator ==(StringEntity entity, string id)
    {
        if (id is null) return entity is null;
        return entity?.Id == id;
    }

    public static bool operator !=(string id, StringEntity entity)
    {
        if (id is null) return !(entity is null);
        return entity?.Id != id;
    }

    public static bool operator ==(string id, StringEntity entity)
    {
        if (id is null) return entity is null;
        return entity?.Id == id;
    }

    public override bool Equals(Entity other)
    {
        if (other is StringEntity se)
            return GetType() == se.GetType() && Id == se.Id;
        else
            return false;
    }

    public override bool Equals(object other) => Equals(other as Entity);

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;
}