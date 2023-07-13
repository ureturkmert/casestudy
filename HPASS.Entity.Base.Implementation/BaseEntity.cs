
using HPASS.Entity.Base.Abstraction;

namespace HPASS.Entity.Base.Implementation
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}