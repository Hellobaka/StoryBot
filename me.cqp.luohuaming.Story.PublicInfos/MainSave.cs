using System.Collections.Generic;
using System.IO;
using me.cqp.luohuaming.Story.Sdk.Cqp;
using me.cqp.luohuaming.Story.Tool.IniConfig;

namespace me.cqp.luohuaming.Story.PublicInfos
{
    public static class MainSave
    {
        /// <summary>
        /// 保存各种事件的数组
        /// </summary>
        public static List<IOrderModel> Instances { get; set; } = new List<IOrderModel>();
        public static CQLog CQLog { get; set; }
        public static CQApi CQApi { get; set; }
        public static string AppDirectory { get; set; }
        public static string ImageDirectory { get; set; }
        public static string Mid
        {
            get
            {
                string r = ConfigMain.Object["Config"]["MID"]?.ToString();
                if (string.IsNullOrWhiteSpace(r))
                    return string.Empty;
                return r;
            }
            set { ConfigMain.Object["Config"]["MID"] = value; ConfigMain.Save(); }
        }
        public static string UID {
            get
            {
                string r = ConfigMain.Object["Config"]["UID"]?.ToString();
                if (string.IsNullOrWhiteSpace(r))
                    return string.Empty;
                return r;
            }
            set { ConfigMain.Object["Config"]["UID"] = value; ConfigMain.Save(); }
        }
        static IniConfig configMain;
        public static IniConfig ConfigMain
        {
            get
            {
                if (configMain != null)
                    return configMain;
                configMain = new IniConfig(Path.Combine(AppDirectory, "Config.ini"));
                configMain.Load();
                return configMain;
            }
            set { configMain = value; }
        }
    }
}
