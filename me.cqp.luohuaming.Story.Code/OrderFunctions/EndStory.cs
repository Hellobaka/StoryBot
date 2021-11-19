using me.cqp.luohuaming.Story.Sdk.Cqp.EventArgs;
using me.cqp.luohuaming.Story.PublicInfos;

namespace me.cqp.luohuaming.Story.Code.OrderFunctions
{
    public class EndStory : IOrderModel
    {
        public bool ImplementFlag { get; set; } = true;
        
        public string GetOrderStr() => "#结束续写";

        public bool Judge(string destStr) => destStr.StartsWith(GetOrderStr());

        public (bool, string) RemoveStory(long Origin)
        {
            if (StoreStory.StoreInstance.ContainsKey(Origin))
            {
                StoreStory.StoreInstance.Remove(Origin);
                return (true, "结束成功，现可重新开始一次续写");
            }
            else
            {
                return (false, "当前来源不存在正在进行的续写");
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

            sendText.MsgToSend.Add(RemoveStory(e.FromGroup).Item2);
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

            sendText.MsgToSend.Add(RemoveStory(e.FromQQ).Item2);
            result.SendObject.Add(sendText);
            return result;
        }
    }
}
