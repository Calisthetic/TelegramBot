using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Config
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string token { get; set; }
    }
}
