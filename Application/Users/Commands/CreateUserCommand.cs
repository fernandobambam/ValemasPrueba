using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class CreateUserCommand :IRequest<int>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly HttpClient _httpClient;
        private const string Url = "https://www.valemasTest.com";

        public CreateUserHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User createUser = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password
            };

            var userJson = JsonSerializer.Serialize(createUser);
            StringContent content = new StringContent(userJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(Url + "/users", content);

            var dataJson = await response.Content.ReadAsStringAsync();

            int data = JsonSerializer.Deserialize<int>(dataJson);

            return data;
        }
    }
}
