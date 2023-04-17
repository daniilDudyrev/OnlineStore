using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OnlineStore.Data.Conveters;

public class RolesValueConverter : ValueConverter<string[], string>
{
    public RolesValueConverter() : base(
        value => string.Join(';', value),
        dbValue => dbValue.Split(';', StringSplitOptions.RemoveEmptyEntries))
    {
    }
}