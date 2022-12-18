using OnlineStore.Domain.Entities;
using OnlineStore.Models.Responses;

namespace OnlineStore.WebApi.Mappers;

public class HttpModelsMapper
{
    public virtual ProductResponse MapProductModelV1(Product obj) 
        => new(obj.Id, obj.Name, obj.Price);
}