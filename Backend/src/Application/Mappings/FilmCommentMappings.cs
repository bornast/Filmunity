using Application.Dtos.FilmComment;
using AutoMapper;

namespace Application.Mappings
{
    public class FilmCommentMappings : Profile
    {
        public FilmCommentMappings()
        {
            CreateMap<Domain.Entities.FilmComment, FilmCommentForListDto>()
                .ForMember(x => x.User, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.User.Id = src.UserId;
                    dest.User.Name = src.User.FirstName + " " + src.User.LastName;
                    dest.User.Username = src.User.Username;
                });

            CreateMap<FilmCommentForCreationDto, Domain.Entities.FilmComment>();
        }
    }
}
