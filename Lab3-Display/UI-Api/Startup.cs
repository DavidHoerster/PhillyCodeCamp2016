using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;
using RethinkDb.Driver;
using RethinkDb.Driver.Ast;
using RethinkDb.Driver.Model;
using Shared;
using UI_Api.Data;
using UI_Api.Hubs;

namespace UI_Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var mongo = new MongoConnection();
            var rethink = new RethinkConnection();

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();

            app.UseWebApi(config);

            ReqlExpr dbTable = RethinkDB.R.Db("baseball").Table("batterStat");

            var feed = dbTable.Changes()
                        .RunChanges<Batter>(RethinkConnection.Connection);

            var observe = feed.ToObservable();
            observe.SubscribeOn(NewThreadScheduler.Default)
                .Subscribe((Change<Batter> b) =>
                {
                    BatterInstance.Instance.UpdateBatterStat(b.NewValue);
                });
        }
    }
}
