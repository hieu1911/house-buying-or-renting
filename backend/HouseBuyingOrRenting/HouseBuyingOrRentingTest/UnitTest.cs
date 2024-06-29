using AutoMapper;
using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace HouseBuyingOrRentingTest
{
    [TestFixture]
    public class Tests
    {
        private UserService _userService;
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IOptions<HashPasswordOptions> _options;
        
        private RealEstateService _realEstateService;
        private IRealEstateRepository _realEstateRepository;

        [SetUp]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _options = Substitute.For<IOptions<HashPasswordOptions>>();
            _realEstateRepository = Substitute.For<IRealEstateRepository>();

            _userService = new UserService(_userRepository, _mapper, _options);
            _realEstateService = new RealEstateService(_realEstateRepository, _mapper);

        }

        [TestCase("12345")]
        [TestCase("123456")]
        [TestCase("abcde")]
        [TestCase("abcdef")]
        [TestCase("abc12")]
        public async Task IsPassWordValid_NotValid(string password)
        {
            var result = _userService.IsPasswordValid(password);

            Assert.That(result, Is.EqualTo(false));
        }

        [TestCase("abc123")]
        public async Task IsPassWordValid_Valid(string password)
        {
            var result = _userService.IsPasswordValid(password);

            Assert.That(result, Is.EqualTo(true));
        }

        [TestCase(-180, 0)]
        [TestCase(-179, 0)]
        [TestCase(179, 0)]
        [TestCase(180, 0)]
        [TestCase(0, -80)]
        [TestCase(0, -89)]
        [TestCase(0, 89)]
        [TestCase(0, 80)]
        public async Task CheckLatAndLong_Valid(double longitude, double latitude)
        {
            var reuslt = _realEstateService.CheckLatAndLong(longitude, latitude);

            Assert.That(reuslt, Is.True);
        }

        [TestCase(-181, 0)]
        [TestCase(181, 0)]
        [TestCase(0, -91)]
        [TestCase(0, 91)]
        public async Task CheckLatAndLong_NotValid(double longitude, double latitude)
        {
            var reuslt = _realEstateService.CheckLatAndLong(longitude, latitude);

            Assert.That(reuslt, Is.False);
        }
    }
}