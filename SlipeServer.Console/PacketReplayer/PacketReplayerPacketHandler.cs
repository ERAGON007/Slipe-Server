﻿using SlipeServer.Packets.Definitions.Sync;
using SlipeServer.Packets.Enums;
using SlipeServer.Server;
using SlipeServer.Server.PacketHandling.Handlers;
using System;

namespace SlipeServer.Console.PacketReplayer;

public class KeySyncReplayerPacketHandler : IPacketHandler<KeySyncPacket>
{
    private readonly PacketReplayerService packetReplayerService;

    public PacketId PacketId => PacketId.PACKET_ID_PLAYER_KEYSYNC;

    public KeySyncReplayerPacketHandler(PacketReplayerService packetReplayerService)
    {
        this.packetReplayerService = packetReplayerService;
    }

    public void HandlePacket(IClient client, KeySyncPacket packet)
    {
        if (!this.packetReplayerService.ShouldPlayerCaptureKeySyncPackets(client.Player))
            return;

        System.IO.Directory.CreateDirectory("packetlog/keysync");
        System.IO.File.WriteAllText($"packetlog/keysync/{(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds}.packet", Newtonsoft.Json.JsonConvert.SerializeObject(packet));
    }
}

public class PureSyncReplayerPacketHandler : IPacketHandler<PlayerPureSyncPacket>
{
    private readonly PacketReplayerService packetReplayerService;

    public PacketId PacketId => PacketId.PACKET_ID_PLAYER_PURESYNC;

    public PureSyncReplayerPacketHandler(PacketReplayerService packetReplayerService)
    {
        this.packetReplayerService = packetReplayerService;
    }

    public void HandlePacket(IClient client, PlayerPureSyncPacket packet)
    {
        if (!this.packetReplayerService.ShouldPlayerCapturePureSyncPackets(client.Player))
            return;

        System.IO.Directory.CreateDirectory("packetlog/puresync");
        System.IO.File.WriteAllText($"packetlog/puresync/{(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds}.packet", Newtonsoft.Json.JsonConvert.SerializeObject(packet));
    }
}
