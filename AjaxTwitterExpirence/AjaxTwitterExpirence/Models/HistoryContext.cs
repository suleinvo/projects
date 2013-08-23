using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AjaxTwitterExpirence.Models
{
    public class HistoryContext: DbContext
    {
        public DbSet<History> History { get; set; }
    }
}