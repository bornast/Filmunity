﻿using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Common
{
    public class ParticipantRoleForDetailedDto
    {
        public ParticipantWithPhotoDto Participant { get; set; }
        public RecordNameDto Role { get; set; }
    }

    public class ParticipantWithPhotoDto : IMainPhotoUploadable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }

    }
}
