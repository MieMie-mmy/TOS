﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
  public class M_GroupModel: BaseModel
    {
        public string GroupID { get; set; }

        public string GroupName { get; set; }

        public int GroupInfoFlg { get; set; }

        public string SaveUpdateFlag { get; set; }

    }
}
