﻿using slideQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slideQ.SmellDetectors
{
    class ChaoticStylistSmellDetectors : ISmellDetector
    {
          private MasterDataModel dataModel;

          public ChaoticStylistSmellDetectors(MasterDataModel dataModel)
        {
            this.dataModel = dataModel;
        }

          public List<PresentationSmell> detect()
          {
              List<PresentationSmell> smellList = new List<PresentationSmell>();
              List<List<CharAttribute>> CharAttrList=  dataModel.SlideDataModelList.Select(x => x.TextFontSize).ToList();
              List<CharAttribute> AllCharObject = new List<CharAttribute>();
              foreach (List<CharAttribute> item in CharAttrList)
              {
                  AllCharObject.AddRange(item);
              }


              List<CustumCharAttribute> CustomList = AllCharObject.Select(x => new CustumCharAttribute { Size = x.Size, Color = x.Color, FontNameofChar = x.FontNameofChar }).ToList();
              int Count = CustomList.GroupBy(x => new {x.Color,x.FontNameofChar,x.Size }).Select(x => x.FirstOrDefault()).Count();
         
      

              if (Count > Constants.Chaotic_Stylist_THRESHOLD)
              {
                  PresentationSmell smell = new PresentationSmell();
                  smell.SmellName = Constants.ChaoticStylist;
                  string Cause = "The tool detected the smell since the slides contains ( " + Count + " ) " + "different styles.";
                  smell.Cause = Cause;
                  smell.SlideNo = 1;
                  smellList.Add(smell);
              }
              return smellList;
          }
    }

    class CustumCharAttribute
    {
        public float Size { get; set; }

        public int Color { get; set; }

        public string FontNameofChar { get; set; }
    }
}
