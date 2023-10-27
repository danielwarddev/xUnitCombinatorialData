namespace xUnitCombinatorialData;

public interface IPhotoService
{
    Task UploadPhoto(string fileName, byte[] data);
}

public class PhotoService : IPhotoService
{
    public static readonly string[] ValidFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
    private readonly IPhotoClient _photoClient;

    public PhotoService(IPhotoClient photoClient)
    {
        _photoClient = photoClient;
    }

    public async Task UploadPhoto(string fileName, byte[] fileData)
    {
        if (fileData.Length > 10000)
        {
            throw new ArgumentException("File size must be under 10kb");
        }

        var fileExtension = fileName.Substring(fileName.LastIndexOf('.'));
        if (!ValidFileExtensions.Contains(fileExtension))
        {
            throw new ArgumentException("Invalid file extension");
        }

        await _photoClient.UploadPhoto(fileName, fileData);
    }
}
