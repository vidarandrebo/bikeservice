using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using BikeHistory.Services;
using Xunit;

namespace BikeHistory.Tests
{
    public class HashTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public HashTests(WebApplicationFactory<Startup> factory) {
            _factory = factory;
        }
        [Fact]
        public void Test1()
        {
            var hashService = _factory.Services.GetService<IPasswordHasher>();
            Assert.NotNull(hashService);
        }
        [Fact]
        public void SaltTest() {
            var hashService = _factory.Services.GetService<IPasswordHasher>();
            string password = "asdfghjhgfdsdfghjkjhgfdsdfghj";
            string hash1 = hashService.Hash(password);
            string hash2 = hashService.Hash(password);
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void CheckHashTest() {
            var hashService = _factory.Services.GetService<IPasswordHasher>();
            string password = "fhfdshdffhuyewr vufdfd";
            string hash = hashService.Hash(password);
            Assert.True(hashService.CheckHash(password, hash));
        }
        [Fact]
        public void CheckIncorrectHashTest() {
            var hashService = _factory.Services.GetService<IPasswordHasher>();
            string password2 = "fdkjsfdkj";
            string password = "fhfdshdffhuyewr vufdfd";
            string hash = hashService.Hash(password);
            Assert.False(hashService.CheckHash(password2, hash));
        }
    }
}
