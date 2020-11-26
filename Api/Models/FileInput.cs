using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class FileInput
    {
        public string base64 { get; set; }
        public FileType type { get; set; }

        public enum FileType
        {
            IMG = 0,
            PDF = 1
        }
    }
}