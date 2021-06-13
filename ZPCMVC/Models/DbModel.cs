using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ZPCMVC.Models
{
    public class DbModel:DbContext
    {
        public DbModel() : base("name=ZPCMVCConnectionSQL")
        {
        }
        public System.Data.Entity.DbSet<ZPCMVC.Models.Poetry> Poetrys { get; set; }
    }
}