using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class AtrmsDocumentsDtlTemp
    {
        public decimal Sno { get; set; }
        public string AtId { get; set; }
        public string RecCode { get; set; }
        public string FileName { get; set; }
        public byte[] Uploadfile { get; set; }
        public string Name { get; set; }
        public string Mimetype { get; set; }
        public DateTime? StatusDt { get; set; }
        public decimal? Filesize { get; set; }
        public decimal? Fileid { get; set; }
    }
}
