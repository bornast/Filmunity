using Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.User
{
    public class UserWithRolesFilterPaginatedSpecification : BaseSpecification<Domain.Entities.User>
    {
        public UserWithRolesFilterPaginatedSpecification(UserFilterDto userFilter)
            : base(x => (string.IsNullOrWhiteSpace(userFilter.Name)
            || x.FirstName.ToLower().Contains(userFilter.Name.ToLower())
            || x.LastName.ToLower().Contains(userFilter.Name.ToLower())
            || x.Username.ToLower().Contains(userFilter.Name.ToLower())
            || (x.FirstName + " " + x.LastName).ToLower().Contains(userFilter.Name.ToLower()))
            && (userFilter.ExcludeUserId == 0 || x.Id != userFilter.ExcludeUserId))
        {
            ApplyPaging(userFilter.Skip, userFilter.Take, userFilter.PageNumber);

            AddInclude($"{nameof(Domain.Entities.User.Roles)}.{nameof(Domain.Entities.UserRole.Role)}");
        }
    }
}
