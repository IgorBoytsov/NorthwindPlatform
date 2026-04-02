using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindPlatform.Staff.Service.Application.Models.Read;

namespace NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Persistence.Configurations
{
    public sealed class EmployeeReadConfiguration : IEntityTypeConfiguration<EmployeeRead>
    {
        public void Configure(EntityTypeBuilder<EmployeeRead> builder)
        {
            builder.ToTable("employees");
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.UserId).IsUnique();
        }
    }
}