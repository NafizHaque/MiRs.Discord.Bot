using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Gateway.MiRsClient;

namespace MiRs.Discord.Bot.MiRsClient
{
    public class MiRsUserClient : IMiRsUserClient
    {
        private readonly string _pathSegment = "RHUser";

        public async Task RegisterUser(RHUser user)
        {
            await "https://localhost:7176/v1/"
                .AppendPathSegment($"{_pathSegment}/user")
                .PostJsonAsync(user);
        }

        public async Task JoinTeam(ulong userid, ulong guildid, string teamname)
        {
            await "https://localhost:7176/v1/"
               .WithHeader("Content-Type", "application/json")
               .AppendPathSegment($"{_pathSegment}/userteam")
               .SetQueryParams(new
               {
                   userId = userid,
                   guildId = guildid,
                   teamname = teamname
               })
               .PostAsync();
        }
    }
}
