﻿using HotelWeb.Utils.FileUploadService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace HotelWeb.Utils.FileUploadService
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly IHostingEnvironment environment;
        public LocalFileUploadService(IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var filePath = Path.Combine(environment.ContentRootPath, @"wwwroot\img", file.FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return filePath;
        }
    }
}
