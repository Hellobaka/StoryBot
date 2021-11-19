using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using me.cqp.luohuaming.Story.Code.OrderFunctions;
using me.cqp.luohuaming.Story.Sdk.Cqp.EventArgs;
using me.cqp.luohuaming.Story.Sdk.Cqp.Interface;
using me.cqp.luohuaming.Story.PublicInfos;
using System.Reflection;
using System.IO;

namespace me.cqp.luohuaming.Story.Code
{
    public class Event_StartUp : ICQStartup
    {
        public void CQStartup(object sender, CQStartupEventArgs e)
        {
            MainSave.AppDirectory = e.CQApi.AppDirectory;
            MainSave.CQApi = e.CQApi;
            MainSave.CQLog = e.CQLog;
            MainSave.ImageDirectory = CommonHelper.GetAppImageDirectory();

            if (string.IsNullOrWhiteSpace(MainSave.UID))
            { 
                e.CQLog.Warning("UID", "请按照文档指示将UID放入对应字条下，之后重载插件"); 
                return; 
            }
            var verify = WebAPI.VerifyUID(MainSave.UID);
            if (verify == null || string.IsNullOrWhiteSpace(verify.data.user.name))
            {
                e.CQLog.Warning("UID", "无效的UID，请重新填写");
                return;
            }
            e.CQLog.Info("UID", $"校验成功，昵称: {verify.data.user.nickname} 手机: {verify.data.user.phone}");

            if (string.IsNullOrWhiteSpace(MainSave.Mid))
            {
                WebAPI.GetModelList();
                e.CQLog.Info("模型列表", "默认拉取第一个");
            }
            foreach (var item in Assembly.GetAssembly(typeof(Event_GroupMessage)).GetTypes())
            {
                if (item.IsInterface)
                    continue;
                foreach (var instance in item.GetInterfaces())
                {
                    if (instance == typeof(IOrderModel))
                    {
                        IOrderModel obj = (IOrderModel)Activator.CreateInstance(item);
                        if (obj.ImplementFlag == false)
                            break;
                        MainSave.Instances.Add(obj);
                    }
                }
            }
        }
    }
}
