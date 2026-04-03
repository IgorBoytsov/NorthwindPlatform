using System.Data;
using static Dapper.SqlMapper;

namespace NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Dapper.TypeHandlers
{
    public sealed class DateOnlyTypeHandler : TypeHandler<DateOnly>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
        parameter.DbType = DbType.Date;
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
        }
        
        public override DateOnly Parse(object value)
        {
            return value switch
            {
                DateTime dt => DateOnly.FromDateTime(dt),
                DateOnly d => d,
                null => throw new InvalidCastException("Cannot convert null to DateOnly"),
                _ => throw new InvalidCastException($"Cannot convert {value.GetType()} to DateOnly")
            };
        }
    }
}