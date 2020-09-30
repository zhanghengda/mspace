/*
 string name = Enum.GetName(typeof(TypeEnum.GAS_NAME), i);
 int type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_SERVER_MESSAGE;

 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.UavModule.Dto
{
    class TypeEnum
    {
        public enum WEB_CONTROLLER
        {
            //Message Type
            CMD_TYPE_SERVER_MESSAGE = 200, //消息号
            CMD_TYPE_WEB2GROUND = 201, //Web与地面站通信
            CMD_TYPE_GROUND2WEB = 202, //地面站与web通信
            CMD_TYPE_CONNECT_GROUND_SERVER = 203, //地面站服务器建立连接
            CMD_TYPE_DISCONNECT_GROUND_SERVER = 204, //地面站服务器断开连接
            CMD_TYPE_CONNECT_SERVER_GROUND = 205, //服务器地面站建立连接
            CMD_TYPE_DISCONNECT_SERVER_GROUND = 206, //服务器地面站断开连接
            CMD_TYPE_SERVER_HEARTBEAT = 207, //服务器心跳包
            CMD_TYPE_SERVER_VEHICLESTATE = 212, //地面站发送无人机数据到服务器

            //Commond
            CMD_REQUEST_WEB_2_GROUDN_REQUEST = 1, //web请求地面站数据
            CMD_REQUEST_WEB_2_GROUDN_SEND = 2, //web发送地面站数据

            //PG
            CMD_FUNCTION_WEB_2_GROUND_PITCH = 10, //Pitch俯仰
            CMD_FUNCTION_WEB_2_GROUND_HEAD = 11, //Head偏航
            CMD_FUNCTION_WEB_2_GROUND_ZOOM = 12, //Zoom变焦
            CMD_FUNCTION_WEB_2_GROUND_LOCK = 13, //Lock锁定模式
            CMD_FUNCTION_WEB_2_GROUND_FREE = 14, //Free控制模式
            CMD_FUNCTION_WEB_2_GROUND_CENTER = 15, //Center回中模式
            CMD_FUNCTION_WEB_2_GROUND_TAKEPIC = 16, //TakePic拍照
            CMD_FUNCTION_WEB_2_GROUND_STARTREC = 17, //StartRec开始录制
            CMD_FUNCTION_WEB_2_GROUND_STOPREC = 18, //StopRec停止录制

            //PK
            CMD_FUNCTION_WEB_2_GROUND_VOLUMEPITCH = 20, //Pitch俯仰
            CMD_FUNCTION_WEB_2_GROUND_VOLUME = 21, //音量
            CMD_FUNCTION_WEB_2_GROUND_VOLUMEMODE = 22, //模式

            //PL
            CMD_FUNCTION_WEB_2_GROUND_LIGHTPITCH = 30, //Pitch俯仰
            CMD_FUNCTION_WEB_2_GROUND_LIGHTMODE = 31, //模式

            //PD
            CMD_FUNCTION_WEB_2_GROUND_DROPPITCH = 40, //Pitch抛投俯仰
            CMD_FUNCTION_WEB_2_GROUND_DROPMODE = 41, //Mode抛投模式
            CMD_FUNCTION_WEB_2_GROUND_DROPDUMP = 42, //Dump抛下按钮
            CMD_FUNCTION_WEB_2_GROUND_DROPWEIGHT = 43, //Weight抛投重量设置

            //PQ
            CMD_FUNCTION_WEB_2_GROUND_PEPITCH = 50, //Pitch俯仰

            //CMD_FUNCTION_WEB_2_GROUND_VEHICLEARM     = 100,//arm
            //CMD_FUNCTION_WEB_2_GROUND_VEHICLETAKEOFF = 101,  //take off
        }

        public enum CAMERA_MOUNT
        {
            CAM_10 = 267,//10倍云台
            CAM_SONGXIA20 = 283,//20倍松下云台
            CAM_SONY20 = 299,//20倍索尼云台
            CAM_QIWA30 = 315,//30倍奇蛙云台
            CAM_GOPRO5 = 331,//Gopro5云台
            CAM_BIFOCAL10 = 347,//10倍红外双光云台
            CAM_SMALLBIFOCAL = 363,//小双光
            CAM_LLL18 = 379,//18倍微光
            CAM_LLLFLOW35 = 395,//35倍微光带跟踪
            CAM_5100 = 411,//5100云台
            CAM_A7R = 427,//A7R云台
            CAM_Filr = 443,//Filr红外
            CAM_F1T2FOCAL = 475,//微光红外二合一短款
            CAM_PGIY1 = 491,//海视英科红外
            CAM_PG2IF2_LT2Z35 = 507,//微光红外二合一长款
        }

        public enum MOUNT_NAME_MAP
        {
            [Description("10倍云台")]
            CAM_10 = 267,
            [Description("20倍松下云台")]
            CAM_SONGXIA20 = 283,
            [Description("20倍索尼云台")]
            CAM_SONY20 = 299,
            [Description("30倍奇蛙云台")]
            CAM_QIWA30 = 315,
            [Description("Gopro5云台")]
            CAM_GOPRO5 = 331,
            [Description("10倍红外双光云台")]
            CAM_BIFOCAL10 = 347,
            [Description("小双光")]
            CAM_SMALLBIFOCAL = 363,
            [Description("18倍微光")]
            CAM_LLL18 = 379,
            [Description("35倍微光带跟踪")]
            CAM_LLLFLOW35 = 395,
            [Description("5100云台")]
            CAM_5100 = 411,
            [Description("A7R云台")]
            CAM_A7R = 427,
            [Description("Filr红外")]
            CAM_Filr = 443,
            [Description("微光红外二合一短款")]
            CAM_F1T2FOCAL = 475,
            [Description("海视英科红外")]
            CAM_PGIY1 = 491,
            [Description("微光红外二合一长款")]
            CAM_PG2IF2_LT2Z35 = 507,
            [Description("Dislink")]
            CAM_NONE_CAM = 9999,
        }

        public enum MOUNT_TYPE
        {
            MOUNT_SPEAKE = 266,//喊话器
            MOUNT_LIGHT = 282,//探照灯
            MOUNT_DROP = 298,//抛投
            MOUNT_GASDETECTION = 346,//毒气检测
            MOUNT_CAM_INFO = 0,
            MOUNT_HYDROGNE = 300,
            MOUNT_FOC = 316,
            MOUNT_3DMAPPING = 268,
            MOUNT_INDICATORLIGHT = 284
        }

        public enum GAS_NAME
        {
            EX = 1,
            CO,
            O2,
            H2,
            CH4,
            C3H8,
            CO2,
            O3,
            H2S,
            SO2,
            NH3,
            CL2,
            ETO,
            HCL,
            PH3,
            HBr,
            HCN,
            AsH3,
            HF,
            Br2,
            NO,
            NO2,
            NOX,
            CLO2,
            SiH4,
            CS2,
            F2,
            B2H6,
            GeH4,
            N2,
            THT,
            C2H2,
            C2H4,
            CH2O,
            LPG,
            HC,
            VOC,//id = 37
            H2O2,
            VOC2,//重复VOC，id = 39
            SF6,
            C7H8,
            C4H6,
            COS,
            N2H4,
            SeH2,
            C8H8,
            C4H8,
            CH2,
        }

        public enum GAS_UNIT_MAP
        {
            LEL,
            VOL,
            PPM,
            PPb,
            mg_m3
        }

        public readonly string[] GAS_UNIT_MAP_ARRY = { "%LEL", "%VOL", "PPM", "PPb", "mg/m3" };

        /// <summary>
        /// 获取枚举项描述信息 例如GetEnumDesc(Days.Sunday)
        /// </summary>
        /// <param name="en">枚举项 如Days.Sunday</param>
        /// <returns></returns>
        private static string GetEnumDesc(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }

        public static string getMountName(int mountype)
        {
            string mountName = "";
            switch (mountype)
            {
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_10:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_10);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_5100:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_5100);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_SONGXIA20:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_SONGXIA20);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_SONY20:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_SONY20);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_QIWA30:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_QIWA30);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_GOPRO5:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_GOPRO5);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_BIFOCAL10:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_BIFOCAL10);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_SMALLBIFOCAL:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_SMALLBIFOCAL);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_LLL18:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_LLL18);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_LLLFLOW35:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_LLLFLOW35);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_Filr:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_Filr);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_F1T2FOCAL:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_F1T2FOCAL);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_PGIY1:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_PGIY1);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_PG2IF2_LT2Z35:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_PG2IF2_LT2Z35);
                    break;
                case (int)TypeEnum.MOUNT_NAME_MAP.CAM_NONE_CAM:
                    mountName = GetEnumDesc(TypeEnum.MOUNT_NAME_MAP.CAM_NONE_CAM);
                    break;
                default:
                    mountName = "None·";
                    break;
            }
            return mountName;
        }
    }
}
