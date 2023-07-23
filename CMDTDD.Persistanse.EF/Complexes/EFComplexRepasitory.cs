using CMDTDD.Entities;
using CMDTDD.Persistanse.EF;
using CMDTDD.Services.Complexes.Contracts;
using CMDTDD.Services.Complexes.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace CMS.Service.Unit.Test.Complexes;

public class EFComplexRepasitory : ComplexRepasitory
{
    private readonly DbSet<Complex> _complexes;
    private readonly DbSet<ComplexUsageType> _complexUsageType;
    private readonly DbSet<UsageType> _usageType;
    private readonly DbSet<CMDTDD.Entities.Unit> _units;
    private readonly DbSet<Block> _blocks;


    public EFComplexRepasitory(EFDataContext context)
    {
        _complexes = context.Set<Complex>();
        _complexUsageType = context.Set<ComplexUsageType>();
        _usageType = context.Set<UsageType>();
        _units = context.Set<CMDTDD.Entities.Unit>();
        _blocks = context.Set<Block>();
    }

    public void Add(Complex complex)
    {
        _complexes.Add(complex);
    }

    public bool IsExistById(int ComplexId)
    {
        return
            _complexes.Any(_ => _.Id == ComplexId);
    }

    public List<GetAllComplexesWithUnitCountDeataiDto>
        GetAllComplexesWithUnitCountDeatail()
    {
        return
            _complexes.Select(c =>
                new GetAllComplexesWithUnitCountDeataiDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    RegisterdUnitCount = c.Blocks.SelectMany(_ => _.Units).Count(),
                    LeftUnitCount = c.UnitCount -
                                    c.Blocks.SelectMany(_ => _.Units).Count(),
                }).ToList();
    }

    public List<GetAllComplexWithBlockDetaisDto> GetAllComplexWithBlockDetails(
        string? blockName)
    {
        var result = _complexes.Select(_ => new GetAllComplexWithBlockDetaisDto
        {
            Name = _.Name,
            Block = _.Blocks
                .Where(_ => blockName != null ? _.Name == blockName : true).Select(
                    b => new ComplexBlockDto
                    {
                        BlockName = b.Name,
                        BlockUnitCount = b.UnitCount,
                        RemainingUnitCount = b.UnitCount - b.Units.Count()
                    }).ToList()
        });


        return result.ToList();
    }

    public List<GetAllUsageTypesOfComplexDto> GetAllUsageTypesByComplexId(int
        complexId)
    {
        return
            (from cut in _complexUsageType
                join u in _usageType on cut.UsageTypeId equals u.Id
                where cut.ComplexId == complexId
                select new GetAllUsageTypesOfComplexDto
                {
                    UsageTypeId = u.Id,
                    UsageTypeName = u.Name
                }
            ).ToList();
    }

    public Complex FindeById(int id)
    {
        return
            _complexes.Find(id);
    }

    public void Update(Complex complex)
    {
        _complexes.Update(complex);
    }

    public bool HasUnitsById(int complexId)
    {
        return
            _units.Any(_ => _.Block.ComplexId == complexId);

        // var complex = _complexes.Where(_ => _.Id == id).Include(_ => _.Blocks)
        //     .ThenInclude(_ => _.Units);
        //
        // return 
        // complex.Any(_ => _.Blocks.Any(_ => _.Units.Count() == 1));
    }

    public void Delete(Complex complex)
    {
        _complexes.Remove(complex);
    }

    public int GetUnitCount(int complexId)
    {
        return
            _complexes.Where(_ => _.Id == complexId)
                .Select(_ => _.UnitCount)
                .FirstOrDefault();
    }

    public int GetLeftUnitCounts(int complexId)
    {
        return
            _blocks.Where(_ => _.ComplexId == complexId)
                .Select(_ => _.UnitCount).Sum();
    }



    public GetOneComplexWithBlockAndUnitcountDeatilAndDto GetOne(int id)
    {
        return
            _complexes
                .Where(_ => _.Id == id)
                .Select(_ => new
                    GetOneComplexWithBlockAndUnitcountDeatilAndDto()
                    {
                        Id = _.Id,
                        Name = _.Name,
                        RegisterdUnitCount =
                            _.Blocks.SelectMany(_ => _.Units).Count(),
                        LeftUnitCount = _.UnitCount -
                                        _.Blocks.SelectMany(_ => _.Units).Count(),
                        BlockCount = _.Blocks.Count()
                    })
                .FirstOrDefault();
    }
}