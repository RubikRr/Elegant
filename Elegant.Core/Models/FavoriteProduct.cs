namespace Elegant.Core.Models;

//TODO подумать в сторону того чтобы поменять свойства. Мб привезать к конкертному пользователю его список любимых продуктов.
public class FavoriteProduct : IEntity
{
    public Guid Id { get; set; }
    public int UserId { get; init; }
    public Product Product { get; init; } = new();
}