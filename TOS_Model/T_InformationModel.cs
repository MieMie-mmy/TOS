using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
    public class T_InformationModel:BaseModel 
    {
        public int ID { get; set; }
        public int InformationID { get; set; }
        public string Date { get; set; }

        public string TitleName { get; set; }

        public int InformationType { get; set; }

        public DateTime DisplayStartDate { get; set; }

        public DateTime DisplayEndDate { get; set; }

        public string AttachedFile1 { get; set; }

        public string AttachedFile2 { get; set; }

        public string AttachedFile3 { get; set; }

        public string AttachedFile4 { get; set; }

        public string DetailInformation { get; set; }

        public int DestinationFlag { get; set; }

        public bool EffectFlag { get; set; }
    }
}
