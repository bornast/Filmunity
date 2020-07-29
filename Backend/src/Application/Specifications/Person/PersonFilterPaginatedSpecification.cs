using Application.Dtos.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Person
{
    public class PersonFilterPaginatedSpecification : BaseSpecification<Domain.Entities.Person>
    {
        public PersonFilterPaginatedSpecification(PersonFilterDto personFilter)
            : base(x => string.IsNullOrWhiteSpace(personFilter.Name) 
            || (x.FirstName + " " + x.LastName).ToLower().Contains(personFilter.Name.ToLower()))
        {
            ApplyPaging(personFilter.Skip, personFilter.Take, personFilter.PageNumber);
        }
    }
}
