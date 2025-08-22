// See https://aka.ms/new-console-template for more information
class Program {
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var baseImageUrl = "https://example.com/images   /     ";
        var draft = new
        {
            Images = new List<Image<string>>
            {
                new Image<string> { ExternalStorageId = "image1.jpg" },
                new Image<string> { ExternalStorageId = "image2.jpg" },
                new Image<string> { ExternalStorageId = "image3.jpg" }
            }
        };

        var SectionPhotos = MapSectionPhotos(draft.Images, baseImageUrl);
        foreach (var photo in SectionPhotos)
        {
            Console.WriteLine(photo);
        }
    }
    private static string CombineImageUrl(string baseUrl, string imageId)
    {
        return $"{baseUrl?.TrimEnd().TrimEnd('/')}/{imageId?.TrimStart().TrimStart('/')}"; // need this stronger check?
                                                                                           //return $"{baseUrl.TrimEnd('/')}/{imageId.TrimStart('/')}";
    }
    private static List<string> MapSectionPhotos<T>(IEnumerable<Image<T>> images, string baseUrl)
    {
        if (images == null) return new();

        return images
            .Where(img => !string.IsNullOrWhiteSpace(img.ExternalStorageId))
            .Select(img => CombineImageUrl(baseUrl, img.ExternalStorageId))
            .Distinct()
            .ToList();
    }
}

internal class Image<T>
{
    public string ExternalStorageId { get; set; }
}