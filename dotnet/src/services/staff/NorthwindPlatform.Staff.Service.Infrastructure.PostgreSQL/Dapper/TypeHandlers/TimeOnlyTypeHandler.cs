using System.Data;
using static Dapper.SqlMapper;

namespace NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Dapper.TypeHandlers
{
    public sealed class TimeOnlyTypeHandler : TypeHandler<TimeOnly>
    {
        public override void SetValue(IDbDataParameter parameter, TimeOnly value)
        {
            parameter.DbType = DbType.Time;
            parameter.Value = value.ToTimeSpan();
        }

        public override TimeOnly Parse(object value)
        {
            return value switch
            {
                TimeSpan ts => TimeOnly.FromTimeSpan(ts),
                TimeOnly t => t,
                null => throw new InvalidCastException("Cannot convert null to TimeOnly"),
                _ => throw new InvalidCastException($"Cannot convert {value.GetType()} to TimeOnly")
            };
        }
    }
}