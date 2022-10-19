namespace Aperture.Tests;

public class TestEntity
{
    public string Name { get; set; } = string.Empty;

    public static List<TestEntity> CreateTestEntities(int count)
    {
        return Enumerable.Range(1, count).Select(index => new TestEntity() { Name = $"Entity {index}" }).ToList();
    }
}