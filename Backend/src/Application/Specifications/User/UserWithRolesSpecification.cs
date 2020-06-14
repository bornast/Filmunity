using Domain.Entities;

namespace Application.Specifications
{
    public class UserWithRolesSpecification : BaseSpecification<User>
    {
        public UserWithRolesSpecification(string username) : base (x => x.Username == username)
        {
            // same as Include(Roles).ThenInclude(Role)
            AddInclude($"{nameof(User.Roles)}.{nameof(UserRole.Role)}");
        }
    }
}
