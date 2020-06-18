using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class CompanyMapConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(CompanyMapName.TableName);

            builder.Property(c => c.Id).UseIdentityColumn().HasColumnName(CompanyMapName.Id);

            builder.Property(c => c.Name).IsRequired().HasColumnType(CompanyMapName.Type_Name).HasColumnName(CompanyMapName.ColumnName_Name);

            builder.Property(c => c.Reliability).IsRequired().HasColumnType(CompanyMapName.Type_Reliability).HasColumnName(CompanyMapName.ColumnName_Reliability);
        }

        private static class CompanyMapName
        {
            public const string TableName = "COMPANY";
            public const string Id = "ID";

            public const string Type_Name = "VARCHAR(50)";
            public const string ColumnName_Name = "NAME";

            public const string Type_Reliability = "INT";
            public const string ColumnName_Reliability = "RELIABILITY";
        }
    }
}
