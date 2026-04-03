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

            builder.Property(e => e.Id).HasColumnName("id");
            
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.HasIndex(e => e.UserId).IsUnique();

            builder.Property(e => e.AccountStatus).HasColumnName("account_status");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Surname).HasColumnName("surname");
            builder.Property(e => e.Patronymic).HasColumnName("patronymic");
            builder.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            builder.Property(e => e.Gender).HasColumnName("gender");
            builder.Property(e => e.Citizenship).HasColumnName("citizenship");
        }
    }
}