using Microsoft.VisualStudio.TestTools.UnitTesting;
using me.cqp.luohuaming.Story.Code.OrderFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace me.cqp.luohuaming.Story.Code.OrderFunctions.Tests
{
    [TestClass()]
    public class DoStoryTests
    {
        [TestMethod()]
        public void GenStoryPicTest()
        {
            DoStory.GenStoryPic(File.ReadAllText("old.txt"), File.ReadAllText("new.txt")).Save("1.png");
            Process.Start("1.png");
        }
    }
}