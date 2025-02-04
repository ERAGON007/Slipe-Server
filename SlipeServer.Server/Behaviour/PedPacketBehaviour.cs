﻿using SlipeServer.Packets.Definitions.Lua.ElementRpc.Ped;
using SlipeServer.Packets.Definitions.Ped;
using SlipeServer.Server.Elements;
using SlipeServer.Server.Elements.Enums;
using SlipeServer.Server.Elements.Events;
using SlipeServer.Server.PacketHandling.Factories;

namespace SlipeServer.Server.Behaviour;

/// <summary>
/// Behaviour responsible for handling ped events and sending corresponding packets
/// </summary>
public class PedPacketBehaviour
{
    private readonly MtaServer server;

    public PedPacketBehaviour(MtaServer server)
    {
        this.server = server;

        server.ElementCreated += HandleElementCreate;
    }

    private void HandleElementCreate(Element obj)
    {
        if (obj is Ped ped)
        {
            ped.ModelChanged += RelayModelChange;
            ped.HealthChanged += RelayHealthChange;
            ped.ArmourChanged += RelayArmourChange;
            ped.WeaponSlotChanged += RelayWeaponSlotChange;
            ped.FightingStyleChanged += RelayFightingStyleChange;
            ped.WeaponReceived += RelayPedWeaponReceive;
            ped.WeaponRemoved += RelayPedWeaponRemove;
            ped.AmmoUpdated += RelayPedAmmoCountUpdate;
            ped.JetpackStateChanged += RelayJetpackStateChanged;
            ped.StatChanged += RelayStatChanged;
            ped.Clothing.Changed += RelayClothesChanged;
            ped.AnimationStarted += RelayPedAnimationStart;
            ped.AnimationStopped += RelayPedAnimationStop;
            ped.AnimationProgressChanged += RelayPedAnimationProgress;
            ped.AnimationSpeedChanged += RelayPedAnimationSpeed;
            ped.GravityChanged += RelayPedGravityChange;
            ped.WeaponReloaded += RelayWeaponReload;

            if (ped is not Player)
            {
                ped.Wasted += RelayPedWasted;
            }
        }
    }

    private void RelayPedWasted(Ped sender, PedWastedEventArgs e)
    {
        var packet = new PedWastedPacket(
            e.Source.Id, e.Killer?.Id ?? 0, (byte)e.WeaponType, (byte)e.BodyPart, e.Ammo, false, sender.GetAndIncrementTimeContext(), e.AnimationGroup, e.AnimationId
        )
        {
            Ammo = e.Ammo
        };
        this.server.BroadcastPacket(packet);
    }

    private void RelayClothesChanged(Ped sender, ClothingChangedEventArgs args)
    {
        this.server.BroadcastPacket(PedPacketFactory.CreateClothesPacket(args.Ped, args.ClothingType, args.Current));
    }

    private void RelayJetpackStateChanged(Element sender, ElementChangedEventArgs<Ped, bool> args)
    {
        if (!args.IsSync)
            if (args.NewValue)
                this.server.BroadcastPacket(PedPacketFactory.CreateGiveJetpack(args.Source));
            else
                this.server.BroadcastPacket(PedPacketFactory.CreateRemoveJetpack(args.Source));
    }

    private void RelayModelChange(object sender, ElementChangedEventArgs<Ped, ushort> args)
    {
        if (!args.IsSync)
            this.server.BroadcastPacket(PedPacketFactory.CreateSetModelPacket(args.Source));
    }

    private void RelayHealthChange(object sender, ElementChangedEventArgs<Ped, float> args)
    {
        if (!args.IsSync)
            this.server.BroadcastPacket(PedPacketFactory.CreateSetHealthPacket(args.Source));
    }

    private void RelayArmourChange(object sender, ElementChangedEventArgs<Ped, float> args)
    {
        if (!args.IsSync)
            this.server.BroadcastPacket(PedPacketFactory.CreateSetArmourPacket(args.Source));
    }

    private void RelayWeaponSlotChange(object sender, ElementChangedEventArgs<Ped, WeaponSlot> args)
    {
        if (!args.IsSync)
            this.server.BroadcastPacket(new SetWeaponSlotRpcPacket(args.Source.Id, (byte)args.NewValue));
    }

    private void RelayFightingStyleChange(object sender, ElementChangedEventArgs<Ped, FightingStyle> args)
    {
        if (!args.IsSync)
            this.server.BroadcastPacket(new SetPedFightingStyleRpcPacket(args.Source.Id, (byte)args.NewValue));
    }

    private void RelayPedWeaponReceive(object? sender, WeaponReceivedEventArgs e)
    {
        this.server.BroadcastPacket(new GiveWeaponRpcPacket(e.Ped.Id, (byte)e.WeaponId, e.AmmoCount, e.SetAsCurrent));
    }

    private void RelayPedWeaponRemove(object? sender, WeaponRemovedEventArgs e)
    {
        this.server.BroadcastPacket(new TakeWeaponRpcPacket(e.Ped.Id, (byte)e.WeaponId, e.AmmoCount));
    }

    private void RelayPedAmmoCountUpdate(object? sender, AmmoUpdateEventArgs e)
    {
        this.server.BroadcastPacket(new SetAmmoCountRpcPacket(e.Ped.Id, (byte)e.WeaponId, e.AmmoCount, e.AmmoInClipCount));
    }

    private void RelayStatChanged(Ped sender, PedStatChangedEventArgs e)
    {
        var packet = PedPacketFactory.CreatePlayerStatsPacket(sender);
        this.server.BroadcastPacket(packet);
    }

    private void RelayPedAnimationStart(Ped sender, PedAnimationStartedEventArgs e)
    {
        this.server.BroadcastPacket(new SetPedAnimationRpcPacket(
            sender.Id,
            e.Block,
            e.Animation,
            (int)e.Time.TotalMilliseconds,
            e.Loops,
            e.UpdatesPosition,
            e.IsInteruptable,
            e.FreezesOnLastFrame,
            e.BlendTime.Milliseconds,
            e.RetainPedState));
    }

    private void RelayPedAnimationStop(Ped sender, System.EventArgs e)
    {
        this.server.BroadcastPacket(new StopPedAnimationRpcPacket(sender.Id));
    }

    private void RelayPedAnimationProgress(Ped sender, PedAnimationProgressChangedEventArgs e)
    {
        this.server.BroadcastPacket(new SetPedAnimationProgressRpcPacket(sender.Id, e.Animation, e.Progress));
    }

    private void RelayPedAnimationSpeed(Ped sender, PedAnimationSpeedChangedEventArgs e)
    {
        this.server.BroadcastPacket(new SetPedAnimationSpeedRpcPacket(sender.Id, e.Animation, e.Speed));
    }

    private void RelayPedGravityChange(Ped sender, ElementChangedEventArgs<Ped, float> args)
    {
        this.server.BroadcastPacket(new SetPedGravityRpcPacket(sender.Id, args.NewValue));
    }

    private void RelayWeaponReload(Ped sender, System.EventArgs e)
    {
        this.server.BroadcastPacket(new ReloadPedWeaponRpcPacket(sender.Id));
    }
}
