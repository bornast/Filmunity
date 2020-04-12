using Application.Specifications;

namespace Application.User.Specifications
{
    public class UserSpecification : BaseSpecification<Domain.Entities.User>
    {
        public UserSpecification(string username) : base (x => x.Username == username)
        {

        }
    }
}
