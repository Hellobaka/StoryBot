using me.cqp.luohuaming.Story.Tool.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace me.cqp.luohuaming.Story.PublicInfos
{
    public static class WebAPI
    {
        public static HttpWebClient GetHttp()
        {
            HttpWebClient client = new HttpWebClient
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36 Edg/95.0.1020.53"
            };
            return client;
        }
        public static UserInfo VerifyUID(string UID)
        {
            string baseURL = $"http://if.caiyunai.com/v2/user/{UID}/info";
            using (var http = GetHttp())
            {
                var result = http.UploadString(baseURL, "{ostype: \"\", lang: \"zh\"}");
                if(VerifyResult(result))
                {
                    return JsonConvert.DeserializeObject<UserInfo>(result);
                }
                else
                {
                    MainSave.CQLog.Info("调用失败", result);
                    return null;
                }
            }
        }
        public static NovelSave_New NewNovel(string UID, string content, string title)
        {
            string baseURL = $"http://if.caiyunai.com/v2/novel/{UID}/novel_save";
            using (var http = GetHttp())
            {
                var result = http.UploadString(baseURL, $"{{title: \"{title}\", nodes: [], text: \"{content}\", ostype: \"\", lang: \"zh\"}}");
                if (VerifyResult(result))
                {
                    return JsonConvert.DeserializeObject<NovelSave_New>(result);
                }
                else
                {
                    MainSave.CQLog.Info("调用失败", result);
                    return null;
                }
            }
        }
        public static NovelAI NovelAI(string UID, string branchID, string lastnode, string nid, string mid, string content)
        {
            string baseURL = $"http://if.caiyunai.com/v2/novel/{UID}/novel_ai";
            using (var http = GetHttp())
            {
                JObject json = new JObject
                {
                    {"branchid", branchID },
                    {"content", content },
                    {"lang","zh" },
                    {"lastnode", lastnode },
                    {"mid", mid },
                    {"nid", nid },
                    {"ostype","" },
                    {"status","http" },
                    {"storyline", false },
                    {"title","" },
                    {"uid", UID },
                };
                var result = http.UploadString(baseURL, json.ToString());
                if (VerifyResult(result))
                {
                    return JsonConvert.DeserializeObject<NovelAI>(result);
                }
                else
                {
                    MainSave.CQLog.Info("调用失败", result);
                    return null;
                }
            }
        }
        public static bool AddNode(string UID, string choose, string nid, string value, string[] nodeids)
        {
            string baseURL = $"http://if.caiyunai.com/v2/novel/{UID}/add_node";
            using (var http = GetHttp())
            {
                JObject json = new JObject
                {
                    {"choose", choose },
                    {"lang", "zh" },
                    {"nodeids", JsonConvert.SerializeObject(nodeids) },
                    {"ostype", "" },
                    {"value", value },
                };
                var result = http.UploadString(baseURL, json.ToString());
                if (VerifyResult(result))
                {
                    return true;
                }
                else
                {
                    MainSave.CQLog.Info("调用失败", result);
                    return false;
                }
            }
        }
        public static NovelSave_Add DoNovelSave_Add(string UID, string branchid, string nid, string[] nodeids, object nodes, string text)
        {
            string baseURL = $"http://if.caiyunai.com/v2/novel/{UID}/novel_save";
            using(var http = GetHttp())
            {
                var json = new JObject
                {
                    {"branchid", branchid },
                    {"lang", "zh" },
                    {"nid", nid },
                    {"nodeids", JsonConvert.SerializeObject(nodeids) },
                    {"nodes", JsonConvert.SerializeObject(nodes) },
                    {"ostype", "" },
                    {"text", text },
                    {"title", "" },
                };
                var result = http.UploadString(baseURL, json.ToString());
                if (VerifyResult(result))
                {
                    return JsonConvert.DeserializeObject<NovelSave_Add>(result);
                }
                else
                {
                    MainSave.CQLog.Info("调用失败", result);
                    return null;
                }
            }
        }
        public static bool VerifyResult(string result) => result.Contains("msg: \"ok\"") && result.Contains("status: 0");
    }
}
