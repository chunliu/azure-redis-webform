using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace azure_redis_webform
{
    public partial class Default : System.Web.UI.Page
    {
        private Lazy<ConnectionMultiplexer> lazyConnection = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["redis"] == null)
                Session["redis"] = DateTime.Now.ToString();

            if (lazyConnection == null)
            {
                lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                {
                    var hostname = ConfigurationManager.AppSettings["hostname"];
                    var accesskey = ConfigurationManager.AppSettings["accesskey"];
                    var useSsl = bool.Parse(ConfigurationManager.AppSettings["sslkey"]);

                    var options = new ConfigurationOptions();
                    options.EndPoints.Add(hostname);
                    options.Password = accesskey;
                    options.Ssl = useSsl;
                    options.AbortOnConnectFail = false;

                    return ConnectionMultiplexer.Connect(options);
                });
            }

            if (!int.TryParse(ConfigurationManager.AppSettings["databaseid"], out int databaseid))
            {
                databaseid = 0;
            }

            IDatabase cache = lazyConnection.Value.GetDatabase(databaseid);
            var reqHost = cache.StringGet("WebHostName");
            if (string.IsNullOrEmpty(reqHost))
            {
                reqHost = HttpContext.Current.Request.Url.Host;
                cache.StringSet("WebHostName", reqHost);
            }

            this.keyLabel.Text = "WebHostName";
            this.valueLabel.Text = reqHost;
        }
    }
}