using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Graxei.Modelo.Generico
{
    public sealed class DownloadFileInfo : DownloadFile
    {
        public FileInfo FileInfo { get; set; }
    }
}
