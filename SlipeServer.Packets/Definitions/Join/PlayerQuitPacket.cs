﻿using SlipeServer.Packets;
using SlipeServer.Packets.Builder;
using SlipeServer.Packets.Enums;

namespace MTAServerWrapper.Packets.Outgoing.Connection;

public class PlayerQuitPacket : Packet
{
    public override PacketId PacketId => PacketId.PACKET_ID_PLAYER_QUIT;
    public override PacketReliability Reliability => PacketReliability.ReliableSequenced;
    public override PacketPriority Priority => PacketPriority.High;

    public uint PlayerId { get; }
    public byte QuitReason { get; }

    public PlayerQuitPacket()
    {

    }

    public PlayerQuitPacket(uint playerId, byte quitReason)
    {
        this.PlayerId = playerId;
        this.QuitReason = quitReason;
    }

    public override byte[] Write()
    {
        var builder = new PacketBuilder();

        builder.WriteElementId(this.PlayerId);
        builder.WriteCapped(this.QuitReason, 3);

        return builder.Build();
    }

    public override void Read(byte[] bytes)
    {

    }
}
