using LibraryApi;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests.Books
{
    public class GettingAllBooks : IClassFixture<WebTestFixture>
    {

        private readonly HttpClient Client;
        public GettingAllBooks(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        } 

        // Do we get a 200?
        [Fact]
        public async Task HasOkStatus()
        {
            var response = await Client.GetAsync("/books");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // Is it returning Json?
        [Fact]
        public async Task HasJsonResponse()
        {
            var response = await Client.GetAsync("/books");
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }
        // If we use a genre, does it filter?

        // does it have the right data?

        // does it have the genre filter?

        // does it have the correct count?



    }
}
