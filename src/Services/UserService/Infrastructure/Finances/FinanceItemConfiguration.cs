using Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Finances;

internal sealed class FinanceItemConfiguration : IEntityTypeConfiguration<Domain.Finances.Transaction>
{
    public void Configure(EntityTypeBuilder<Domain.Finances.Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TransactionDate).HasConversion(d => d != null ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : d, v => v);

        builder.HasOne<User>().WithMany().HasForeignKey(t => t.UserId);
    }
}
