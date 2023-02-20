using Microsoft.Data.SqlClient.Server;
using static System.Net.Mime.MediaTypeNames;

namespace SNTSS_API.Utilitys
{
    public class Download
    {
        private IWebHostEnvironment _environment;

        public string DownloadImg(string pathName, string path)
        {
            try
            {
                string dir = Directory.GetCurrentDirectory() + '/';
                var fullName =  Path.Combine(dir, path, pathName);
                using (var fs = new FileStream(fullName,FileMode.Open,FileAccess.Read))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Convert Image to byte[]
                        fs.CopyTo(ms);
                        byte[] imageBytes = ms.ToArray();

                        // Convert byte[] to Base64 String
                        string base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }
            catch (Exception e)
            {
                return "error: " + e.Message;
            }
        }
    }
}
