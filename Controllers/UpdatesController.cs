using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DXAUpdater.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            List<DXANET.DataElement> elements = new List<DXANET.DataElement>();
            DXANET.DataElement xx1 = new DXANET.DataElement(){
                name = "Higher Education Provider code",
                domain = "Education",
                status = "Standard",
                definition = "A code which uniquely identifies the Higher Education Provider",
                guidance = "Field Name: INSTITUTION",
                identifier = "http://dxa.gov.au/definition/edu/edu306",
                usage = new List<string>{"See source for more information"},
                datatype = new DXANET.Datatype(){facets = null, type = "Numeric"},
                values = new List<string>{},
                sourceURL = "http://heimshelp.education.gov.au/sites/heimshelp/2018_data_requirements/2018dataelements/pages/306"};
            DXANET.DataElement xx2 = new DXANET.DataElement(){
                name = "Course code",
                domain = "Education",
                status = "Standard",
                definition = "A code which uniquely identifies each course within a Higher Education/VET Provider (Provider).",
                guidance = "Field Name: INSTITUTION",
                identifier = "http://dxa.gov.au/definition/edu/edu307",
                usage = new List<string>{"See source for more information"},
                datatype = new DXANET.Datatype(){facets = null, type = "Alphanumeric"},
                values = new List<string>{},
                sourceURL = "http://heimshelp.education.gov.au/sites/heimshelp/2018_data_requirements/2018dataelements/pages/307"};

            DXANET.DataElement xx3 = new DXANET.DataElement(){
                name = "Course name - full",
                domain = "Education",
                status = "Standard",
                definition = "The full name of the course",
                guidance = "Field Name: COURSE-NAME",
                identifier = "http://dxa.gov.au/definition/edu/edu308",
                usage = new List<string>{"See source for more information"},
                datatype = new DXANET.Datatype(){facets = null, type = "Alphanumeric"},
                values = new List<string>{},
                sourceURL = "http://heimshelp.education.gov.au/sites/heimshelp/2018_data_requirements/2018dataelements/pages/308"};

            DXANET.DataElement xx4 = new DXANET.DataElement(){
                name = "Course of study type code",
                domain = "Education",
                status = "Standard",
                definition = "A code which indicates the type of higher education/VET course",
                guidance = "Field Name: COURSE-TYPE",
                identifier = "http://dxa.gov.au/definition/edu/edu310",
                usage = new List<string>{"See source for more information"},
                datatype = new DXANET.Datatype(){facets = null, type = "Numeric"},
                values = new List<string>{},
                sourceURL = "http://heimshelp.education.gov.au/sites/heimshelp/2018_data_requirements/2018dataelements/pages/310"};

            DXANET.DataElement xx5 = new DXANET.DataElement(){
                name = "Special course type code",
                domain = "Education",
                status = "Standard",
                definition = "A code which identifies courses of special interest to the department",
                guidance = "Field Name: SPECIAL-COURSE",
                identifier = "http://dxa.gov.au/definition/edu/edu312",
                usage = new List<string>{"See source for more information"},
                datatype = new DXANET.Datatype(){facets = null, type = "Numeric"},
                values = new List<string>{},
                sourceURL = "http://heimshelp.education.gov.au/sites/heimshelp/2018_data_requirements/2018dataelements/pages/312"};

            UpdatedData d1 = new UpdatedData(){ DataID = Guid.NewGuid().ToString(), UpdateDescription = "updated for blah reason", UpdatedDomain="edu", UpdatedIdentifiers = new List<string>{xx1.identifier} ,Payload = JsonConvert.SerializeObject(xx1), PayloadType = "DATA"};
            UpdatedData d2 = new UpdatedData(){ DataID = Guid.NewGuid().ToString(), UpdateDescription = "updated for some other reason", UpdatedDomain="edu", UpdatedIdentifiers = new List<string>{xx2.identifier, xx3.identifier} ,Payload = JsonConvert.SerializeObject(new List<DXANET.DataElement>{xx2, xx3}), PayloadType = "DATA"};
            UpdatedData d3 = new UpdatedData(){ DataID = Guid.NewGuid().ToString(), UpdateDescription = "best update in the world", UpdatedDomain="edu", UpdatedIdentifiers = new List<string>{xx3.identifier, xx4.identifier, xx5.identifier} ,Payload = JsonConvert.SerializeObject(new List<DXANET.DataElement>{xx3, xx4, xx5}), PayloadType = "DATA"};
            d1.NextDataID = d2.DataID;
            d2.NextDataID = d3.DataID;

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
