﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SupplyCoordinationEntities : DbContext
    {
        public SupplyCoordinationEntities()
            : base("name=SupplyCoordinationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SendSupplyMail> SendSupplyMail { get; set; }
        public virtual DbSet<SupplyAccount> SupplyAccount { get; set; }
        public virtual DbSet<SupplyAction> SupplyAction { get; set; }
        public virtual DbSet<SupplyCategory> SupplyCategory { get; set; }
        public virtual DbSet<SupplyLoginLog> SupplyLoginLog { get; set; }
        public virtual DbSet<SupplyMaiMatch> SupplyMaiMatch { get; set; }
        public virtual DbSet<SupplyModel> SupplyModel { get; set; }
        public virtual DbSet<SupplyModelAction> SupplyModelAction { get; set; }
        public virtual DbSet<SupplyOperation> SupplyOperation { get; set; }
        public virtual DbSet<SupplyOperationDetail> SupplyOperationDetail { get; set; }
        public virtual DbSet<SupplyPostData> SupplyPostData { get; set; }
    }
}
