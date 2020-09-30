using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule.Models
{
    public class EventInfo
    {
        public string id { get; set; }
        public string event_name { get; set; }
        public string geom { get; set; }
        public string up_time { get; set; }
        public string status { get; set; }
        public string event_details { get; set; }

        public string report_to_people { get; set; }
        public string department_id { get; set; }
        public string event_address { get; set; }
        public string imgs { get; set; }
        public string type { get; set; }
        public string licence_plate { get; set; }
        public string addtime { get; set; }
        public string user_id { get; set; }
        public string endimg { get; set; }
        public string enddetails { get; set; }
        public string d_id { get; set; }
        public string dp_id { get; set; }
        public string event_code { get; set; }
        public string event_pid { get; set; }
        public string dates { get; set; }
        public string unit_code { get; set; }
        public string expiration_time { get; set; }
        public string expiration_warning_time { get; set; }
        public string dispatchd_time { get; set; }
        public string review_time { get; set; }
        public string accept_time { get; set; }
        public string accept_expired_one_time { get; set; }
        public string accept_expired_two_time { get; set; }
        public string finish_time { get; set; }
        public string finish_review_time { get; set; }

    }
}
