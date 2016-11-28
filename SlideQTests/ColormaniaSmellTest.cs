﻿using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NUnit.Framework;
using slideQ.Model;
using slideQ.SmellDetectors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlideQTests
{
    class ColormaniaSmellTest
    {
        private List<PresentationSmell> SlideDataModelList = null;

        [SetUp]
        public void GetPPTObject()
        {
            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.WorkDirectory));
            string path = @solution_dir + @"\TestFile\Colormania.pptx";
            string absolute = Path.GetFullPath(path);
            Application ppApp = new Application();
            ppApp.Visible = MsoTriState.msoTrue;
            Presentations oPresSet = ppApp.Presentations;
            _Presentation PPTObject = oPresSet.Open(@path, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoTrue);
            SmellDetector detector = new SmellDetector();
            SlideDataModelList = detector.detectPresentationSmells(PPTObject.Slides);
        }

        [Test]
        public void PositiveTest()
        {
            bool flag = false;
            if (SlideDataModelList.Where(x => x.SlideNo == 2 && x.SmellName.Equals(slideQ.Constants.COLORMANIA)) != null)
                flag = true;
            Assert.AreEqual(true, flag);
        }

        [Test]
        public void NegativeTest()
        {
            bool flag = false;
            if (SlideDataModelList.Where(x => x.SlideNo == 1 && x.SmellName.Equals(slideQ.Constants.COLORMANIA)).Count() == 0)
                flag = true;
            Assert.AreEqual(true, flag);
        }
    }
}
