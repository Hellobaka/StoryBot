using me.cqp.luohuaming.Story.Sdk.Cqp.EventArgs;
using me.cqp.luohuaming.Story.PublicInfos;
using System.Threading;
using System.Linq;

namespace me.cqp.luohuaming.Story.Code.OrderFunctions
{
    public class DoStory : IOrderModel
    {
        public bool ImplementFlag { get; set; } = true;

        public string GetOrderStr() => "续写";

        public bool Judge(string destStr) => destStr.StartsWith(GetOrderStr());

        public string GetStoryString(long Origin, string content)
        {
            var story = StoreStory.StoreInstance[Origin];
            story.UpdateRemove();
            if (story.nid == "")
            {
                var newNovel = WebAPI.NewNovel(MainSave.UID, content, "");
                story.Text = content;
                story.nid = newNovel.data.nid;
                story.mid = MainSave.Mid;
                story.branchid = newNovel.data.novel.branchid;
                var newNode = new StoreStory.Node { nodeid = newNovel.data.firstnode.nodeid };
                newNode.values.Add(new StoreStory.Node.Value { from = "user", value = content });
                story.nodes.Add(newNode);
                Thread.Sleep(500);
                var newResult = WebAPI.NovelAI(MainSave.UID, story.branchid, story.nodes[0].nodeid, story.nid, story.mid, story.Text);
                var nextNewNode = newResult.data.nodes[0];
                story.Text += nextNewNode.content;
                story.nodes.Add(new StoreStory.Node { nodeid= nextNewNode.nodeid, parentid= nextNewNode.parentid });
                Thread.Sleep(500);
                WebAPI.AddNode(MainSave.UID, nextNewNode.nodeid, story.nid, story.Text, newResult.data.nodes.Select(x => x.nodeid).ToArray());
                WebAPI.DoNovelSave_Add(MainSave.UID, story.branchid, story.nid, newResult.data.nodes.Select(x => x.nodeid).ToArray(), story.GenNodes(), story.Text);
                return story.Text;
            }
            else
            {
                story.Text += content;
                var newResult = WebAPI.NovelAI(MainSave.UID, story.branchid, story.nodes[story.nodes.Count-1].nodeid, story.nid, story.mid, story.Text);
                var nextNewNode = newResult.data.nodes[0];
                story.Text += nextNewNode.content;
                story.nodes.Add(new StoreStory.Node { nodeid = nextNewNode.nodeid, parentid = nextNewNode.parentid });
                Thread.Sleep(500);
                WebAPI.AddNode(MainSave.UID, nextNewNode.nodeid, story.nid, story.Text, newResult.data.nodes.Select(x => x.nodeid).ToArray());
                WebAPI.DoNovelSave_Add(MainSave.UID, story.branchid, story.nid, newResult.data.nodes.Select(x => x.nodeid).ToArray(), story.GenNodes(), story.Text);
                return story.Text;
            }
        }

        public FunctionResult Progress(CQGroupMessageEventArgs e)//群聊处理
        {
            FunctionResult result = new FunctionResult
            {
                Result = true,
                SendFlag = true,
            };
            SendText sendText = new SendText
            {
                SendID = e.FromGroup,
            };
            if(StoreStory.StoreInstance.ContainsKey(e.FromGroup) is false)
            {
                result.Result = false;
                result.SendFlag = false;
                return result;
            }
            string content = e.Message.Text.Substring(GetOrderStr().Length).Trim();
            string msg;
            if(StoreStory.StoreInstance[e.FromGroup].nid == "" && content == "")
            {
                msg = "第一次调用请填写内容，之后可不添加内容直接调用续写";
            }
            else
            {
                msg = GetStoryString(e.FromGroup, content);
            }
            sendText.MsgToSend.Add(msg);
            result.SendObject.Add(sendText);
            return result;
        }

        public FunctionResult Progress(CQPrivateMessageEventArgs e)//私聊处理
        {
            FunctionResult result = new FunctionResult
            {
                Result = true,
                SendFlag = true,
            };
            SendText sendText = new SendText
            {
                SendID = e.FromQQ,
            };
            if (StoreStory.StoreInstance.ContainsKey(e.FromQQ) is false)
            {
                result.Result = false;
                result.SendFlag = false;
                return result;
            }
            string content = e.Message.Text.Substring(GetOrderStr().Length).Trim();
            string msg;
            if (StoreStory.StoreInstance[e.FromQQ].nid == "" && content == "")
            {
                msg = "第一次调用请填写内容，之后可不添加内容直接调用续写";
            }
            else
            {
                msg = GetStoryString(e.FromQQ, content);
            }
            sendText.MsgToSend.Add(msg);
            result.SendObject.Add(sendText);
            return result;
        }
    }
}
