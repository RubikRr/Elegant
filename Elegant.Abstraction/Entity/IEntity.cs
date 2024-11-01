namespace Elegant.Abstraction.Entity;

[PublicAPI]
public interface IEntity<TPrimaryKey>
{
    TPrimaryKey Id { get; set; }
}

public interface IEntity : IEntity<Guid>;
