using OnlineStore.Domain.Entities;
using OnlineStore.Models.Responses;

namespace OnlineStore.WebApi.Mappers;

public class HttpModelsMapper
{
    public virtual ProductResponse MapProductModel(Product obj)
        => new(obj.Id, obj.Name, obj.Price, obj.Image, obj.Description, obj.CategoryId);

    public virtual CartItemResponse MapCartItemModel(CartItem obj)
        => new(obj.ProductId, obj.Quantity);
}