using NUnit.Framework;
using System.Configuration;

namespace Tests
{
    public class Configuration : IConfiguration
    {

        public string savefood
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
            }
        }

    }


    public interface IConfiguration
    {
        string savefood { get; }
    }
}