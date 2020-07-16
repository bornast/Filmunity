﻿using Application.Dtos.Film;
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
                .AfterMap((src, dest) =>
                {
                    CalculateRating(src.Ratings);
                });

            CreateMap<Film, FilmForListDto>()
                .AfterMap((src, dest) =>
                {
                    CalculateRating(src.Ratings);
                });

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
        }

        private float CalculateRating(ICollection<Rating> ratings)
        {
            return ratings.Count > 0 ? (float)Math.Round(ratings.Select(x => x.RatingValue).Average(), 2) : 0.0f;
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
