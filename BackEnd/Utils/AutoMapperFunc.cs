using AutoMapper;

namespace Utils
{
    public class AutoMapperFunc
    {
        public static TToChange ChangeValues<TOrigin, TToChange>(object objToGetValue)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigin, TToChange>();
            });
            IMapper mapper = configuration.CreateMapper();
            return mapper.Map<TToChange>(objToGetValue);
        }
    }
}
