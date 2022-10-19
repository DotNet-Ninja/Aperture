using Aperture.Constants;
using Aperture.Extensions;

namespace Aperture.Entities;

public class SampleData
{
    public static void AddSamplePhotos(ApertureDb db, int quantity)
    {
        if (db.Tags.Any() || db.Photos.Any())
        {
            return;
        }
        var rnd = new Random(DateTime.Now.Millisecond);
        var tags = Source.Tags;
        var text = new Dictionary<int, string>()
        {
            {1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
            {2, "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
            {3, "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum, sed do eiusmod tempor incididunt ut labore."}
        };

        var photos = Enumerable.Range(1, quantity)
            .Select(index=> new
            {
                Ordinal = index,
                PhotoIndex = rnd.Next(1, Source.Photos.Count + 1)
            })
            .Select(item => new
            {
                Ordinal = item.Ordinal,
                Index = item.PhotoIndex,
                Photo = Source.Photos[item.PhotoIndex],
                Date = Source.GenerateDate(item.Ordinal)
            })
            .Select(p => new Photo
            {
                Title = $"{p.Photo.Name} #{p.Ordinal}",
                Tags = tags.Where(t => p.Photo.TagNames.Contains(t.Name)).ToList(),
                FileName = $"{p.Photo.Name.Slugify()}.00{p.Index}.jpg",
                ExposureSummary = "Canon EF-M 15-45mm f/3.5-6.3 @ 45mm - 1/60 sec. @ f/8, ISO 100",
                Caption = text[p.Ordinal%text.Count+1],
                ContentType = "image/jpeg",
                DateCreated = p.Date,
                DateUploaded = p.Date,
                Metadata = new List<Property>
                {
                    new Property
                    {
                        Tag = MetadataTag.Make,
                        Name = "Camera Make",
                        Value = "Canon"
                    }
                },
                FullUrl = new Uri($"/images/samples/sample.00{p.Index}.full.jpg", UriKind.Relative),
                LargeUrl = new Uri($"/images/samples/sample.00{p.Index}.full.jpg", UriKind.Relative),
                SmallUrl = new Uri($"/images/samples/sample.00{p.Index}.small.jpg", UriKind.Relative),
                ThumbnailUrl = new Uri($"/images/samples/sample.00{p.Index}.thumb.jpg", UriKind.Relative),
                IconUrl = new Uri($"/images/samples/sample.00{p.Index}.icon.jpg", UriKind.Relative),
                Orientation = Orientation.Landscape,
                Slug = $"{p.Photo.Name} #{p.Ordinal}".Slugify()
            });
        db.Tags.AddRange(tags);
        db.Photos.AddRange(photos);
        db.SaveChanges();
    }

    private class Source
    {
        private int Index { get; set; }
        public string Name { get; set; } = string.Empty;
        private string FullName { get; set; } = string.Empty;
        private string SmallName { get; set; } = string.Empty;
        private string ThumbName { get; set; } = string.Empty;
        public List<string> TagNames { get; set; } = new List<string>();

        public static List<Tag> Tags => new List<Tag>
        {
            new Tag { Name = "Flag" },
            new Tag {Name="America"},
            new Tag {Name="Ocean"},
            new Tag {Name="Oregon"},
            new Tag {Name="Flower"},
            new Tag {Name="Tree"},
            new Tag {Name="North Shore"},
            new Tag{Name="Wildlife"},
            new Tag {Name="Sample"}
        };

        public static DateTimeOffset GenerateDate(int ordinal)
        {
            return DateTimeOffset.Now;
        }

        public static Dictionary<int, Source> Photos => new Dictionary<int, Source>
        {
            {
                1,
                new Source
                {
                    Index = 1,
                    Name = "Flag",
                    FullName = "sample.001.full.jpg",
                    SmallName = "sample.001.small.jpg",
                    ThumbName = "sample.001.thumb.jpg",
                    TagNames = { "Flag", "America", "Sample" }
                }
            },
            {
                2,
                new Source
                {
                    Index = 2,
                    Name = "Oregon Coast",
                    FullName = "sample.002.full.jpg",
                    SmallName = "sample.002.small.jpg",
                    ThumbName = "sample.002.thumb.jpg",
                    TagNames = { "Ocean", "Oregon", "Sample" }
                }
            },
            {
                3,
                new Source
                {
                    Index = 3,
                    Name = "Flower",
                    FullName = "sample.003.full.jpg",
                    SmallName = "sample.003.small.jpg",
                    ThumbName = "sample.003.thumb.jpg",
                    TagNames = { "Flower", "Sample" }
                }
            },
            {
                4,
                new Source
                {
                    Index = 4,
                    Name = "Fungus",
                    FullName = "sample.004.full.jpg",
                    SmallName = "sample.004.small.jpg",
                    ThumbName = "sample.004.thumb.jpg",
                    TagNames = { "Tree", "North Shore", "Sample" }
                }
            },
            {
                5,
                new Source
                {
                    Index = 5,
                    Name = "North Shore",
                    FullName = "sample.005.full.jpg",
                    SmallName = "sample.005.small.jpg",
                    ThumbName = "sample.005.thumb.jpg",
                    TagNames = { "Sample" }
                }
            },
            {
                6,
                new Source
                {
                    Index = 6,
                    Name = "Ducky",
                    FullName = "sample.006.full.jpg",
                    SmallName = "sample.006.small.jpg",
                    ThumbName = "sample.006.thumb.jpg",
                    TagNames = { "North Shore", "Wildlife", "Sample" }
                }
            }
        };
    }
}
