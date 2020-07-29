using Application.Dtos.Common;
using Application.Dtos.Person;
using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Person;
using Application.Interfaces.Photo;
using Application.Specifications.Person;
using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _context;

        public PersonService(IUnitOfWork uow, IMapper mapper, ICurrentUserService currentUserService, IPhotoService photoService, IHttpContextAccessor context)
        {
            _uow = uow;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _photoService = photoService;
            _context = context;
        }

        public async Task<PersonForDetailedDto> GetOne(int id)
        {
            var person = await _uow.Repository<Domain.Entities.Person>().FindByIdAsync(id);

            if (person == null)
                return null;

            var personToReturn = _mapper.Map<PersonForDetailedDto>(person);

            await _photoService.IncludePhotos(personToReturn, (int)EntityTypes.Person);

            return personToReturn;
        }

        public async Task<IEnumerable<PersonForListDto>> GetAll(PersonFilterDto personFilter)
        {
            var persons = await _uow.Repository<Domain.Entities.Person>().FindAsyncWithPagination(new PersonFilterPaginatedSpecification(personFilter));

            _context.HttpContext.Response.AddPagination(persons.CurrentPage, persons.PageSize, persons.TotalCount, persons.TotalPages);

            var personsToReturn = _mapper.Map<IEnumerable<PersonForListDto>>(persons);

            await _photoService.IncludeMainPhoto(personsToReturn, (int)EntityTypes.Person);

            return personsToReturn;
        }        

        public async Task<PersonForDetailedDto> Create(PersonForSaveDto personForCreation)
        {
            var person = _mapper.Map<Domain.Entities.Person>(personForCreation);

            _uow.Repository<Domain.Entities.Person>().Add(person);

            await _uow.SaveAsync();

            var personToReturn = _mapper.Map<PersonForDetailedDto>(person);

            return personToReturn;
        }

        public async Task<PersonForDetailedDto> Update(int id, PersonForSaveDto personForUpdate)
        {
            var person = await _uow.Repository<Domain.Entities.Person>().FindByIdAsync(id);

            _mapper.Map(personForUpdate, person);

            await _uow.SaveAsync();

            var personToReturn = _mapper.Map<PersonForDetailedDto>(person);

            await _photoService.IncludePhotos(personToReturn, (int)EntityTypes.Person);

            return personToReturn;
        }

        public async Task Delete(int id)
        {
            var person = await _uow.Repository<Domain.Entities.Person>().FindByIdAsync(id);

            if (person == null)
                throw new NotFoundException(nameof(Domain.Entities.Person));

            _uow.Repository<Domain.Entities.Person>().Remove(person);

            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<RecordNameDto>> GetRecordNames()
        {
            var persons = await _uow.Repository<Domain.Entities.Person>().FindAsync();

            var personsToReturn = _mapper.Map<IEnumerable<RecordNameDto>>(persons);

            return personsToReturn;
        }

    }
}
