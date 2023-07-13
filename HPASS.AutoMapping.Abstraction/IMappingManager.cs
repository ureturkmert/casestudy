namespace HPASS.AutoMapping.Abstraction
{
    public interface IMappingManager
    {
        TTo Map<TFrom, TTo>(TFrom entity);

        TTo Map<TFrom, TTo>(TFrom source, TTo destination);

        IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> entities);

    }

}