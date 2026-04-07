using System.Net;
using Moq;
using Moq.Protected;

namespace Joaoaalves.Tiny.Core.Tests.Mocks;

internal static class HttpMessageHandlerMock
{
    internal static (Mock<HttpMessageHandler> Mock, HttpClient Client) Create(
        string responseJson,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        mock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            });

        var client = new HttpClient(mock.Object)
        {
            BaseAddress = new Uri("https://api.tiny.com.br/api2/")
        };

        return (mock, client);
    }

    internal static void VerifyRequest(
        Mock<HttpMessageHandler> mock,
        Func<HttpRequestMessage, bool> predicate,
        Times times)
    {
        mock.Protected().Verify(
            "SendAsync",
            times,
            ItExpr.Is<HttpRequestMessage>(r => predicate(r)),
            ItExpr.IsAny<CancellationToken>());
    }
}
