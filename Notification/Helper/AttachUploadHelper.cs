using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;

namespace Notification.Helper
{
    public static class AttachUploadHelper
    {
        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        public static string PrepareAttachmentDetails(IFormFile file, string webRootPath, string contentId = "upload")
        {
            if (file == null)
                return null;

            var listAttachments = new List<AttachmentQueue>();
            string uploads = Path.Combine(webRootPath, "uploads");
            string path = Path.Combine(uploads, Guid.NewGuid().ToString());

            string filePath = Path.Combine(path, file.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            string mimeType = GetMimeType(filePath);

            var attachment = new AttachmentQueue
            {
                ContentId = contentId,
                Type = mimeType,
                Disposition = "inline",
                FileName = file.FileName,
                Content = filePath
            };

            listAttachments.Add(attachment);
            return JsonConvert.SerializeObject(listAttachments);
        }

        public static byte[] GetBase64FileContent(string attachmentUrl)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                attachmentUrl);
            Byte[] bytes = File.ReadAllBytes(path);
            return bytes;
        }
    }
}
