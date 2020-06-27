using Application.Models;
using Application.Interfaces;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Application.Dtos.Photo;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using Application.Services.Photo;
using Application.Interfaces.Common;
using FluentAssertions;
using System.Collections.Generic;
using Application.Specifications.Photo;

namespace UnitTests.Application.Services.Photo
{
    [TestFixture]
    public class PhotoServiceTests
    {
        #region fields

        private Mock<IMapper> _mapper;
        private Mock<IUnitOfWork> _uow;
        private Mock<ICloudUploadService> _cloudUploadService;
        private PhotoService _service;
        private PhotoForCreationDto _photoForCreation;
        private PhotoCloudUploadResult _uploadResult;
        private Domain.Entities.Photo _photo;
        private Domain.Entities.Photo _mainPhoto;
        private IEnumerable<Domain.Entities.Photo> _photos;
        private IEnumerable<PhotoForDetailedDto> _photosForDetailed;
        private IMainPhotoUploadable _mainPhotoUploadable;

        #endregion

        #region setup

        [SetUp]
        public void SetUp()
        {
            InitializeMocks();
            InitializeObjects();
            InitializeMockSetup();
        }

        #endregion

        #region tests

        [Test]
        public async Task Upload_NoMainPhoto_SetPhotoAsMain()
        {
            _mainPhoto = null;

            await _service.Upload(_photoForCreation);

            _cloudUploadService.Verify(x => x.UploadPhotoToCloud(_photoForCreation.File), Times.Once);

            _photo.IsMain.Should().BeTrue();

            _uow.Verify(x => x.Repository<Domain.Entities.Photo>().Add(_photo), Times.Once);

            _uow.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task Upload_MainPhotoExists_DontSetPhotoAsMain()
        {
            await _service.Upload(_photoForCreation);

            _cloudUploadService.Verify(x => x.UploadPhotoToCloud(_photoForCreation.File), Times.Once);

            _photo.IsMain.Should().BeFalse();

            _uow.Verify(x => x.Repository<Domain.Entities.Photo>().Add(_photo), Times.Once);

            _uow.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task GetEntityPhotos_NoEntityPhotos_ReturnEmptyList()
        {
            _photos = null;

            _photosForDetailed = new List<PhotoForDetailedDto>();

            var result = await _service.GetEntityPhotos(1, 1);

            result.Should().BeEmpty();
        }

        [Test]
        public async Task GetEntityPhotos_PhotosExist_ReturnListOfPhotos()
        {
            var result = await _service.GetEntityPhotos(1, 1);

            result.Should().NotBeEmpty();
        }

        [Test]
        public async Task IncludeMainPhoto_NoMainPhoto_SetMainPhotoToNull()
        {
            _mainPhoto = null;

            await _service.IncludeMainPhoto(_mainPhotoUploadable, (int)EntityTypes.Film);

            _mainPhotoUploadable.MainPhoto.Should().BeNull();
        }

        [Test]
        public async Task IncludeMainPhoto_MainPhotoExists_MainPhotoShouldBeSet()
        {
            await _service.IncludeMainPhoto(_mainPhotoUploadable, (int)EntityTypes.Film);

            _mainPhotoUploadable.MainPhoto.Should().BeNull();
        }

        #endregion

        #region private methods

        private void InitializeMocks()
        {
            _uow = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _cloudUploadService = new Mock<ICloudUploadService>();
        }

        private void InitializeObjects()
        {
            _service = new PhotoService(_cloudUploadService.Object, _mapper.Object, _uow.Object);

            _photoForCreation = new PhotoForCreationDto
            {
                File = new Mock<IFormFile>().Object,
                Description = "description",
                EntityTypeId = (int)EntityTypes.Film,
                EntityId = 1
            };

            _uploadResult = new PhotoCloudUploadResult
            {
                PublicId = "PublicId",
                Url = "Url"
            };

            _photo = new Domain.Entities.Photo();

            _mainPhoto = new Domain.Entities.Photo();

            _photos = new List<Domain.Entities.Photo>();

            _photosForDetailed = new List<PhotoForDetailedDto>
            {
                new PhotoForDetailedDto {},
                new PhotoForDetailedDto {}
            };

            _mainPhotoUploadable = new MainPhotoUploadable
            {
                Id = 1
            };
        }

        private void InitializeMockSetup()
        {
            _cloudUploadService.Setup(x => x.UploadPhotoToCloud(_photoForCreation.File)).Returns(_uploadResult);

            _mapper.Setup(x => x.Map<Domain.Entities.Photo>(_photoForCreation)).Returns(_photo);

            _uow.Setup(x => x.Repository<Domain.Entities.Photo>()
                .FindOneAsync(It.IsAny<ISpecification<Domain.Entities.Photo>>()))
                .Returns(() => Task.FromResult(_mainPhoto));

            _uow.Setup(x => x.Repository<Domain.Entities.Photo>()
                .FindAsync(It.IsAny<PhotoFilterSpecification>()))
                .Returns(() => Task.FromResult(_photos));

            _mapper.Setup(x => x.Map<IEnumerable<PhotoForDetailedDto>>(_photos)).Returns(_photosForDetailed);
        }

        #endregion
    }

    public class MainPhotoUploadable : IMainPhotoUploadable
    {
        public int Id { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }
    }

}
