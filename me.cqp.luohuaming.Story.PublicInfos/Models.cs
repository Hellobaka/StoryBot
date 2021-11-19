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
    public class NovelSave_Add
    {
        public class Index
        {
            public int status { get; set; }
            public string msg { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string nid { get; set; }
            public Branch branch { get; set; }
            public Novel novel { get; set; }
            public object[] error { get; set; }
        }

        public class Branch
        {
            public string _id { get; set; }
            public int created_at { get; set; }
            public string updated_text { get; set; }
            public string nid { get; set; }
            public string[] nodes { get; set; }
            public string start_node { get; set; }
            public object source_id { get; set; }
        }

        public class Novel
        {
            public string _id { get; set; }
            public string nid { get; set; }
            public int created_at { get; set; }
            public int updated_text { get; set; }
            public string uid { get; set; }
            public string title { get; set; }
            public string mid { get; set; }
            public object status { get; set; }
            public int size { get; set; }
            public object hidden { get; set; }
            public object audit_time { get; set; }
            public int audit_status { get; set; }
            public string _audit_status { get; set; }
            public bool is_updated { get; set; }
            public string branchid { get; set; }
            public string lastnode { get; set; }
        }

    }
    public class NovelSave_New
    {
        public class Index
        {
            public int status { get; set; }
            public string msg { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string nid { get; set; }
            public Firstnode firstnode { get; set; }
            public Novel novel { get; set; }
        }
        public class Firstnode
        {
            public string _id { get; set; }
            public int created_at { get; set; }
            public string updated_text { get; set; }
            public string nid { get; set; }
            public Value[] values { get; set; }
            public object[] children { get; set; }
            public string nodeid { get; set; }
            public string parentid { get; set; }
            public string content { get; set; }
            public bool isedit { get; set; }
            public bool hidden { get; set; }
            public object[] brother { get; set; }
        }
        public class Value
        {
            public string from { get; set; }
            public string value { get; set; }
        }
        public class Novel
        {
            public string _id { get; set; }
            public string nid { get; set; }
            public int created_at { get; set; }
            public int updated_text { get; set; }
            public string uid { get; set; }
            public string title { get; set; }
            public string mid { get; set; }
            public object status { get; set; }
            public object size { get; set; }
            public object hidden { get; set; }
            public object audit_time { get; set; }
            public int audit_status { get; set; }
            public string _audit_status { get; set; }
            public bool is_updated { get; set; }
            public string branchid { get; set; }
            public string lastnode { get; set; }
            public string firstnode { get; set; }
            public Node[] nodes { get; set; }
            public string[] nodeids { get; set; }
        }
        public class Node
        {
            public string _id { get; set; }
            public int created_at { get; set; }
            public string updated_text { get; set; }
            public string nid { get; set; }
            public Detail[] values { get; set; }
            public object[] children { get; set; }
            public string nodeid { get; set; }
            public string parentid { get; set; }
            public string content { get; set; }
            public bool isedit { get; set; }
            public bool hidden { get; set; }
            public object[] brother { get; set; }
        }
        public class Detail
        {
            public string from { get; set; }
            public string value { get; set; }
        }
    }
    public class ModelList
    {
        public class Index
        {
            public int status { get; set; }
            public string msg { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public Model[] models { get; set; }
        }

        public class Model
        {
            public string _id { get; set; }
            public string uid { get; set; }
            public bool _public { get; set; }
            public string mid { get; set; }
            public int status { get; set; }
            public string name { get; set; }
            public string _type { get; set; }
            public string _status { get; set; }
            public string dreamid { get; set; }
            public string abroad_dreamid { get; set; }
            public float temperature { get; set; }
            public string description { get; set; }
            public string[] null_data { get; set; }
            public string pathname { get; set; }
            public string type { get; set; }
        }

    }
    public class NovelAI
    {
        public class Index
        {
            public int status { get; set; }
            public string msg { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public Node[] nodes { get; set; }
            public int count { get; set; }
            public string cur_nodeid { get; set; }
        }

        public class Node
        {
            public string _id { get; set; }
            public int created_at { get; set; }
            public string updated_text { get; set; }
            public string nid { get; set; }
            public Detail[] values { get; set; }
            public object[] children { get; set; }
            public string nodeid { get; set; }
            public string parentid { get; set; }
            public string content { get; set; }
            public bool isedit { get; set; }
            public bool hidden { get; set; }
            public string xid { get; set; }
            public string[] brother { get; set; }
        }

        public class Detail
        {
            public string from { get; set; }
            public string value { get; set; }
        }

    }
    public class UserInfo
    {
        public class Index
        {
            public int status { get; set; }
            public string msg { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public User user { get; set; }
        }

        public class User
        {
            public string _id { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public bool visible { get; set; }
            public object config { get; set; }
            public bool dream { get; set; }
            public string nickname { get; set; }
            public string headimgurl { get; set; }
            public object shut { get; set; }
            public object shut_type { get; set; }
            public int shut_time { get; set; }
            public int model_length { get; set; }
            public bool model_type { get; set; }
            public int shut_end_time { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string is_login { get; set; }
            public bool isLogin { get; set; }
        }

    }
}
