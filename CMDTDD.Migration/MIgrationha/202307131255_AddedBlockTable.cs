using FluentMigrator;

namespace CMDTDD.Migration.MIgrationha;
[Migration(202307131255)]
public class _202307131255_AddedBlockTable : FluentMigrator.Migration 
{
    public override void Up()
    {
        CteateTable();
    }


    public override void Down()
    {
        Delete.Table("Blocks");
    }
    private void CteateTable()
    {
        Create.Table("Blocks").WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(30).NotNullable().WithColumn("UnitCount")
            .AsInt32().NotNullable()
            .WithColumn("ComplexId").AsInt32().NotNullable()
            .ForeignKey("FK_Blocks_Compelexes", "Complexes", "Id");
    }
}