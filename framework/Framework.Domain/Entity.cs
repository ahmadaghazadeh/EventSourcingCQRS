namespace Framework.Domain
{
	public abstract class Entity<T>
{
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
    }
    
}
