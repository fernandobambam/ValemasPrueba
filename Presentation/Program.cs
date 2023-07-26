using Application;
using Application.Users.Commands;
using Application.Users.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => await MainAsync(args)).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddMediatR(x => x.RegisterServicesFromAssembly(typeof(MediatoREntryPoint).Assembly))
                .AddTransient<HttpClient>()
                .BuildServiceProvider();

            IMediator mediator = serviceProvider.GetService<IMediator>();

            await mediator.Send(new GetAllUsersQuery());
            await mediator.Send(new CreateUserCommand
            {
                Email = "email@gmail.com",
                UserName = "Test",
                Password = "Test"
            });
        }
    }
}