using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebhookResolver
{
    public class Webhook
    {
        public int type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public object avatar { get; set; }
        public string channel_id { get; set; }
        public string guild_id { get; set; }
        public object application_id { get; set; }
        public string token { get; set; }
    }
}
