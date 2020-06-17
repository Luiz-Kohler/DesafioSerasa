using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class DebitMapConfig : IEntityTypeConfiguration<Debit>
    {
        public void Configure(EntityTypeBuilder<Debit> builder)
        {
            builder.ToTable(DebitMapName.TableName);

            builder.Property(d => d.Id).UseIdentityColumn().HasColumnName(DebitMapName.Id);

            builder.HasOne(d => d.Company).WithMany(c => c.Debits).HasForeignKey(d => d.CompanyId);
        }
        private static class DebitMapName
        {
            public const string TableName = "DEBIT";
            public const string Id = "ID";
        }
    }
}
