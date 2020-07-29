using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Person
{
    public class PersonFilterDto : BaseFilter
    {
        public string Name { get; set; }
    }
}
