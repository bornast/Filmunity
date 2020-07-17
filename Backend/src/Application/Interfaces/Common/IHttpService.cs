using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Common
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
