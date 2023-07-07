using Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Domain.OrderAggregate
{
    public class Adress : ValueObject
    {
        public string Province { get; private set; }//il
        public string District { get; private set; }//ilce
        public string Street { get; private set; } //cadde
        public string ZipCode { get; private set; }
        public string Line { get; private set; }

        public Adress(string province, string district, string street, string zipCode, string line)
        {
            Province = province;
            District = district;
            Street = street;
            ZipCode = zipCode;
            Line = line;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            //yield return new object[] { Province, District, Street, ZipCode };
            yield return Province; 
            yield return District; 
            yield return Street; 
            yield return ZipCode;
            yield return Line;
        }
    }
}
