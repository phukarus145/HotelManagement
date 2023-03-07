﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HotelWeb.Utils.FileUploadService
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
