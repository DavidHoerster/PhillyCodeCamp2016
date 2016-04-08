using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Shared;
using UI_Api.Data;
using MongoDB.Driver;
using System.Net;

namespace UI_Api.Controllers
{
    public class PlayerController : ApiController
    {

        public HttpResponseMessage Get(String id)
        {
            var coll = MongoConnection.Database.GetCollection<Player>("players");
            var player = coll.AsQueryable().FirstOrDefault(p => p.Id.Equals(id));
            var stats = MongoConnection.Database.GetCollection<Batter>("batter");
            var batter = stats.AsQueryable().FirstOrDefault(b => b.Id.Equals(id));

            var resp = new { name = player, stats = batter };
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
    }
}
