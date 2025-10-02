namespace ReportGeneratorService.Core.Entities;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; }
    protected Entity() {}

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object obj)
    {
        if (obj is not Entity<TId> other)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        if (GetType() != other.GetType())
            return false;
        if (Id.Equals(default(TId)) || other.Id.Equals(default(TId)))
            return false;
        
        return Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TId> a, Entity<TId> b)
    {
        if (a is null && b is null)
            return true;
        if (a is null || b is null)
            return false;
        
        return a.Equals(b);
    }

    public static bool operator !=(Entity<TId> a, Entity<TId> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}