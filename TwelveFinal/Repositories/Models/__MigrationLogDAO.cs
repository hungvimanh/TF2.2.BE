using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class __MigrationLogDAO
    {
        public Guid migration_id { get; set; }
        public string script_checksum { get; set; }
        public string script_filename { get; set; }
        public DateTime complete_dt { get; set; }
        public string applied_by { get; set; }
        public byte deployed { get; set; }
        public string version { get; set; }
        public string package_version { get; set; }
        public string release_version { get; set; }
        public int sequence_no { get; set; }
    }
}
