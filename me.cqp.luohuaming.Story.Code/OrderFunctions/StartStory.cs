using me.cqp.luohuaming.Story.Sdk.Cqp.EventArgs;
using me.cqp.luohuaming.Story.PublicInfos;

namespace me.cqp.luohuaming.Story.Code.OrderFunctions
{
    public class StartStory : IOrderModel
    {
        public bool ImplementFlag { get; set; } = true;
        
        public string GetOrderStr() => "#创建续写";

        public bool Judge(string destStr) => destStr.StartsWith(GetOrderStr());

        public (bool, string) CreateStory(long Origin, bool group)
        {
            if (StoreStory.StoreInstance.ContainsKey(Origin))
            {
                return (false, "当前来源已创建了一个续写，请继续或终止");
            }
            else
            {
                StoreStory.StoreInstance.Add(Origin, new StoreStory.Story { Origin = Origin, IsGroup = group });
                return (true, "创建成功，请使用 续写 命令开始");
            }    
        }

        public FunctionResult Progress(CQGroupMessageEventArgs e)
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
            sendText.MsgToSend.Add(CreateStory(e.FromGroup, true).Item2);
            result.SendObject.Add(sendText);
            return result;
        }

        public FunctionResult Progress(CQPrivateMessageEventArgs e)
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

            sendText.MsgToSend.Add(CreateStory(e.FromQQ, false).Item2);
            result.SendObject.Add(sendText);
            return result;
        }
    }
}
