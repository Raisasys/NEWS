using Core;

namespace Domain
{
    public abstract class NewsDestination : GuidEntity
    {

	}


	
	public class NewsPublicDestination : NewsDestination
	{
		protected NewsPublicDestination() { }
		public NewsPublicDestination(bool authenticated) => Authenticated =authenticated;
		public bool Authenticated { get; set; }
    }

    public class NewsScopeDestination : NewsDestination
	{
        protected NewsScopeDestination(){}
        public NewsScopeDestination(IEnumerable<Guid> scopes) => Scopes = scopes.Select(i => new ScopeDestination(i)).ToList();
		public virtual ICollection<ScopeDestination> Scopes { get; set; }
	}

    public class ScopeDestination : GuidEntity
    {
	    protected ScopeDestination() { }
		public ScopeDestination(Guid scopeId) => ScopeId = scopeId;

		public Guid ScopeId { get; set; }
    }

}
