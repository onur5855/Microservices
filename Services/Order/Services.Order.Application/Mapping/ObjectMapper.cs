using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Mapping
{
    public static class ObjectMapper
    {// lazy => object Mapper ne zaman  kullanırsam  o zaman  mappleme yapıcak mapplemesi için çağrılması gerekicek
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config=new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustumMapping>();
            });
            return config.CreateMapper();
        });


        public static IMapper Mapper=> lazy.Value;
    }
}
