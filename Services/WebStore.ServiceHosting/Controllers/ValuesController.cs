using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> _values = Enumerable.Range(1, 10).Select(x => $"Value_{x:00}").ToList();

        // GET: api/<ValuesController>
        [HttpGet]
        public ICollection<string> Get()
        {
            return _values;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _values.Count())
                return NotFound();

            return _values.ElementAt(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            _values.Add(value);

            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _values.Count)
                return NotFound();

            _values.RemoveAt(id);
            _values.Insert(id, value);

            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _values.Count)
                return NotFound();

            _values.RemoveAt(id);

            return Ok();
        }
    }
}
