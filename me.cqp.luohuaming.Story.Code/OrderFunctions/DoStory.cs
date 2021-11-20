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
    public class DoStory : IOrderModel
    {
        public bool ImplementFlag { get; set; } = true;

        public string GetOrderStr() => "续写";

        public bool Judge(string destStr) => destStr.StartsWith(GetOrderStr());

        private object StoryLock = new object();
        public (string, string) GetStoryString(long Origin, string content)
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
                return (content, nextNewNode.content);
            }
            else
            {
                story.Text += content;
                string oldText = story.Text;
                var newResult = WebAPI.NovelAI(MainSave.UID, story.branchid, story.nodes[story.nodes.Count-1].nodeid, story.nid, story.mid, story.Text);
                var nextNewNode = newResult.data.nodes[0];
                story.Text += nextNewNode.content;
                story.nodes.Add(new StoreStory.Node { nodeid = nextNewNode.nodeid, parentid = nextNewNode.parentid });
                Thread.Sleep(500);
                WebAPI.AddNode(MainSave.UID, nextNewNode.nodeid, story.nid, story.Text, newResult.data.nodes.Select(x => x.nodeid).ToArray());
                WebAPI.DoNovelSave_Add(MainSave.UID, story.branchid, story.nid, newResult.data.nodes.Select(x => x.nodeid).ToArray(), story.GenNodes(), story.Text);
                return (oldText, nextNewNode.content);
            }
        }
        public static Bitmap GenStoryPic(string oldText, string newText)
        {
            oldText = "  " + oldText;
            oldText = oldText.Replace("\\n", "\n").Replace("\n", "\n  ");
            newText = newText.Replace("\\n", "\n").Replace("\n", "\n  ");            
            int padding = 20, width = MainSave.PicWidth;
            Font font = new Font(MainSave.Font, 16, FontStyle.Regular);
            int maxWidth = width - padding * 2, charGap = -8, maxHeight = 0;
            using (Bitmap Result = new Bitmap(width, 30000))
            {
                using (Graphics g = Graphics.FromImage(Result))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.FillRectangle(Brushes.White, new RectangleF(0, 0, width, 30000));
                    PointF nowPoint = new PointF(padding, padding);
                    float maxCharWidth = 0;
                    foreach (var item in oldText)
                    {
                        DrawString(g, item, Brushes.Black, ref nowPoint, font, padding, charGap, ref maxCharWidth, maxWidth, out float charHeight);
                    }
                    foreach (var item in newText)
                    {
                        DrawString(g, item, Brushes.Red, ref nowPoint, font, padding, charGap, ref maxCharWidth, maxWidth, out float charHeight);
                        maxHeight = (int)(nowPoint.Y + charHeight + padding);
                    }
                }
                Bitmap tmp = new Bitmap(width, maxHeight);
                using (Graphics g = Graphics.FromImage(tmp))
                    g.DrawImageUnscaled(Result, new Point(0, 0));
                return tmp;
            }
        }
        public static void DrawString(Graphics g, char text, Brush color, ref PointF point, Font font, int padding, int charGap, ref float maxCharWidth, int maxWidth, out float charHeight)
        {
            var charSize = g.MeasureString(text.ToString(), font);
            charHeight = charSize.Height;
            if (text == '\n')
            {
                point.X = padding;
                point.Y += charSize.Height + 2;
                return;
            }
            if (charSize.Width < -charGap)
                charSize.Width = maxCharWidth > 0 ? maxCharWidth : (-charGap) * 3;
            maxCharWidth = Math.Max(maxCharWidth, charSize.Width);
            g.DrawString(text.ToString(), font, color, point);
            WrapTest(maxWidth, padding, charGap, charSize, ref point);
        }
        public static void WrapTest(int maxWidth, int padding, int charGap, SizeF charSize, ref PointF point)
        {
            if (point.X + charSize.Width >= maxWidth)
            {
                point.X = padding;
                point.Y += charSize.Height + 2;
            }
            else
            {
                point.X += charSize.Width + charGap;
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
                lock (StoreStory.StoreInstance)
                {
                    e.FromGroup.SendGroupMessage(MainSave.ThinkText.Split('|').OrderBy(x => Guid.NewGuid().ToString()).First());
                    (string oldText, string newText) = GetStoryString(e.FromGroup, content);
                    using (Bitmap pic = GenStoryPic(oldText, newText))
                    {
                        Directory.CreateDirectory(Path.Combine(MainSave.ImageDirectory, "Story"));
                        string filename = $"{DateTime.Now:yyyyMMDDHHmmss}.png";
                        pic.Save(Path.Combine(MainSave.ImageDirectory, "Story", filename));
                        msg = CQApi.CQCode_Image(Path.Combine("Story", filename)).ToSendString();
                    }
                }
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
                lock (StoreStory.StoreInstance)
                {
                    e.FromQQ.SendPrivateMessage(MainSave.ThinkText.Split('|').OrderBy(x => Guid.NewGuid().ToString()).First());
                    (string oldText, string newText) = GetStoryString(e.FromQQ, content);
                    using (Bitmap pic = GenStoryPic(oldText, newText))
                    {
                        Directory.CreateDirectory(Path.Combine(MainSave.ImageDirectory, "Story"));
                        string filename = $"{DateTime.Now:yyyyMMDDHHmmss}.png";
                        pic.Save(Path.Combine(MainSave.ImageDirectory, "Story", filename));
                        msg = CQApi.CQCode_Image(Path.Combine("Story", filename)).ToSendString();
                    }
                }
            }
            sendText.MsgToSend.Add(msg);
            result.SendObject.Add(sendText);
            return result;
        }
    }
}
