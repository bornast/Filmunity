using Application.Dtos.Photo;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Photo;
using Application.Specifications.Photo;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Photo
{
    public class PhotoService : IPhotoService
    {
        private readonly ICloudUploadService _cloudUploadService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public PhotoService(ICloudUploadService cloudUploadService, IMapper mapper, IUnitOfWork uow)
        {
            _cloudUploadService = cloudUploadService;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task Upload(PhotoForCreationDto photoForCreation)
        {
            var uploadResult = _cloudUploadService.UploadPhotoToCloud(photoForCreation.File);

            var photo = _mapper.Map<Domain.Entities.Photo>(photoForCreation);

            photo.Url = uploadResult.Url;
            photo.PublicId = uploadResult.PublicId;

            var mainPhotoSpecification = new PhotoFilterSpecification(photoForCreation.EntityTypeId, photoForCreation.EntityId, isMain: true);
            var mainPhoto = await _uow.Repository<Domain.Entities.Photo>().FindOneAsync(mainPhotoSpecification);

            if (mainPhoto == null)
                photo.IsMain = true;

            _uow.Repository<Domain.Entities.Photo>().Add(photo);

            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<PhotoForDetailedDto>> GetEntityPhotos(int entityTypeId, int entityId) 
        {
            var photoSpecification = new PhotoFilterSpecification(entityTypeId, entityId);

            var photos = await _uow.Repository<Domain.Entities.Photo>().FindAsync(photoSpecification);

            var result = _mapper.Map<IEnumerable<PhotoForDetailedDto>>(photos);

            return result;
        }

        public async Task IncludePhotos(IPhotoUploadable entity, int entityTypeId)
        {
            entity.Photos = await GetEntityPhotos(entityTypeId, entity.Id);

            entity.MainPhoto = entity.Photos.FirstOrDefault(x => x.IsMain);
        }

        public async Task IncludeMainPhoto(IMainPhotoUploadable entity, int entityTypeId)
        {
            var photoSpecification = new PhotoFilterSpecification(entityTypeId, entity.Id, isMain: true);

            var photo = await _uow.Repository<Domain.Entities.Photo>().FindOneAsync(photoSpecification);

            entity.MainPhoto = _mapper.Map<PhotoForDetailedDto>(photo);
        }

        public async Task IncludeMainPhoto(IEnumerable<IMainPhotoUploadable> entities, int entityTypeId)
        {
            foreach (var entity in entities)
            {
                await IncludeMainPhoto(entity, entityTypeId);
            }
        }
    }
}