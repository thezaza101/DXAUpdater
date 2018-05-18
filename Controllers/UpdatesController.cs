using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DXAUpdater.Models;
using Microsoft.EntityFrameworkCore;

namespace DXAUpdater.Controllers
{

    [Route("api/[controller]")]
    public class UpdatesController : Controller
    {
        private readonly UpdatedDataContext _context;

        public UpdatesController(UpdatedDataContext context)
        {
            _context = context;    
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            UpdatedData d1 = new UpdatedData(){ DataID = Guid.NewGuid().ToString(), Payload = new List<DXANET.DataElement>()};
            UpdatedData d2 = new UpdatedData(){ DataID = Guid.NewGuid().ToString(), Payload = new List<DXANET.DataElement>()};
            UpdatedData d3 = new UpdatedData(){ DataID = Guid.NewGuid().ToString(), Payload = new List<DXANET.DataElement>()};
            d1.NextDataID = d2.DataID;
            d2.NextDataID = d3.DataID;
            DXANET.DataElement de1 = new DXANET.DataElement(){ name = "xx 1"};
            DXANET.DataElement de2 = new DXANET.DataElement(){ name = "xx 2"};
            DXANET.DataElement de3 = new DXANET.DataElement(){ name = "xx 3"};
            DXANET.DataElement de3x = new DXANET.DataElement(){ name = "xx 3.1"};
            d1.Payload.Add(de1);
            d2.Payload.Add(de2);
            d3.Payload.Add(de3);
            d3.Payload.Add(de3x);
            _context.Add(d1);
            _context.Add(d2);
            _context.Add(d3);
            _context.SaveChanges();
            return new string[] { d1.DataID, d2.DataID, d3.DataID };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public UpdatedData Get(string id)
        {            
            if (id == null)
            {
                return null;
            }            
            var updateddata = _context.UpdatedData.Include(d => d.Payload).SingleOrDefault(u => u.DataID.Equals(id));
            if (updateddata==null)
            {
                return null;
            }
                      
            return updateddata;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
