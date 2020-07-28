using Application.Dtos.Common;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Film
{
    public class FilmForCreationDto : IObjectToValidate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public int TypeId { get; set; }
        public int Year { get; set; }
        public string Duration { get; set; }
        public int LanguageId { get; set; }
        public List<int> GenreIds { get; set; } = new List<int>();
        public List<PariticipantRoleForSaveDto> ParticipantsRoles { get; set; } = new List<PariticipantRoleForSaveDto>();
    }    
}
