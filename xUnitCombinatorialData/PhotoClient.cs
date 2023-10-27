namespace xUnitCombinatorialData;

public interface IPhotoClient
{
    Task UploadPhoto(string fileName, byte[] fileData);
}

public class PhotoClient : IPhotoClient
{
    public Task UploadPhoto(string fileName, byte[] fileData)
    {
        throw new NotImplementedException();
    }
}
