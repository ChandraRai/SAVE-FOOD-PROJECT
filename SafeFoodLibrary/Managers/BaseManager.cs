using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeFoodLibrary.Managers
{
    public class BaseManager
    {
        protected string connStr { get; set; }
        public BaseManager(string connectionString)
        {
            connStr = connectionString;
        }
    }
}
