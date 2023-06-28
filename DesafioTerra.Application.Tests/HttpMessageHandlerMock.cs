using System.Net;

namespace DesafioTerra.Application.Tests
{
    public class HttpMessageHandlerMock : HttpMessageHandler
    {
        private readonly HttpStatusCode _code;

        public HttpMessageHandlerMock(HttpStatusCode code)
        {
            _code = code;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                StatusCode = _code,
            });
        }
    }
}