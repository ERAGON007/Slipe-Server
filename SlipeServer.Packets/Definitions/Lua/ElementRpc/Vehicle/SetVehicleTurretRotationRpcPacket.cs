﻿using SlipeServer.Packets.Builder;
using SlipeServer.Packets.Enums;
using System;
using System.Numerics;

namespace SlipeServer.Packets.Definitions.Lua.ElementRpc.Vehicle;

public class SetVehicleTurretRotationRpcPacket : Packet
{
    public override PacketId PacketId => PacketId.PACKET_ID_LUA_ELEMENT_RPC;
    public override PacketReliability Reliability => PacketReliability.ReliableSequenced;
    public override PacketPriority Priority => PacketPriority.High;

    public uint ElementId { get; set; }
    public Vector2 Rotation { get; set; }

    public SetVehicleTurretRotationRpcPacket(uint elementId, Vector2 rotation)
    {
        this.ElementId = elementId;
        this.Rotation = rotation;
    }

    public override void Read(byte[] bytes)
    {
        throw new NotSupportedException();
    }

    public override byte[] Write()
    {
        var builder = new PacketBuilder();
        builder.Write((byte)ElementRpcFunction.SET_VEHICLE_TURRET_POSITION);
        builder.WriteElementId(this.ElementId);
        builder.Write(this.Rotation);
        return builder.Build();
    }
}
