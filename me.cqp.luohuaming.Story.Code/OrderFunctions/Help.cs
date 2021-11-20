using me.cqp.luohuaming.Story.Sdk.Cqp.EventArgs;
using me.cqp.luohuaming.Story.PublicInfos;
using System.Threading;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;
using System.IO;
using me.cqp.luohuaming.Story.Sdk.Cqp;

namespace me.cqp.luohuaming.Story.Code.OrderFunctions
{
    public class Help : IOrderModel
    {
        public bool ImplementFlag { get; set; } = true;

        public string GetOrderStr() => "#续写帮助";

        public bool Judge(string destStr) => destStr.StartsWith(GetOrderStr());

        string help = "公用接口，请注意言论，避免高频率调用。在进行续写时请写有意义的句子，避免浪费彩云小梦官方的计算资源浪费。\n" +
    "#创建续写 新建一篇续写，一个来源只能同时拥有一篇续写，10分钟无操作自动销毁\n" +
    "#结束续写 结束续写，可重新开始一次\n" +
    "续写 [内容(首次必需)] 只有当前来源存在一篇续写时可以调用。第一次使用请加上一个开头，之后再调用可直接写‘续写’两个字";

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
            sendText.MsgToSend.Add(help);
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

            sendText.MsgToSend.Add(help);
            result.SendObject.Add(sendText);
            return result;
        }
    }
}
