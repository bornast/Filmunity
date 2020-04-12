using Ardalis.GuardClauses;
using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Extensions
{
    public static class GuardExtensions
    {
        public static void EntityNotFound(this IGuardClause guardClause, object input, string parameterName)
        {
            if (input == null)
                throw new NotFoundException(parameterName);
        }

        public static void Unauthorized(this IGuardClause guardClause, object input)
        {            
            if (input == null || input is bool && (bool)input == false)
                throw new UnauthorizedException();
        }

    }
}
