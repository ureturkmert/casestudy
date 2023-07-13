namespace HPASS.Entity.Base.Abstraction
{
    public interface IEntity
    {
        Guid Id { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? CreationDate { get; set; }
        Guid? ModifiedBy { get; set; }
        DateTime? ModificationDate { get; set; }
        bool IsDeleted { get; set; }

    }
}