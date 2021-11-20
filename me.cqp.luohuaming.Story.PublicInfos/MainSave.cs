using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static string Font {
            get
            {
                string r = ConfigMain.Object["Config"]["Font"]?.ToString();
                if (string.IsNullOrWhiteSpace(r))
                    return "微软雅黑";
                return r;
            }
            set { ConfigMain.Object["Config"]["Font"] = value; ConfigMain.Save(); }
        }
        public static int PicWidth {
            get
            {
                string r = ConfigMain.Object["Config"]["PicWidth"]?.ToString();
                if (string.IsNullOrWhiteSpace(r))
                    return 900;
                return Convert.ToInt32(r);
            }
            set { ConfigMain.Object["Config"]["PicWidth"] = value; ConfigMain.Save(); }
        }
        public static string ThinkText {
            get
            {
                string r = ConfigMain.Object["Config"]["ThinkText"]?.ToString();
                if (string.IsNullOrWhiteSpace(r))
                    return "emmmm|让我想想...|我试试能写出点啥...|难内...";
                return r;
            }
            set { ConfigMain.Object["Config"]["ThinkText"] = value; ConfigMain.Save(); }
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
