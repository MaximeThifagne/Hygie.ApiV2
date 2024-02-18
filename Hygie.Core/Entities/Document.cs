using Hygie.Core.Entities.Base;

namespace Hygie.Core.Entities
{
    public class Document : BaseEntity
    {
        public string UserId { get; set; }

        public string DocumentType { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public bool Verified { get; set; }

        #region FILE
        public long Size { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public byte[] Data { get; set; }
        #endregion
    }
}

