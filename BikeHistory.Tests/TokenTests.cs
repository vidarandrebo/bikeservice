using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using BikeHistory.Services;

namespace BikeHistory.Tests
{
    public class TokenTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TokenTests(WebApplicationFactory<Startup> factory) {
            _factory = factory;
        }

        [Fact]
        public void Test1()
        {
            var tokenService = _factory.Services.GetService<ITokenAuthenticator>();
            Assert.NotNull(tokenService);
        }
        [Fact]
        public void ValidateTokenUserID()
        {
            var tokenService = _factory.Services.GetService<ITokenAuthenticator>();
            int id = 30;
            string token = tokenService.GenerateToken(id);
            int storedId = tokenService.GetUserId(token);
            Assert.Equal(id, storedId);
        }
        [Fact]
        public void ValidateTokenUserIDFail()
        {
            var tokenService = _factory.Services.GetService<ITokenAuthenticator>();
            int id = 30;
            int idFalse = 31;
            string token = tokenService.GenerateToken(id);
            int storedId = tokenService.GetUserId(token);
            Assert.NotEqual(idFalse, storedId);
        }
    }
}
