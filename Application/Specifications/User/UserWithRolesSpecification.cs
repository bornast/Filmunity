using Domain.Entities;

namespace Application.Specifications
{
    public class UserWithRolesSpecification : BaseSpecification<User>
    {
        public UserWithRolesSpecification(string username) : base (x => x.Username == username)
        {
            AddInclude(x => x.Roles);
        }
    }
}
