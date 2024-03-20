using EyeAdvertisingDotNetTask.Core.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeAdvertisingDotNetTask.Core.Helpers
{
    public class ImageHelper
    {
        // GetImageLinks function deserializes json array of Image names and returns a list of image paths
        public static List<string> GetImageLinks(string jsonImgsArr)
        {
            var imgsFinalArr = new List<string>();
            if (!string.IsNullOrWhiteSpace(jsonImgsArr))
            {
                var imgsArr = JsonConvert.DeserializeObject<List<string>>(jsonImgsArr);
                if (imgsArr != null && imgsArr.Any())
                {
                    foreach (var img in imgsArr)
                    {
                        var imgFullPath = string.Concat(FolderNames.BaseLink, "/", FolderNames.Images, "/", img);
                        imgsFinalArr.Add(imgFullPath);
                    }
                }
            }

            return imgsFinalArr;
        }
    }
}
