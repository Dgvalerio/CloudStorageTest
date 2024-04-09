using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;

public class UploadProfilePhotoUseCase: IUploadProfilePhotoUseCase
{
    private readonly IStorageService _storageService;

    public UploadProfilePhotoUseCase(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public void Execute(IFormFile file)
    {
        var fileStream = file.OpenReadStream();

        var isImage = fileStream.Is<JointPhotographicExpertsGroup>();

        if (isImage == false)
        {
            throw new Exception("This file is not an image.");
        }

        var user = GetFromDatabase();
        
        _storageService.Upload(file, user);
    }

    private User GetFromDatabase()
    {
        return new User
        {
            Id = 1,
            Name = "Davi",
            Email = "al.davigvalerio@gmail.com",
            // todo: insert RefreshToken and AccessToken from https://developers.google.com/oauthplayground and https://console.cloud.google.com 
            RefreshToken = "",
            AccessToken = ""
        };
    }
}