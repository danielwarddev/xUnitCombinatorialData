using NSubstitute;
using System.Net;
using FluentAssertions;

namespace xUnitCombinatorialData.UnitTests;

public class PhotoServiceTests
{
    private readonly PhotoService _photoService;
    private readonly IPhotoClient _photoClient = Substitute.For<IPhotoClient>();

    public PhotoServiceTests()
    {
        _photoService = new PhotoService(_photoClient);
    }

    [Theory, CombinatorialData]
    public async Task When_Extension_And_Size_Are_Valid_Then_Uploads_Photo_Successfully(
        [CombinatorialMemberData(nameof(PhotoService.ValidFileExtensions), MemberType = typeof(PhotoService))] string fileExtension,
        [CombinatorialValues(1000, 3000, 10000)] int fileSize)
    {
        var fileName = $"myFile{fileExtension}";
        var fileData = new byte[fileSize];

        await _photoService.UploadPhoto(fileName, fileData);
    }
    
    [Theory, CombinatorialData]
    public async Task When_Extension_Is_Invalid_Then_Throws_Exception(
        [CombinatorialValues(".gif", ".txt", ".bmp")] string fileExtension,
        [CombinatorialValues(1000, 3000, 10000)] int fileSize)
    {
        var fileName = $"myFile{fileExtension}";
        var fileData = new byte[fileSize];

        var uploadPhoto = async () => await _photoService.UploadPhoto(fileName, fileData);
        await uploadPhoto.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid file extension");
    }
    
    [Theory, CombinatorialData]
    public async Task When_Size_Is_Over_10kb_Then_Throws_Exception(
        [CombinatorialMemberData(nameof(PhotoService.ValidFileExtensions), MemberType = typeof(PhotoService))] string fileExtension,
        [CombinatorialValues(10001, 20000, 100000)] int fileSize)
    {
        var fileName = $"myFile{fileExtension}";
        var fileData = new byte[fileSize];

        var uploadPhoto = async () => await _photoService.UploadPhoto(fileName, fileData);
        await uploadPhoto.Should().ThrowAsync<ArgumentException>().WithMessage("File size must be under 10kb");
    }
}