using AutoMapper;
using HPASS.AutoMapping.Abstraction;

namespace HPASS.AutoMapping.Implementation
{
    public class MappingManager : IMappingManager
    {

        private readonly IMapper _mapper;


        public MappingManager(IMapper _mapper)
        {
            this._mapper = _mapper;
        }


        public TTo Map<TFrom, TTo>(TFrom entity)
        {

            return this._mapper.Map<TFrom, TTo>(entity);
        }


        public TTo Map<TFrom, TTo>(TFrom source, TTo destination)
        {

            return this._mapper.Map(source, destination);
        }


        public IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> entities)
        {

            return this._mapper.Map<IEnumerable<TFrom>, IEnumerable<TTo>>(entities);
        }
    }

}