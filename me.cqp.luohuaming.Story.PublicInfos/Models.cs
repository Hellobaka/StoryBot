using System.Collections.Generic;
using System.Text;
using me.cqp.luohuaming.Story.Sdk.Cqp.EventArgs;

namespace me.cqp.luohuaming.Story.PublicInfos
{
    public interface IOrderModel
    {
        bool ImplementFlag { get; set; }
        string GetOrderStr();
        bool Judge(string destStr);
        FunctionResult Progress(CQGroupMessageEventArgs e);
        FunctionResult Progress(CQPrivateMessageEventArgs e);
    }
}
