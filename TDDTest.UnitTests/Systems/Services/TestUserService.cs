using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TDDTest.API.Config;
using TDDTest.API.Models;
using TDDTest.API.Services;
using TDDTest.UnitTests.Fixtures;
using TDDTest.UnitTests.Helpers;

namespace TDDTest.UnitTests.Systems.Services
{
    public class TestUserService
    {

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UserService(httpClient, config);

            //Act
            await sut.GetAllUsers();

            //Assert 
            handlerMock.Protected()
                .Verify(
                "SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                
            );

        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsListOfUsers()
        {
            //Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetUpReturn404();
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            //Act
           var result =  await sut.GetAllUsers();

            //Assert 
            result.Count.Should().Be(0);

        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UserService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert 
            result.Count.Should().Be(3);

        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse, endpoint );

            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UserService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            var uri = new Uri(endpoint);

            //Assert 
            handlerMock.Protected()
                .Verify(
                "SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()

            );

        }
    }
}
