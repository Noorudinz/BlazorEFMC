using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Shared
{
    public class BTU
    {
       [Key]
       public Int64 ID { get; set; }
       public string FlatNo { get; set; }
       public string MeterID { get; set; }
       public decimal? Reading { get; set; }
       public DateTime? ReadingDate { get; set; }
       public DateTime? CreatedDate { get; set; }
       public bool flag { get; set; }
    }

    public class UploadedFile
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }

    public class SaveFile
    {
        public List<FileData> Files { get; set; }
    }

    public class FileData
    {
        public byte[] Data { get; set; }
        public string FileType { get; set; }
        public long Size { get; set; }
    }
}
