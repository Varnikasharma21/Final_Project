using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using New.RequestModel;

namespace Final.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly DriveContext _cgcontext;
        public FolderController(DriveContext project)
        {
            _cgcontext = project;
        }

        // GET: api/PlayersInfo
        [HttpGet]
        public IActionResult Get()
        {
            var getInfo = _cgcontext.Folders.ToList();
            return Ok(getInfo);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _cgcontext.Folders.Where(obj => obj.CreatedBy == id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        // POST: api/PlayersInfo
        [HttpPost]
        public void Post([FromBody] FoldersRequest value)
        {
            Folders obj = new Folders();
            obj.FolderName = value.FolderName;
            obj.CreatedAt = value.CreatedAt;
            obj.CreatedBy = value.CreatedBy;
            obj.IsDeleted = value.IsDeleted;
            _cgcontext.Folders.Add(obj);
            _cgcontext.SaveChanges();


        }

        [HttpGet("{value},{id:int}")]
        public IActionResult GetDetails(string value, int id)
        {

            var result = _cgcontext.Folders.Where(o => o.CreatedBy == id).Where(obj => obj.FolderName.Contains(value));
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var delete = _cgcontext.Documents.Where(res => res.FolderId == id).ToList();
            delete.ForEach(res => _cgcontext.Documents.Remove(res));
            var del = _cgcontext.Folders.Where(res => res.FolderId == id).ToList();
            del.ForEach(res => _cgcontext.Folders.Remove(res));
            _cgcontext.SaveChanges();

        }
    }
}

