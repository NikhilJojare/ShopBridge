#region FileUploadController
/**************************************************************************************************
 * Type                     : Class File
 * File                     : FileUploadController.cs
 * Purpose                  : This class is used for Upload files
 * Date                     : 23/02/2021
 * Last Updated             : 23/02/2021
 * Author                   : Nikhil Jojare.
 * Version                  : 1.0.0.0
 * Internal Dependencies    : N/A
 * External Dependencies    : N/A
 * Assembly                 :
 * Environment              : Microsoft Visual Studio.NET 2017
 * Framework                : Microsoft.Net 4.6.1
 *
 ****************************************************************************************************
 * Modification history:
 ****************************************************************************************************
 * Modified By          Modified On             Reason

 *****************************************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ShopBridge.Controllers
{
    public class FileUploadController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UploadFiles(string moduleName, string fileName)
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/" + moduleName + "/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Save the Files.
            foreach (string key in HttpContext.Current.Request.Files)
            {
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[key];
                postedFile.SaveAs(path + fileName);
            }

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage CropAndSaveImage(string moduleName, string fileName, int x, int y, int w, int h)
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/" + moduleName + "/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Save the Files.
            foreach (string key in HttpContext.Current.Request.Files)
            {
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[key];
                postedFile.SaveAs(path + fileName);

                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/" + moduleName), fileName);
                string cropFileName = "";
                string cropFilePath = "";
                if (File.Exists(filePath))
                {
                    System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
                    Rectangle CropArea = new Rectangle(x, y, w, h);
                    try
                    {
                        Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                        using (Graphics g = Graphics.FromImage(bitMap))
                        {
                            g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                        }
                        cropFileName = "crop_" + fileName;
                        cropFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/" + moduleName), cropFileName);
                        bitMap.Save(cropFilePath);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
