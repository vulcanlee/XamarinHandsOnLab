using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace XamarinHandsOnLabService.Controllers
{
    [MobileAppController]
    public class MyTaskController : ApiController
    {
        // GET api/<controller>
        public async Task<string> Get()
        {
            var fooBegin = DateTime.UtcNow.ToString();
            await Task.Delay(5000);
            var fooComplete = DateTime.UtcNow.ToString();
            return $"OK...{fooBegin} -> {fooComplete}";
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}