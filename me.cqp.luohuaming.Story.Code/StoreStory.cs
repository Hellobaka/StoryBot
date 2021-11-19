using me.cqp.luohuaming.Story.PublicInfos;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;

namespace me.cqp.luohuaming.Story.Code
{
    public class StoreStory
    {
        public class Node
        {
            public string nodeid { get; set; } = "";
            public string parentid { get; set; } = "";
            public List<Value> values { get; set; } = new List<Value>();
            public class Value
            {
                public string from { get; set; }
                public string value { get; set; }
            }
        }
        public class Story
        {
            public long Origin { get; set; } = 0;
            public bool IsGroup { get; set; } = false;
            public List<Node> nodes { get; set; } = new List<Node>();
            public string nid { get; set; } = "";
            public string branchid { get; set; } = "";
            public string mid { get; set; } = "";
            public string Text { get; set; } = "";

            private long timeout = 0;
            private void CallRemove()
            {
                MainSave.CQLog.Info("续写超时", "无动作十分钟，自动销毁");
                string msg = "续写已十分钟无响应，触发自动终止，现可重新开始一次续写";
                if (IsGroup)
                    MainSave.CQApi.SendGroupMessage(Origin, msg);
                else
                    MainSave.CQApi.SendPrivateMessage(Origin, msg);
                StoreInstance.Remove(Origin);
            }
            public void UpdateRemove()
            {
                timeout = 0;
            }
            public JArray GenNodes()
            {
                JArray json = new JArray
                {
                    new JObject
                    {
                        {"brother","0" },
                        {"nodeid", nodes[0].nodeid },
                        {"values", new JArray
                            {
                                new JObject{ { "from", "model" }, {"value", "" } },
                                new JObject{ { "from", "user" }, {"value", nodes[0].values[0].value } },
                                new JObject{ { "from", "model" }, {"value", "" } },
                            }
                        },
                    }
                };
                return json;
            }
            public Story()
            {
                Thread thread = new Thread(() =>
                {
                    MainSave.CQLog.Info("续写超时时钟", "生效");
                    while (true)
                    {
                        Thread.Sleep(500);
                        if (timeout > 1000 * 60 * 10)
                        {
                            CallRemove();
                            break;
                        }
                        timeout += 500;
                    }
                });
            }
        }
        public static Dictionary<long, Story> StoreInstance = new Dictionary<long, Story>();
    }
}
