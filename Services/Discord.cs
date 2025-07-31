using System;
using DiscordRPC;
using DiscordRPC.Logging;

namespace BookRP.Services;

public class Discord
{
    private readonly DiscordRpcClient _client = new("1380604010574381096");

    public void Initialize()
    {
        if (_client.IsInitialized) return;
        
        _client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

        _client.OnReady += (_, e) =>
        {
            Console.WriteLine("Received ready from user {0}", e.User.Username);
        };

        _client.OnPresenceUpdate += (_, e) =>
        {
            Console.WriteLine("Received presence update {0}", e.Presence);
        };
        
        _client.Initialize();
    }

    public void UpdatePresence(RichPresence presence)
    {
        if (!_client.IsInitialized) return;
        _client.SetPresence(presence);
    }

    public void RemovePresence()
    {
        if (!_client.IsInitialized) return;
        _client.ClearPresence();
    }
}