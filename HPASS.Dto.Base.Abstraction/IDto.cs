namespace HPASS.Dto.Base.Abstraction
{
    public interface IDto
    {
        Guid Id { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? CreationDate { get; set; }
        Guid? ModifiedBy { get; set; }
        DateTime? ModificationDate { get; set; }
        bool IsDeleted { get; set; }
    }
}