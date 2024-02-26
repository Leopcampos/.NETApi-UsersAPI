using Bogus;
using FluentAssertions;
using System.Net;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Tests.Helpers;

namespace UsersAPI.Tests;

public class UsersTests
{
    [Fact]
    public async Task Users_Post_Returns_Created()
    {
        //Dados enviados para a requisição
        var faker = new Faker("pt_BR");
        var request = new UserAddRequestDto
        {
            Name = faker.Person.FirstName,
            Email = faker.Internet.Email(),
            Password = "@Teste123",
            PasswordConfirm = "@Teste123"
        };

        //Serializando os dados da requisição
        var content = TestHelper.CreateContent(request);

        //Fazendo a requisição POST para a API
        var result = await TestHelper.CreateClient.PostAsync("/api/users", content);

        //Capturando e verificando o status da resposta
        result.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);

        //Capturando e verificando o conteúdo da resposta
        var response = TestHelper.ReadResponse<UserResponseDto>(result);
        response.Id.Should().NotBeEmpty();
        response.Name.Should().Be(request.Name);
        response.Email.Should().Be(request.Email);
        response.CreatedAt.Should().NotBeNull();
    }

    [Fact(Skip = "Não implementado.")]
    public void Users_Post_Returns_BadRequest()
    {
        //TODO
    }

    [Fact(Skip = "Não implementado.")]
    public void Users_Put_Returns_Ok()
    {
        //TODO
    }

    [Fact(Skip = "Não implementado.")]
    public void Users_Delete_Returns_Ok()
    {
        //TODO
    }

    [Fact(Skip = "Não implementado.")]
    public void Users_Get_Returns_Ok()
    {
        //TODO
    }
}