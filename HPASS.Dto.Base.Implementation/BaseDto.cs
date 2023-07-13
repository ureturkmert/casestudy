using HPASS.Dto.Base.Abstraction;

namespace HPASS.Dto.Base.Implementation
{
    public class BaseDto : IDto
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}