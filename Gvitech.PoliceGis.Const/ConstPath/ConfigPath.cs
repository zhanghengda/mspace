using System;

namespace Mmc.Mspace.Const.ConstPath
{
    // Token: 0x02000002 RID: 2
    public class ConfigPath
    {
        // Token: 0x04000001 RID: 1
        public static readonly string ShellConfig = "Config\\ShellConfig.xml";

        // Token: 0x04000002 RID: 2
        public static readonly string ToolConfig = "Config\\ToolConfig.xml";

        // Token: 0x04000003 RID: 3
        public static readonly string DataSourceConfig = "Config\\DataSourceConfig.xml";

        public static readonly string ConfigDb = "conf.db";

        public static readonly string InspectionDb = "Inspection.db";

        public static readonly string WGS84_UTM_Path = @"data\prj\WGS84_UTM.json";

        public static string WorkSpaceConfig
        {
            get
            {
#if DEBUG
                return "Config\\WSConfig-dev.xml";
#else
                return "Config\\WSConfig.xml";
#endif
            }
        }
        //public static readonly string WorkSpaceConfig = "Config\\WSConfig.xml";

        // Token: 0x04000005 RID: 5
        public static readonly string FieldsFilterConfig = "Config\\FieldsFilterConfig.xml";

        // Token: 0x04000006 RID: 6
        public static readonly string LayersConfig = "Config\\LayersConfig.xml";

        // Token: 0x04000007 RID: 7
        public static readonly string LogConfig = "\\logs";

        public static string WebConnectConfig
        {
            get
            {
#if DEBUG
                return "Config\\WebConnectConfig-dev.json";
#else
                return "Config\\WebConnectConfig.json";
#endif
            }
        }

        public static string MappingCameraConfig
        {
            get
            {
                return "Config\\MappingCameraConfig.json";
            }
        }

        public static string JoyBaseConfig
        {
            get
            {
#if DEBUG
                return "Config\\joystick-dev.ini";
#else
                return "Config\\joystick.ini";
#endif
            }
        }

        //public static readonly string WebConnectConfig = "Config\\WebConnectConfig.json";
        /// <summary>
        /// 坐标文件路径
        /// </summary>
        public static readonly string CrsConfig = "Config\\crswkt.prj";

        public static string LogoConfig = "Config\\LogoConfig.json";
    }
}
