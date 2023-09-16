using Hygie.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hygie.Core.Entities
{
    public class ProfilePicture : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public long Size { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public byte[] Data { get; set; }
    }
}
