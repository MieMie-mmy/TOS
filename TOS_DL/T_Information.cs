//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOS_DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Information
    {
        public int InformationID { get; set; }
        public string TitleName { get; set; }
        public byte DestinationFlg { get; set; }
        public byte EffectFlg { get; set; }
        public byte InformationType { get; set; }
        public System.DateTime DisplayStartDate { get; set; }
        public System.DateTime DisplayEndDate { get; set; }
        public System.DateTime Date { get; set; }
        public string AttachedFile1 { get; set; }
        public string AttachedFile2 { get; set; }
        public string AttachedFile3 { get; set; }
        public string AttachedFile4 { get; set; }
        public string DetailInformation { get; set; }
        public string InsertOperator { get; set; }
        public Nullable<System.DateTime> InsertDateTime { get; set; }
        public string UpdateOperator { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
    }
}
