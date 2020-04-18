using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FilmParticipant
    {
        public Film Film { get; set; }
        public int FilmId { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public FilmRole FilmRole { get; set; }
        public int FilmRoleId { get; set; }
    }
}
