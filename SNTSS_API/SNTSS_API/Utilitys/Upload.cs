namespace SNTSS_API.Utilitys
{
    public class Upload
    {

        public async Task<string> UploadPictureUsers(IFormFile picture, string namePicture, string path)
        {
            try
            {
                string dir = Directory.GetCurrentDirectory()+'/';
                var pathCombine = Path.Combine(dir, path, namePicture);
                
                if (File.Exists(pathCombine))
                {
                  namePicture = DateTime.Now.ToString("dd-mm-yyy hh-mm-ss") + namePicture;
                }

                bool folderExists = Directory.Exists(pathCombine); 
                if (!folderExists) Directory.CreateDirectory(path);


                using (Stream stream = new FileStream(pathCombine, FileMode.Create))
                {
                   await picture.CopyToAsync(stream);
                }

                return namePicture;
            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}
