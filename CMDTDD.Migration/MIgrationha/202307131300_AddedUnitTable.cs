using FluentMigrator;

namespace CMDTDD.Migration.MIgrationha
{
    [Migration(2023071300)]
    public class _202307131300_AddedUnitTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            CreateTable();
        }


        public override void Down()
        {
            Delete.Table("Units");
        }
        private void CreateTable()
        {
            Create.Table("Units").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(30).NotNullable()
                .WithColumn("ResidenseType").AsInt32().NotNullable().WithColumn("BlockId")
                .AsInt32().NotNullable().ForeignKey("FK_Units_Blocks", "Blocks", "Id");
        }
    }
}