using Application.Dtos.User;
using Application.Helpers;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Common.Exceptions;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Filmunity.UnitTests.Application.Services
{
    [TestFixture]
    public class AuthServiceTests
    {
        #region fields
        private AuthService _service;
        private Mock<IUnitOfWork> _uow;
        private Mock<IMapper> _mapper;
        private Mock<IJwtService> _jwtService;
        private Mock<IHashService> _hashService;
        private UserForLoginDto _userForLoginDto;
        private UserForRegistrationDto _userForRegistrationDto;
        private Mock<IRepository<User>> _userRepo;
        private User _user;
        private Password _password;
        private bool _passwordVerified;
        #endregion

        [SetUp]
        public void SetUp()
        {
            InitializeMocks();
            InitializeObjects();
            InitializeMockSetup();
        }

        [Test]
        public void Login_UserNotFound_ThrowUnauthorizedException()
        {
            _user = null;
            Assert.That(() => _service.Login(_userForLoginDto), Throws.Exception.TypeOf<UnauthorizedException>());
        }

        [Test]
        public void Login_WrongUserPassword_ThrowUnauthorizedException()
        {
            _passwordVerified = false;

            Assert.That(() => _service.Login(_userForLoginDto), Throws.Exception.TypeOf<UnauthorizedException>());
        }

        [Test]
        public async Task Login_UserSuccesfullyLoggedIn_ReturnJwtToken()
        {           
            var result = await _service.Login(_userForLoginDto);

            Assert.That(result, Is.EqualTo("some-token"));
        }

        [Test]
        public async Task Register_WhenCalled_UpdateUserPasswordHashAndSalt()
        {
            await _service.Register(_userForRegistrationDto);

            Assert.That(_user.PasswordHash, Is.Not.Null);
            Assert.That(_user.PasswordSalt, Is.Not.Null);
        }

        [Test]
        public async Task Register_WhenCalled_InvokeRepositoryAddAndUnitOfWorkSaveAsyncOnce()
        {            
            await _service.Register(_userForRegistrationDto);

            _userRepo.Verify(x => x.Add(_user), Times.Once);

            _uow.Verify(x => x.SaveAsync(), Times.Once);
        }

        #region private methods
        private void InitializeMocks()
        {
            _uow = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _jwtService = new Mock<IJwtService>();
            _hashService = new Mock<IHashService>();
            _userRepo = new Mock<IRepository<User>>();
        }

        private void InitializeObjects()
        {
            _service = new AuthService(_uow.Object, _mapper.Object, _jwtService.Object, _hashService.Object);
            _userForLoginDto = new UserForLoginDto
            {
                Username = "string",
                Password = "string"
            };
            _user = new User
            {
                Username = "string",
                Email = "string"
            };
            _userForRegistrationDto = new UserForRegistrationDto
            {
                Username = "string",
                Password = "string"
            };

            _password = new Password
            {
                PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
            };

            _passwordVerified = true;
        }

        private void InitializeMockSetup()
        {
            _mapper.Setup(x => x.Map<User>(_userForRegistrationDto)).Returns(_user);

            _hashService.Setup(x => x.CreatePasswordHash(_userForRegistrationDto.Password))
                .Returns(_password);

            _uow.Setup(x => x.Repository<User>()).Returns(_userRepo.Object);

            _uow.Setup(x => x.Repository<User>()
                .FindOneAsync(It.IsAny<ISpecification<User>>()))
                .Returns(() => Task.FromResult(_user));

            _jwtService.Setup(x => x.GenerateJwtToken(_user)).Returns("some-token");

            _hashService.Setup(x => x.VerifyPasswordHash(
                It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(() => _passwordVerified);
        }
        #endregion
    }

}
