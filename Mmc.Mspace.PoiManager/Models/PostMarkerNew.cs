﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.Mspace.PoiManagerModule.Dto;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class PostMarkerNew
    {
        /*
         * {
  "status": 1,
  "message": "ok",
  "data": {
    "geom": "POLYGON Z ((113.995309121529 22.7054270502508 0.137482631020248,113.998482423707 22.7036631266178 0.093123439699411,113.998833491843 22.7043666863431 0.096002439036965,113.999320669907 22.7030940845684 0.08461631834507,113.999922695503 22.7022703539708 0.077820172533393,114.000266784772 22.7019426236052 0.075245871208608,114.001884732741 22.7014081366199 0.069831419736147,114.00179253029 22.7042276571461 0.083971693180501,114.003651925202 22.7021864268472 0.072802881710231,114.005807199173 22.6996740139572 0.077285534702241,114.007436681226 22.6988365735589 0.089889232069254,114.005195791576 22.7025416168193 0.079043961130083,114.004077596549 22.703845807154 0.082626449875534,114.003538266082 22.7059465251262 0.100440689362586,114.00532017752 22.7070311058149 0.119114068336785,114.009865457812 22.7077701875284 0.167280565015972,114.013493286664 22.7075894636039 0.219443227164447,114.015428114557 22.708088221111 0.264898268505931,114.018241091438 22.7066654900828 0.311662694439292,114.019069998348 22.7060658202834 0.326621199958026,114.01982486951 22.7053947556843 0.340577667579055,114.020119054956 22.7033018396139 0.332417734898627,114.020166433549 22.7010722270691 0.325436878018081,114.020227105263 22.699637564763 0.326935491524637,114.019944391047 22.6975192505948 0.325647142715752,114.019629251433 22.6966548092167 0.321988799609244,114.019013214875 22.6960485456399 0.309503613039851,114.018267848534 22.6950292090232 0.299014402553439,114.017780470817 22.6941671557954 0.296020346693695,114.017097549783 22.6927534046099 0.297874125652015,114.016533201706 22.6920157574154 0.29584271274507,114.014931501945 22.691414189162 0.270907898433506,114.012485475666 22.6911183702561 0.23093565646559,114.010819162252 22.6909891766796 0.208160908892751,114.009020942327 22.6912551208911 0.181521812453866,114.008678133561 22.6928127418761 0.153066140599549,114.009502504035 22.6936322878648 0.150783356279135,114.007897316062 22.6939294835063 0.130716919898987,114.006020509098 22.6940322859936 0.115776646882296,114.00433672504 22.6954354851611 0.093398581258953,114.003991219428 22.6965762143236 0.083055261522532,114.003589607959 22.6971600898003 0.078387077897787,114.002679487837 22.6980643840954 0.072859953157604,114.002053680895 22.6987383189968 0.070619581267238,114.001645421328 22.6991889595524 0.069959782063961,114.000576693089 22.6996129289522 0.071795938536525,113.999021192622 22.7001278160571 0.078482697717845,113.997725900388 22.7013618841608 0.088596392422915,113.996371398107 22.7028992896581 0.10651308298111,113.995515458077 22.7034531330154 0.119003470055759,113.995204632583 22.7037492066338 0.124622103758156,113.995309121529 22.7054270502508 0.137482631020248))",
    "code": "BZ2019522151542",
    "img": "/cc/img.png",
    "address": "中国上海",
    "type": 3,
    "cat_id": 1,
    "title": "标题",
    "style": "css样式",
    "lp_size": 12,
    "phone": "13111112222",
    "operator": "处理人名称",
    "detail": "标注详情",
    "level": 1,
    "status": 0,
    "user_id": 1,
    "addtime": "2019-04-23 11:22:57",
    "letter": "B",
    "marker_id": 1002175
  }
}
         */

        public int marker_id { get; set; }
        public string geom { get; set; }
        public string code { get; set; }
        public string img { get; set; }
        public string address { get; set; }
        public int type { get; set; }
        public int cat_id { get; set; }
        public string title { get; set; }
        public string style { get; set; }
        public string lp_size { get; set; }
        public string phone { get; set; }
        public string @operator { get; set; }
        public string detail { get; set; }
        public int level { get; set; }
        public int status { get; set; }
        public string user_id { get; set; }
        //public string addtime { get; set; }
        public string letter { get; set; }
        public List<TagItem> tags { get; set; }
    }
}