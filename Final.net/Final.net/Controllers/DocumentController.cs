using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Final.net.Models;
using Final.net.RequestModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using New.RequestModel;

namespace Final.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DriveContext _cgcontext;
        private readonly IHostingEnvironment _environment;
        public DocumentController(DriveContext project, IHostingEnvironment environment)
        {
            _cgcontext = project;
            _environment = environment;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var getInfo = _cgcontext.Documents.ToList();
            return Ok(getInfo);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _cgcontext.Documents.Where(obj => obj.FolderId == id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public void Post([FromBody] DocumentRequest value)
        {
            Documents obj = new Documents();
            obj.DocumentName = value.DocumentName;
            obj.DocumentType = value.DocumentType;
            obj.Size = value.Size;
            obj.FolderId = value.FolderId;
            obj.CreatedAt = value.CreatedAt;
            obj.CreatedBy = value.CreatedBy;
            obj.IsDeleted = value.IsDeleted;
            _cgcontext.Documents.Add(obj);
            _cgcontext.SaveChanges();


        }
        [HttpGet("{userid:int}/{folderid}/{value}")]
        public IActionResult GetDetails(string value, int userid,int folderid)
        {

            var result = _cgcontext.Documents.Where(e=>e.CreatedBy==userid).Where(o => o.FolderId == folderid).Where(obj => obj.DocumentName.Contains(value));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var delete = _cgcontext.Documents.Where(res => res.DocumentId == id).ToList();
            delete.ForEach(res => _cgcontext.Documents.Remove(res));
            _cgcontext.SaveChanges();

        }
        [HttpPost("upload/")]
        public IActionResult Upload(List<IFormFile> files)

        {
            //GetFolder with userID = userModel.UserID;
            FoldersRequest model = new FoldersRequest();
            model.FolderId= 51;


            long fsize = files.Sum(f => f.Length);
            var RootPath = Path.Combine(_environment.ContentRootPath, "Resources", "Documents");

            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);
            foreach (var file in files)
            {
                var filePath = Path.Combine(RootPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    var Documents = new Documents()
                    {
                        DocumentName = file.FileName,
                        DocumentType = file.ContentType,
                        Size = (int)file.Length,
                        FolderId = model.FolderId

                    };
                    file.CopyTo(stream);
                    _cgcontext.Documents.Add(Documents);
                    _cgcontext.SaveChanges();
                }
            }
            return Ok(new { count = files.Count, fsize });
        }

    }
}
