using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class InvoiceMapConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable(InvoiceMapName.TableName);

            builder.Property(d => d.Id).UseIdentityColumn().HasColumnName(InvoiceMapName.Id);

            builder.HasOne(d => d.Company).WithMany(c => c.Invoices).HasForeignKey(d => d.CompanyId);
        }

        private static class InvoiceMapName
        {
            public const string TableName = "INVOICE";
            public const string Id = "ID";
        }
    }
}
