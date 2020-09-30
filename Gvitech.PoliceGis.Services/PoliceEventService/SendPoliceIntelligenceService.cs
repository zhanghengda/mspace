using Mmc.Mspace.Services.SocketService;
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Mmc.Mspace.Services.PoliceEventService
{
    public class SendPoliceIntelligenceService : MessageSendService
    {
        public SendPoliceIntelligenceService(string host, int port, ProtocolType pt) : base(host, port, pt)
        {
        }

        protected override object CreateMessage()
        {
            bool flag = SendPoliceIntelligenceService.pm == null;
            if (flag)
            {
                SendPoliceIntelligenceService.pm = new PoliceIntelligenceModel();
            }
            SendPoliceIntelligenceService.pm.CaseId = this.rdm.Next(1, 2000).ToString().PadLeft(4, '0');
            SendPoliceIntelligenceService.pm.CaseCotent = this.GenericCaseContent(this.rdm);
            SendPoliceIntelligenceService.pm.Informant = this.GenericInformant(this.rdm);
            SendPoliceIntelligenceService.pm.ReportingTime = DateTime.Now.ToLongTimeString();
            SendPoliceIntelligenceService.pm.CrimePlace = this.GenericCrimePlace(this.rdm);
            SendPoliceIntelligenceService.pm.Longitude = this.GenericLogitude(this.rdm);
            SendPoliceIntelligenceService.pm.Latitude = this.GenericLatitude(this.rdm);
            SendPoliceIntelligenceService.pm.CaseType = (CaseType)this.rdm.Next(0, 4);
            return SendPoliceIntelligenceService.pm;
        }

        private double GenericLogitude(Random rdm)
        {
            return (double)rdm.Next(106000, 106600) / 1000.0;
        }

        private double GenericLatitude(Random rdm)
        {
            return (double)rdm.Next(38000, 38600) / 1000.0;
        }

        private string GenericContent(int count)
        {
            bool flag = count < 1;
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                byte[] array = new byte[count * 2];
                int num;
                for (int i = 0; i < count; i = num + 1)
                {
                    Point gb2312Char = this.GetGB2312Char(this.rdm);
                    array[i * 2] = (byte)(gb2312Char.X + 160.0);
                    array[i * 2 + 1] = (byte)(gb2312Char.Y + 160.0);
                    num = i;
                }
                result = Encoding.GetEncoding("GB2312").GetString(array);
            }
            return result;
        }

        private Point GetGB2312Char(Random rdm)
        {
            int num = rdm.Next(40) + 16;
            int num2 = rdm.Next((num == 55) ? 89 : 94) + 1;
            return new Point((double)num, (double)num2);
        }

        private string GenericCaseContent(Random rdm)
        {
            return SendPoliceIntelligenceService.CaseContents[rdm.Next(SendPoliceIntelligenceService.CaseContents.Length)];
        }

        private string GenericInformant(Random rdm)
        {
            return SendPoliceIntelligenceService.Informant[rdm.Next(SendPoliceIntelligenceService.Informant.Length)];
        }

        private string GenericCrimePlace(Random rdm)
        {
            return SendPoliceIntelligenceService.CrimePlace[rdm.Next(SendPoliceIntelligenceService.CrimePlace.Length)];
        }

        private static PoliceIntelligenceModel pm;

        private static readonly string[] CaseContents = new string[]
        {
            "发生抢劫案，嫌疑人逃跑中。",
            "打架斗殴，场面血腥。",
            "入室盗窃，损失惨重。",
            "交通事故，伤亡严重。",
            "商场着火，火势严峻。",
            "发生踩踏，秩序混乱",
            "广场儿童走失，失联两天"
        };

        private static readonly string[] Informant = new string[]
        {
            "张山",
            "李思",
            "王武",
            "钱柳",
            "孙琦",
            "刘霸",
            "赵旧",
            "朝阳群众",
            "西城大妈",
            "海淀网友"
        };

        private static readonly string[] CrimePlace = new string[]
        {
            "水立方",
            "鸟巢",
            "北京西站",
            "北京站",
            "北京南站",
            "五道口",
            "天安门",
            "CBD",
            "798",
            "后海",
            "三里屯"
        };

        private readonly Random rdm = new Random();
    }
}