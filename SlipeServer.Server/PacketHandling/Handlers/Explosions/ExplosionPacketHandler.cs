﻿using SlipeServer.Packets.Definitions.Explosions;
using SlipeServer.Packets.Enums;
using SlipeServer.Server.Extensions;
using SlipeServer.Server.PacketHandling.Handlers.Middleware;
using SlipeServer.Server.ElementCollections;

namespace SlipeServer.Server.PacketHandling.Handlers.Explosions;

public class ExplosionPacketHandler : IPacketHandler<ExplosionPacket>
{
    private readonly ISyncHandlerMiddleware<ExplosionPacket> middleware;
    private readonly IElementCollection elementCollection;

    public PacketId PacketId => PacketId.PACKET_ID_EXPLOSION;

    public ExplosionPacketHandler(
        ISyncHandlerMiddleware<ExplosionPacket> middleware,
        IElementCollection elementCollection
    )
    {
        this.middleware = middleware;
        this.elementCollection = elementCollection;
    }

    public void HandlePacket(IClient client, ExplosionPacket packet)
    {
        var player = client.Player;
        packet.PlayerSource = player.Id;

        if (packet.OriginId != null)
        {
            var explosionorigin = this.elementCollection.Get(packet.OriginId.Value);
            if (explosionorigin != null)
            {
                if (explosionorigin is Elements.Vehicle vehicle)
                {
                    vehicle.BlowUp();
                }
            }
        }

        var nearbyPlayers = this.middleware.GetPlayersToSyncTo(client.Player, packet);

        packet.SendTo(nearbyPlayers);
    }
}
