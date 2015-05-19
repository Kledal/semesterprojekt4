using System.Net;
using Kentor.AuthServices.Configuration;

namespace Kentor.AuthServices.WebSso
{
    class NotFoundCommand : ICommand
    {
        public CommandResult Run(HttpRequestData request, IOptions options)
        {
            return new CommandResult()
            {
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }
    }
}
