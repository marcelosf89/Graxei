using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FAST.Model
{
    public sealed class DownloadByte: DownloadFile
    {
        public byte[] FileByte { get; set; }
    }
}
