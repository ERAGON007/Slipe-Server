﻿using SlipeServer.Packets;
using SlipeServer.Server.Elements;
using System.Collections.Generic;
using System.Linq;

namespace SlipeServer.Server.Extensions;

public static class PacketExtensions
{
    public static void SendTo(this Packet packet, IClient client) => client.SendPacket(packet);
    public static void SendTo(this Packet packet, Player player) => player.Client.SendPacket(packet);

    public static void SendTo(this Packet packet, IEnumerable<IClient> clients)
    {
        if (!clients.Any())
            return;

        byte[] data = packet.Write();
        foreach (var client in clients.ToArray())
        {
            client.SendPacket(packet.PacketId, data, packet.Priority, packet.Reliability);
        }
    }

    public static void SendTo(this Packet packet, IEnumerable<Player> players)
    {
        if (!players.Any())
            return;

        byte[] data = packet.Write();
        foreach (var player in players.ToArray())
        {
            player.Client.SendPacket(packet.PacketId, data, packet.Priority, packet.Reliability);
        }
    }
}
