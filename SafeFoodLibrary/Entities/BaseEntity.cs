using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeFoodLibrary.Entities
{
    public class BaseEntity
    {
        protected string connStr { get; set; }
        public BaseEntity(string connectionString)
        {
            connStr = connectionString;
        }
    }
}
