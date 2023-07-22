using CMDTDD.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMDTDD.Persistanse.EF.Blocks;

public class BlockEntityMap : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> _)
    {
        _.ToTable("Blocks");
        _.HasKey(_ => _.Id);
        _.Property(_ => _.Id).ValueGeneratedOnAdd();
        _.Property(_ => _.UnitCount).IsRequired();
        _.Property(_ => _.ComplexId).IsRequired();

        _.HasOne(_ => _.Complex)
            .WithMany(_ => _.Blocks)
            .HasForeignKey(_ => _.ComplexId);
    }
}