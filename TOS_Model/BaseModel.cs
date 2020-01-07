using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
    public class BaseModel
    {
        public string InsertOperator { get; set; }

        public string AccessPC { get; set; }
        public DateTime InsertDateTime { get; set; }
        public string UpdateOperator { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public string Date { get; set; }

        public string TitleName { get; set; }

        public int InformationType { get; set; }


    }
}
