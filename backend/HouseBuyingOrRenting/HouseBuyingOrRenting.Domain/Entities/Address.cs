using HouseBuyingOrRenting.Domain.Enums;

namespace HouseBuyingOrRenting.Domain
{
    public class Address
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public AddressType Type { get; set; }
    }
}
