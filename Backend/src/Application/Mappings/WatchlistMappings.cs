using Application.Dtos.Common;
using Application.Dtos.Watchlist;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Mappings
{
    public class WatchlistMappings : Profile
    {
        public WatchlistMappings()
        {
            CreateMap<Watchlist, WatchlistForDetailedDto>();

            CreateMap<Watchlist, WatchlistForListDto>();

            CreateMap<WatchlistForCreationDto, Watchlist>()
                .ForMember(x => x.Films,opt => opt.MapFrom(x => x.Films
                .Select(x => new FilmWatchlist { Sequence = x.Sequence, FilmId = x.FilmId })));

            CreateMap<FilmWatchlist, FilmForWatchlistDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.FilmId))
            .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Film.Title))
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Film.Description))
            .ForMember(x => x.MainPhoto, opt => opt.Ignore());

            CreateMap<WatchlistForUpdateDto, Watchlist>()
                .ForMember(x => x.Films, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    HandleFilms(src, dest);
                });
        }
        
        private void HandleFilms(WatchlistForUpdateDto src, Watchlist dest)
        {
            // remove films
            var filmsToRemove = dest.Films
            .Where(x => !src.Films.Select(x => x.FilmId).Contains(x.FilmId)).ToList();

            foreach (var filmToRemove in filmsToRemove)
                dest.Films.Remove(filmToRemove);

            // update films
            foreach (var film in dest.Films.Where(x => src.Films.Select(x => x.FilmId).Contains(x.FilmId)))
            {
                var filmFromRequest = src.Films.FirstOrDefault(x => x.FilmId == film.FilmId);

                if (filmFromRequest != null)
                {
                    film.Sequence = filmFromRequest.Sequence;
                    film.IsWatched = filmFromRequest.IsWatched;
                }
            }

            // add films
            var filmsToAdd = src.Films.Where(f => !dest.Films.Any(x => x.FilmId == f.FilmId)).ToList()
            .Select(x => new FilmWatchlist { Sequence = x.Sequence, FilmId = x.FilmId, IsWatched = x.IsWatched });

            foreach (var filmToAdd in filmsToAdd)
                dest.Films.Add(filmToAdd);
        }
    }
}
