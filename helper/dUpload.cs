using Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Helper
{
    public class dUpload
    {
        /*
          
            dUpload.Upload("input_field_name","~/Uploads/");
          
         */

        public static string Upload(string uploadInputFieldName,string uploadFilePath)
        {
            /*multipart must reuire*/
            var filename = Path.GetFileName(System.Web.HttpContext.Current.Request.Files[uploadInputFieldName].FileName);

            filename = dUniqId.get()+ filename.Replace(' ','_');
            var Uploadpath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(uploadFilePath), filename);
            System.Web.HttpContext.Current.Request.Files[uploadInputFieldName].SaveAs(Uploadpath);
            string[] arr = uploadFilePath.Split('~');
            string UploadpathUrl = arr[1] + filename;

            //string UploadpathUrl = "/Uploads/" + filename;

            return UploadpathUrl;

        }
    }
}