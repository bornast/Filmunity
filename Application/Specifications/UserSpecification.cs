using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string username) : base (x => x.Username == username)
        {

        }
    }
}
