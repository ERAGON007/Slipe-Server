﻿using SlipeServer.Packets.Builder;
using SlipeServer.Packets.Constants;
using SlipeServer.Packets.Enums;
using SlipeServer.Packets.Reader;
using SlipeServer.Packets.Structures;
using System;
using System.Numerics;

namespace SlipeServer.Packets.Definitions.Sync;

public class KeySyncPacket : Packet
{
    public override PacketId PacketId => PacketId.PACKET_ID_PLAYER_KEYSYNC;
    public override PacketReliability Reliability => PacketReliability.UnreliableSequenced;
    public override PacketPriority Priority => PacketPriority.Medium;

    public SmallKeySyncStructure SmallKeySyncStructure { get; set; } = new SmallKeySyncStructure();
    public float PlayerRotation { get; set; }
    public float CameraRotation { get; set; }
    public KeySyncFlagsStructure KeySyncFlagsStructure { get; set; } = new KeySyncFlagsStructure();


    public bool HasWeapon { get; set; }
    public ushort AmmoInClip { get; set; }
    public byte WeaponType { get; set; }
    public byte WeaponSlot { get; set; }

    public Vector3 AimOrigin { get; set; }
    public Vector3 AimDirection { get; set; }
    public float AimArm { get; set; }
    public VehicleAimDirection VehicleAimDirection { get; set; }

    public uint PlayerId { get; set; }

    public KeySyncPacket()
    {

    }

    public KeySyncPacket(SmallKeySyncStructure smallKeySyncStructure, float playerRotation, float cameraRotation, KeySyncFlagsStructure keySyncFlagsStructure)
    {
        this.SmallKeySyncStructure = smallKeySyncStructure;
        this.PlayerRotation = playerRotation;
        this.CameraRotation = cameraRotation;
        this.KeySyncFlagsStructure = keySyncFlagsStructure;
    }

    public override void Read(byte[] bytes)
    {
        var reader = new PacketReader(bytes);

        this.SmallKeySyncStructure = new SmallKeySyncStructure();
        this.SmallKeySyncStructure.Read(reader);

        this.PlayerRotation = reader.GetFloatFromBits(12, -MathF.PI, MathF.PI);
        this.CameraRotation = reader.GetFloatFromBits(12, -MathF.PI, MathF.PI);

        this.KeySyncFlagsStructure = new KeySyncFlagsStructure();
        this.KeySyncFlagsStructure.Read(reader);

        if (this.SmallKeySyncStructure.ButtonCircle || this.SmallKeySyncStructure.RightShoulder1)
        {
            this.HasWeapon = reader.GetBit();
            if (this.HasWeapon)
            {
                this.WeaponType = reader.GetByte();
                this.WeaponSlot = reader.GetWeaponSlot();

                if (WeaponConstants.WeaponsWithAmmo.Contains(this.WeaponSlot))
                {
                    this.AmmoInClip = reader.GetAmmo();

                    this.AimArm = ((reader.GetInt16()) * MathF.PI / 180f) / 90.0f;
                    this.AimOrigin = reader.GetVector3();
                    this.AimDirection = reader.GetNormalizedVector();
                    this.VehicleAimDirection = (VehicleAimDirection)reader.GetByteCapped(2);
                }
            }
        }
    }

    public override byte[] Write()
    {
        var builder = new PacketBuilder();

        builder.WriteElementId(this.PlayerId);

        this.SmallKeySyncStructure.Write(builder);

        builder.WriteFloatFromBits(this.PlayerRotation, 12, -MathF.PI, MathF.PI, true);
        builder.WriteFloatFromBits(this.CameraRotation, 12, -MathF.PI, MathF.PI, true);

        this.KeySyncFlagsStructure.Write(builder);

        if (this.SmallKeySyncStructure.ButtonCircle || this.SmallKeySyncStructure.RightShoulder1)
        {
            builder.WriteWeaponSlot(this.WeaponSlot);

            if (WeaponConstants.WeaponsWithAmmo.Contains(this.WeaponSlot))
            {
                builder.WriteAmmo(this.AmmoInClip);

                builder.Write((short)(this.AimArm * 90f * 180f / MathF.PI));
                builder.Write(this.AimOrigin);
                builder.WriteNormalizedVector(this.AimDirection);
                builder.Write((byte)this.VehicleAimDirection);
            }
        }

        if (this.KeySyncFlagsStructure.IsSyncingVehicle)
        {
            // write turret if vehicle has turret
            // write hydraulics if upgrade present
            // write shoulder buttons if in plane or heli
        }

        return builder.Build();
    }

    public override void Reset()
    {
        this.HasWeapon = false;
        this.WeaponType = 0;
        this.WeaponSlot = 0;
        this.AmmoInClip = 0;

        this.AimArm = 0;
        this.AimOrigin = Vector3.Zero;
        this.AimDirection = Vector3.Zero;
        this.VehicleAimDirection = 0;
    }
}
