using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Romulox.Core.NoIntro.Helpers;

namespace Romulox.Core.Helpers
{
    public class ImageDownloader
    {
        private string webRootPath;
        private string imagesPath;

        public ImageDownloader(string webRootPath, string imagesPath)
        {
            this.webRootPath = webRootPath;
            this.imagesPath = imagesPath;
        }

        public async Task<string> DownloadImageAsync(string url, string file)
        {
            var md5Hash = DatFilesHelper.ComputeMd5Hash(file);

            // if the image already exists then return it
            var image = Directory.GetFiles(webRootPath + imagesPath).FirstOrDefault(i => i.Contains(md5Hash));
            
            var relativePath = imagesPath + md5Hash + ".jpg";
            
            if (image != null)
            {
                return relativePath;
            }

            var downloadPath = webRootPath + relativePath;

            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("user-agent", "Romulox Comes In Peace.");
                
                await webClient.DownloadFileTaskAsync(url, downloadPath);
            }
            
            return relativePath;
        }
    }
}