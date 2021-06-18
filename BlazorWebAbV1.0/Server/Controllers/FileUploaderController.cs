using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorWebAbV1.Server.Models;
using BlazorWebAbV1.Shared;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BlazorWebAbV1.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        private readonly IWebHostEnvironment env;

        public FileUploadController
    (IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpPost]
        public void Post(UploadedFile uploadedFile)
        {
            var path = $"{env.WebRootPath}\\{uploadedFile.FileName}";
            var fs = System.IO.File.Create(path);
            fs.Write(uploadedFile.FileContent, 0,
    uploadedFile.FileContent.Length);
            fs.Close();
        }
    }
}