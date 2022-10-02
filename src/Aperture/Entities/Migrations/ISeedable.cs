namespace Aperture.Entities.Migrations;

public interface ISeedable
{
    void SeedData(DateTimeOffset date);
}