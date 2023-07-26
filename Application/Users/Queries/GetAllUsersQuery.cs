using Domain.Entities;
using MediatR;
using System.Text.Json;

namespace Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
    }

    public class GetAllUserHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly HttpClient _httpClient;
        private const string Url = "https://www.valemasTest.com";

        public GetAllUserHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(Url + "/users");
            
            byte[] data = await response.Content.ReadAsByteArrayAsync();

            List<User> users = JsonSerializer.Deserialize<List<User>>(data);

            return users;
        }
    }
}
