using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System;

namespace MatrixCommandTool.Helper
{
    public class JsonConfigurationHelper
    {
        private IConfiguration _configuration;
        private readonly string _basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string ConfigPath = System.IO.Path.Combine(Environment.CurrentDirectory, "appSettings.json");

        public void SetConfiguration(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public static void Save()
        {
            try
            {
                if (System.IO.File.Exists(ConfigPath))
                    File.Delete(ConfigPath);
                using (FileStream fs = new FileStream(ConfigPath, FileMode.Create, FileAccess.Write))
                {
                    string str = Newtonsoft.Json.JsonConvert.SerializeObject(GlobalContext.Current.Config);
                    var by = Encoding.Default.GetBytes(str);
                    fs.Write(by, 0, by.Length);
                }
            }
            catch (Exception ex)
            {
                GlobalContext.Current.Logger.Error(ex.Message);
            }
        }
    }
}