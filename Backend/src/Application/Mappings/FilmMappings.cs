using Application.Dtos.Common;
using Application.Dtos.Film;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Mappings
{
    public class FilmMappings : Profile
    {
        public FilmMappings()
        {
            CreateMap<Film, FilmForDetailedDto>()
                .ForMember(x => x.Type, opt => opt.MapFrom(x => new RecordNameDto { Id = x.Type.Id, Name = x.Type.Name }))
                .ForMember(x => x.Country, opt => opt.MapFrom(x => new RecordNameDto { Id = x.Country.Id, Name = x.Country.Name }))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => new RecordNameDto { Id = x.Language.Id, Name = x.Language.Name }))
                .ForMember(x => x.Genres, opt =>
                opt.MapFrom(x => x.Genres.Select(x => 
                    new RecordNameDto { Id = x.Genre.Id, Name = x.Genre.Name })))
                .ForMember(x => x.Participants, opt =>
                opt.MapFrom(x => x.Participants.Select(x => new ParticipantRoleForDetailedDto
                {
                    Participant = new ParticipantWithPhotoDto
                    {
                        Id = x.PersonId,
                        Name = x.Person.FirstName + " " + x.Person.LastName
                    },
                    Role = new RecordNameDto
                    {
                        Id = x.FilmRoleId,
                        Name = x.FilmRole.Name
                    }
                })));

            CreateMap<Film, FilmForListDto>()
                .ForMember(x => x.Genres, opt => opt.MapFrom(x => x.Genres.Select(x => x.Genre.Name)));

            CreateMap<FilmForCreationDto, Film>()
                .ForMember(x => x.Genres, 
                    opt => opt.MapFrom(x => x.GenreIds.Select(x => new FilmGenre { GenreId = x }) ))
                .ForMember(x => x.Participants,
                    opt => opt.MapFrom(x => x.ParticipantsRoles.Select(x => 
                        new FilmParticipant { PersonId = x.ParticipantId, FilmRoleId = x.RoleId })));

            CreateMap<FilmForUpdateDto, Film>()
                .AfterMap((src, dest) =>
                {
                    HandleFilmGenres(src, dest);
                    HandleFilmParticipants(src, dest);
                });

            CreateMap<Film, RecordNameDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => $"{x.Title} ({x.Year})"));
        }

        private void HandleFilmGenres(FilmForUpdateDto src, Film dest)
        {
            // remove genres
            var genresToRemove = dest.Genres.Where(x => !src.GenreIds.Contains(x.GenreId)).ToList();

            foreach (var genreToRemove in genresToRemove)
                dest.Genres.Remove(genreToRemove);

            // add genres
            var genresToAdd = src.GenreIds.Where(id => !dest.Genres.Any(x => x.GenreId == id)).ToList()
            .Select(id => new FilmGenre { GenreId = id });

            foreach (var genreToAdd in genresToAdd)
                dest.Genres.Add(genreToAdd);
        }

        private void HandleFilmParticipants(FilmForUpdateDto src, Film dest)
        {
            /// remove film participants
            var participantsToRemove = dest.Participants
            .Where(x => !src.ParticipantsRoles.Select(x => new { x.ParticipantId, x.RoleId })
                .Any(y => y.ParticipantId == x.PersonId && y.RoleId == x.FilmRoleId)).ToList();

            foreach (var participantToRemove in participantsToRemove)
                dest.Participants.Remove(participantToRemove);

            // add film participants
            var participantsToAdd = src.ParticipantsRoles
            .Where(x => !dest.Participants.Any(y => y.PersonId == x.ParticipantId && y.FilmRoleId == x.RoleId)).ToList()
            .Select(x => new FilmParticipant { PersonId = x.ParticipantId, FilmRoleId = x.RoleId });

            foreach (var participantToAdd in participantsToAdd)
                dest.Participants.Add(participantToAdd);
        }

    }
}
