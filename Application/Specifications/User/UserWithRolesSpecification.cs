using Application.Specifications;
using System.Security.Cryptography.X509Certificates;

namespace Application.User.Specifications
{
    public class UserWithRolesSpecification : BaseSpecification<Domain.Entities.User>
    {
        public UserWithRolesSpecification(string username) : base (x => x.Username == username)
        {
            AddInclude(x => x.Roles);
        }
    }
}
