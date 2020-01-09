﻿
using System.Linq;
using TOS_Model;
using TOS_DL;

namespace Company_BL
{
    public class CompanyBL
    {
        public M_CompanyModel CheckLogin(M_CompanyModel mcmodel)
        {
            TOSEntities ent = new TOSEntities();
            M_Company mc = ent.M_Company.Where(m => m.CompanyCD == mcmodel.CompanyCD && m.Password == mcmodel.Password).SingleOrDefault();

            if (mc == null)
                return null;
            else
            {
                mcmodel.CompanyName = mc.CompanyName;
                mcmodel.UserRole = mc.UserRole;
                return mcmodel;
            }
        }
    }
}
