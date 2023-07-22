

using CMDTDD.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMDTDD.Persistanse.EF.Complexes;

public class ComplexEntityMap : IEntityTypeConfiguration<Complex>
{
    public void Configure(EntityTypeBuilder<Complex> _)
    {
        _.ToTable("Complexes");
        _.HasKey(_ => _.Id);
        _.Property(_ => _.Id).ValueGeneratedOnAdd();
        _.Property(_ => _.Name).IsRequired().HasMaxLength(30);
    
    }
}