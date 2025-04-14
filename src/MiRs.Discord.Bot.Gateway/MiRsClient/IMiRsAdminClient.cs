﻿using MiRs.Discord.Bot.Domain.Entities;

namespace MiRs.Discord.Bot.Gateway.MiRsClient
{
    public interface IMiRsAdminClient
    {
        Task<IEnumerable<GuildTeam>> GetGuildTeams(ulong guildId);

        Task<IEnumerable<GuildEvent>> GetGuildEvents(ulong guildId);

        Task CreateGuildTeam(ulong guildId, string teamname);

        Task CreateGuildEvent(GuildEvent guildEvent);
    }
}
