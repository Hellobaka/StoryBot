using Microsoft.VisualStudio.TestTools.UnitTesting;
using me.cqp.luohuaming.Story.Code.OrderFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using me.cqp.luohuaming.Story.PublicInfos;

namespace me.cqp.luohuaming.Story.Code.OrderFunctions.Tests
{
    [TestClass()]
    public class DoStoryTests
    {
        [TestMethod()]
        public void GenStoryPicTest()
        {
            new StoreStory.Story { Origin = 0, IsGroup = false };
            Console.WriteLine(MainSave.Font);
            DoStory.GenStoryPic(File.ReadAllText("old.txt"), File.ReadAllText("new.txt")).Save("1.png");
            Process.Start("1.png");
        }
    }
}