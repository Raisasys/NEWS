using System.Security.Cryptography;

namespace Core;

public abstract class Aggregate : GuidEntity, IAggregate<Guid> { }
public abstract class IntAggregate : IntEntity, IAggregate<int> { }
public abstract class LongAggregate : LongEntity, IAggregate<long> { }
public abstract class StringAggregate : StringEntity, IAggregate<string> { }

