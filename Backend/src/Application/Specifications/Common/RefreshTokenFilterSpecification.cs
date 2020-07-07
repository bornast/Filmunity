using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Common
{
    public class RefreshTokenFilterSpecification : BaseSpecification<RefreshToken>
    {
        public RefreshTokenFilterSpecification(string jti, string token = null, bool? isInvalidated = null, bool? isUsed = null)
            : base(x => (string.IsNullOrWhiteSpace(token) || x.Token == token) &&
            (string.IsNullOrWhiteSpace(jti) || x.JwtId == jti) &&
            (isInvalidated == null || x.Invalidated == isInvalidated) &&
            (isUsed == null || x.Used == isUsed))
        {

        }        

    }

}
