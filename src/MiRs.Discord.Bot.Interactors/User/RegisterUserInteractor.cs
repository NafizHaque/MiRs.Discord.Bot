using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Mediator.Model.Users;

namespace MiRs.Discord.Bot.Interactors.User
{
    public class RegisterUserInteractor : RequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IMiRsUserClient _mirsUserClient;
        private readonly AppSettings _appSettings;
        public RegisterUserInteractor(IMiRsUserClient mirsUserClient, IOptions<AppSettings> appSettings)
        {
            _mirsUserClient = mirsUserClient;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to get teams in guiild.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<RegisterUserResponse> HandleRequest(RegisterUserRequest request, RegisterUserResponse result, CancellationToken cancellationToken)
        {
            RHUser UserToBeCreated = new RHUser
            {
                UserId = request.UserId,
                Username = request.Username,
                Runescapename = request.RunescapeUsername,
                CreatedDate = DateTimeOffset.Now,
            };

            await _mirsUserClient.RegisterUser(UserToBeCreated);

            return result;
        }
    }
}
