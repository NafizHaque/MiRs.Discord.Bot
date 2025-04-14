using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Mediator.Model.Home;
using NetCord;

namespace MiRs.Discord.Bot.Interactors.Home
{
    public class HomeInteractor : RequestHandler<HomeRequest, HomeResponse>
    {

        /// <summary>
        /// Handles the request to create a Home.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<HomeResponse> HandleRequest(HomeRequest request, HomeResponse result, CancellationToken cancellationToken)
        {   
            return result;
        }
    }
}
