using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMDTDD.Persistanse.EF.UsageType;

public class UsageTypeEntityMap : IEntityTypeConfiguration<Entities.UsageType>
{
    public void Configure(EntityTypeBuilder<Entities.UsageType> _)
    {
        _.ToTable("UsageType");
        _.HasKey("Id");
        _.Property(_ => _.Id)
            .ValueGeneratedOnAdd();
        _.Property(_ => _.Name)
            .IsRequired(true)
            .HasMaxLength(30);
    }
}