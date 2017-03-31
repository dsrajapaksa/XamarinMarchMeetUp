using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCupsApp.Model
{
    public class CupOfCoffee
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
        public DateTime DateUtc { get; set; }



        [Newtonsoft.Json.JsonIgnore]
        public String DateDisplay { get { return DateUtc.ToLocalTime().ToString("d"); } }

        [Newtonsoft.Json.JsonIgnore]
        public string TimeDisplay { get { return DateUtc.ToLocalTime().ToString("t"); } }
    }
}
