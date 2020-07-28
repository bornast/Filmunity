using Application.Dtos.Common;
using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Film
{
    public class FilmForDetailedDto : IPhotoUploadable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }        
        public float Rating { get; set; }
        public float ImdbRating { get; set; }
        public int Year { get; set; }
        public string Duration { get; set; }        
        public RecordNameDto Type { get; set; }
        public RecordNameDto Country { get; set; }
        public RecordNameDto Language { get; set; }
        public IEnumerable<RecordNameDto> Genres { get; set; } = new List<RecordNameDto>();
        public IEnumerable<ParticipantRoleForDetailedDto> Participants { get; set; } = new List<ParticipantRoleForDetailedDto>();
        public IEnumerable<PhotoForDetailedDto> Photos { get; set; } = new List<PhotoForDetailedDto>();        
    }
}
