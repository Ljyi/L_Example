//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupplyModel
    {
        public int SupplyModelID { get; set; }
        public Nullable<int> ModelCode { get; set; }
        public string ModelName { get; set; }
        public string ModelPath { get; set; }
        public Nullable<bool> IsEnable { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsSupplyBase { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastUpdUser { get; set; }
        public Nullable<System.DateTime> LastUpdTime { get; set; }
    }
}
