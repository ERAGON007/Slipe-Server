﻿using SlipeServer.Packets.Definitions.Lua;
using SlipeServer.Packets.Definitions.Lua.ElementRpc.Element;
using SlipeServer.Packets.Enums;
using SlipeServer.Server.Concepts;
using SlipeServer.Server.Elements.Enums;
using SlipeServer.Server.Elements.Events;
using SlipeServer.Server.Enums;
using SlipeServer.Server.PacketHandling.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using SlipeServer.Packets.Constants;
using System.Drawing;
using SlipeServer.Packets.Lua.Event;
using SlipeServer.Server.Extensions;

namespace SlipeServer.Server.Elements;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class Player : Ped
{
    public override ElementType ElementType => ElementType.Player;

    private IClient? client;
    public IClient Client
    {
        get => this.client ?? new NullClient();
        set => this.client = value;
    }

    public Camera Camera { get; }

    private byte wantedLevel = 0;
    public byte WantedLevel
    {
        get => this.wantedLevel;
        set
        {
            var args = new ElementChangedEventArgs<Player, byte>(this, this.WantedLevel, value, this.IsSync);
            this.wantedLevel = value;
            WantedLevelChanged?.Invoke(this, args);
        }
    }

    public Element? ContactElement { get; set; }

    public Vector3 AimOrigin { get; set; }
    public Vector3 AimDirection { get; set; }

    public Vector3 CameraPosition { get; set; }
    public Vector3 CameraDirection { get; set; }
    public float CameraRotation { get; set; }

    public bool IsOnGround { get; set; }
    public bool IsDucked { get; set; }
    public bool WearsGoggles { get; set; }
    public bool HasContact { get; set; }
    public bool IsChoking { get; set; }
    public bool AkimboTargetUp { get; set; }
    public bool IsSyncingVelocity { get; set; }
    public bool IsStealthAiming { get; set; }
    public bool IsVoiceMuted { get; set; }
    public bool IsChatMuted { get; set; }
    public List<Ped> SyncingPeds { get; set; }
    public List<Vehicle> SyncingVehicles { get; set; }
    public Controls Controls { get; private set; }

    private Team? team;
    public Team? Team
    {
        get => this.team;
        set
        {
            var previousTeam = this.team;
            this.team = value;
            this.TeamChanged?.Invoke(this, new PlayerTeamChangedArgs(this, value, previousTeam));
            this.team?.Players.Add(this);
        }
    }

    public Dictionary<int, PlayerPendingScreenshot> PendingScreenshots { get; } = new();

    private readonly HashSet<Element> subscriptionElements;
    private Dictionary<string, KeyState> BoundKeys { get; }

    private int money;
    public int Money
    {
        get => this.money;
        set
        {
            int clampedMoney = Math.Clamp(value, -99999999, 99999999);
            var previousTeam = this.money;
            this.money = clampedMoney;
            this.MoneyChanged?.Invoke(this, new PlayerMoneyChangedEventArgs(this, clampedMoney, true));
        }
    }

    private string? nametagText;
    public string NametagText
    {
        get => this.nametagText ?? this.Name;
        set
        {
            var args = new ElementChangedEventArgs<Player, string>(this, this.NametagText, value, this.IsSync);
            this.nametagText = value;
            NametagTextChanged?.Invoke(this, args);
        }
    }

    private bool isNametagShowing = true;
    public bool IsNametagShowing
    {
        get => this.isNametagShowing;
        set
        {
            var args = new ElementChangedEventArgs<Player, bool>(this, this.IsNametagShowing, value, this.IsSync);
            this.isNametagShowing = value;
            IsNametagShowingChanged?.Invoke(this, args);
        }
    }

    private Color? nametagColor;
    public Color? NametagColor
    {
        get => this.nametagColor;
        set
        {
            var args = new ElementChangedEventArgs<Player, Color?>(this, this.NametagColor, value, this.IsSync);
            this.nametagColor = value;
            NametagColorChanged?.Invoke(this, args);
        }

    }

    private string DebuggerDisplay => $"{this.Name} ({this.Id})";

    public Player() : base(0, Vector3.Zero)
    {
        this.Camera = new Camera(this);
        this.subscriptionElements = new();
        this.SyncingPeds = new();
        this.SyncingVehicles = new();
        this.BoundKeys = new();
        this.Controls = new(this);

        this.Disconnected += HandleDisconnect;
    }

    private void HandleDisconnect(Player sender, PlayerQuitEventArgs e)
    {
        if (this.Vehicle != null)
            this.Vehicle.RunAsSync(() => this.Vehicle.RemovePassenger(this));
    }

    public new Player AssociateWith(MtaServer server)
    {
        return server.AssociateElement(this);
    }

    public void SubscribeTo(Element element)
    {
        if (this.IsSubscribedTo(element))
            return;

        this.subscriptionElements.Add(element);
        this.Subscribed?.Invoke(this, new PlayerSubscriptionEventArgs(this, element));
        element.AddSubscriber(this);
    }

    public void UnsubscribeFrom(Element element)
    {
        if (!this.IsSubscribedTo(element))
            return;

        this.subscriptionElements.Remove(element);
        this.UnSubscribed?.Invoke(this, new PlayerSubscriptionEventArgs(this, element));
        element.RemoveSubscriber(this);
    }

    public bool IsSubscribedTo(Element element) => this.subscriptionElements.Contains(element);

    public void Spawn(Vector3 position, float rotation, ushort model, byte interior, ushort dimension)
    {
        this.position = position;
        this.PedRotation = rotation;
        this.model = model;
        this.interior = interior;
        this.dimension = dimension;

        this.Weapons.Clear(false);
        this.Vehicle = null;
        this.Seat = null;
        this.VehicleAction = VehicleAction.None;
        this.HasJetpack = false;
        this.health = 100;
        this.armor = 0;

        this.Spawned?.Invoke(this, new PlayerSpawnedEventArgs(this));
    }

    public void ShowHudComponent(HudComponent hudComponent, bool isVisible)
    {

        this.Client.SendPacket(PlayerPacketFactory.CreateShowHudComponentPacket(hudComponent, isVisible));
    }

    public void SetFpsLimit(ushort limit)
    {
        this.Client.SendPacket(PlayerPacketFactory.CreateSetFPSLimitPacket(limit));
    }

    public void PlaySound(byte sound)
    {
        this.Client.SendPacket(PlayerPacketFactory.CreatePlaySoundPacket(sound));
    }

    public void ForceMapVisible(bool isVisible)
    {
        this.Client.SendPacket(PlayerPacketFactory.CreateForcePlayerMapPacket(isVisible));
    }

    public void SetTransferBoxVisible(bool isVisible)
    {
        this.Client.SendPacket(PlayerPacketFactory.CreateTransferBoxVisiblePacket(isVisible));
    }

    public void ToggleAllControls(bool isEnabled, bool gtaControls = true, bool mtaControls = true)
    {
        this.Client.SendPacket(PlayerPacketFactory.CreateToggleAllControlsPacket(isEnabled, gtaControls, mtaControls));
    }

    public void TriggerCommand(string command, string[] arguments)
    {
        this.CommandEntered?.Invoke(this, new PlayerCommandEventArgs(this, command, arguments));
    }

    public void TriggerDamaged(Element? damager, WeaponType damageType, BodyPart bodyPart)
    {
        this.Damaged?.Invoke(this, new PlayerDamagedEventArgs(this, damager, damageType, bodyPart));
    }

    public override void Kill(Element? damager, WeaponType damageType, BodyPart bodyPart, ulong animationGroup = 0, ulong animationId = 15)
    {
        this.RunAsSync(() =>
        {
            this.health = 0;
            this.Vehicle = null;
            this.Seat = null;
            this.VehicleAction = VehicleAction.None;
            InvokeWasted(new PedWastedEventArgs(this, damager, damageType, bodyPart, animationGroup, animationId));
        });
    }

    public void VoiceDataStart(byte[] voiceData)
    {
        if (!this.IsVoiceMuted)
            this.VoiceDataReceived?.Invoke(this, new PlayerVoiceStartArgs(this, voiceData));
    }

    public void VoiceDataEnd()
    {
        this.VoiceDataEnded?.Invoke(this, new PlayerVoiceEndArgs(this));
    }

    public void TriggerDisconnected(QuitReason reason)
    {
        if (this.Destroy())
            this.Disconnected?.Invoke(this, new PlayerQuitEventArgs(reason));        
    }

    public void TakeScreenshot(ushort width, ushort height, string tag = "", byte quality = 30, uint maxBandwith = 5000, ushort maxPacketSize = 500)
    {
        quality = Math.Clamp(quality, (byte)0, (byte)100);
        this.Client.SendPacket(ElementPacketFactory.CreateTakePlayerScreenshotPacket(width, height, tag, quality, maxBandwith, maxPacketSize, null));
    }

    public void ScreenshotEnd(int screenshotId)
    {
        var pendingScreenshot = this.PendingScreenshots[screenshotId];
        if (pendingScreenshot != null && pendingScreenshot.Stream != null)
        {
            using (var stream = pendingScreenshot.Stream)
            {
                this.ScreenshotTaken?.Invoke(this, new ScreenshotEventArgs(pendingScreenshot.Stream, pendingScreenshot.ErrorMessage, pendingScreenshot.Tag));
            }
            this.PendingScreenshots.Remove(screenshotId);
        }
    }

    public void Kick(string reason)
    {
        this.Kicked?.Invoke(this, new PlayerKickEventArgs(reason, PlayerDisconnectType.CUSTOM));
        this.Client.SendPacket(new PlayerDisconnectPacket(PlayerDisconnectType.CUSTOM, reason));
    }

    public void Kick(PlayerDisconnectType type = PlayerDisconnectType.CUSTOM)
    {
        this.Kicked?.Invoke(this, new PlayerKickEventArgs(string.Empty, type));
        this.Client.SendPacket(new PlayerDisconnectPacket(type, string.Empty));
    }

    public void TriggerSync()
    {
        this.PureSynced?.Invoke(this, EventArgs.Empty);
    }

    public void ResendModPackets()
    {
        this.Client.ResendModPackets();
    }

    public void ResendPlayerACInfo()
    {
        this.Client.ResendPlayerACInfo();
    }

    public void SetMoney(int money, bool instant = false)
    {
        int clampedMoney = Math.Clamp(money, -99999999, 99999999);
        var args = new PlayerMoneyChangedEventArgs(this, clampedMoney, instant);
        this.money = clampedMoney;
        MoneyChanged?.Invoke(this, args);
    }

    public void SetBind(KeyConstants.Controls control, KeyState keyState) => SetBind(KeyConstants.ControlToString(control), keyState);
    public void SetBind(KeyConstants.Keys key, KeyState keyState) => SetBind(KeyConstants.KeyToString(key), keyState);

    public void SetBind(string key, KeyState keyState = KeyState.Down)
    {
        if(!KeyConstants.IsValid(key))
            throw new ArgumentException($"Key '{key}' is not valid.", key);

        if (keyState == KeyState.None)
        {
            this.RemoveBind(key);
            return;
        }

        this.BoundKeys[key] = keyState;
        this.KeyBound?.Invoke(this, new PlayerBindKeyArgs(this, key, keyState));
    }

    public void RemoveBind(string key, KeyState keyState = KeyState.Down)
    {
        if (!KeyConstants.IsValid(key))
            throw new ArgumentException($"Key '{key}' is not valid.", key);

        this.BoundKeys.Remove(key);
        this.KeyUnbound?.Invoke(this, new PlayerBindKeyArgs(this, key, keyState));
    }

    public void TriggerLuaEvent(string name, Element? source = null, params LuaValue[] parameters)
    {
        new LuaEventPacket(name, (source ?? this).Id, parameters).SendTo(this);
    }

    public void TriggerPlayerACInfo(IEnumerable<byte> detectedACList, uint d3d9Size, string d3d9MD5, string D3d9SHA256)
    {
        this.AcInfoReceived?.Invoke(this, new PlayerACInfoArgs(detectedACList, d3d9Size, d3d9MD5, D3d9SHA256));
    }

    public void TriggerPlayerDiagnosticInfo(uint level, string message)
    {
        this.DiagnosticInfoReceived?.Invoke(this, new PlayerDiagnosticInfo(level, message));
    }

    public void TriggerPlayerModInfo(string infoType, IEnumerable<ModInfoItem> modInfoItems)
    {
        this.ModInfoReceived?.Invoke(this, new PlayerModInfoArgs(infoType, modInfoItems));
    }

    public void TriggerNetworkStatus(PlayerNetworkStatusType networkStatusType, uint ticks)
    {
        this.NetworkStatusReceived?.Invoke(this, new PlayerNetworkStatusArgs(networkStatusType, ticks));
    }

    public void TriggerResourceStarted(ushort netId)
    {
        this.ResourceStarted?.Invoke(this, new PlayerResourceStartedEventArgs(this, netId));
    }

    public void TriggerBoundKey(BindType bindType, KeyState keyState, string key)
    {
        this.BindExecuted?.Invoke(this, new PlayerBindExecutedEventArgs(this, bindType, keyState, key));
    }

    public void TriggerCursorClicked(byte button,
        Point position,
        Vector3 worldPosition,
        Element? element)
    {
        this.CursorClicked?.Invoke(this, new PlayerCursorClickedEventArgs(button, position, worldPosition, element));
    }

    public void TriggerJoined()
    {
        this.Joined?.Invoke(this, EventArgs.Empty);
    }

    public Blip CreateBlipFor(BlipIcon icon, ushort visibleDistance = 16000, short ordering = 0, byte size = 2, Color? color = null)
    {
        var blip = new Blip(this.position, icon, visibleDistance, ordering)
        {
            Size = size,
            Color = color ?? Color.White
        };

        void attachBlip(Element element, EventArgs args)
        {
            blip.AttachTo(this);
            this.Joined -= attachBlip;
        }

        if (this.Id == 0)
            this.Joined += attachBlip;
        else
            blip.AttachTo(this);

        return blip;
    }

    public event ElementChangedEventHandler<Player, byte>? WantedLevelChanged;
    public event ElementChangedEventHandler<Player, string>? NametagTextChanged;
    public event ElementChangedEventHandler<Player, bool>? IsNametagShowingChanged;
    public event ElementChangedEventHandler<Player, Color?>? NametagColorChanged;
    public event ElementEventHandler<Player, PlayerDamagedEventArgs>? Damaged;
    public event ElementEventHandler<Player, PlayerSpawnedEventArgs>? Spawned;
    public event ElementEventHandler<Player, PlayerCommandEventArgs>? CommandEntered;
    public event ElementEventHandler<Player, PlayerVoiceStartArgs>? VoiceDataReceived;
    public event ElementEventHandler<Player, PlayerVoiceEndArgs>? VoiceDataEnded;
    public event ElementEventHandler<Player, PlayerQuitEventArgs>? Disconnected;
    public event ElementEventHandler<Player, ScreenshotEventArgs>? ScreenshotTaken;
    public event ElementEventHandler<Player, PlayerKickEventArgs>? Kicked;
    public event ElementEventHandler<Player, PlayerSubscriptionEventArgs>? Subscribed;
    public event ElementEventHandler<Player, PlayerSubscriptionEventArgs>? UnSubscribed;
    public event ElementEventHandler<Player, EventArgs>? PureSynced;
    public event ElementEventHandler<Player, PlayerACInfoArgs>? AcInfoReceived;
    public event ElementEventHandler<Player, PlayerDiagnosticInfo>? DiagnosticInfoReceived;
    public event ElementEventHandler<Player, PlayerModInfoArgs>? ModInfoReceived;
    public event ElementEventHandler<Player, PlayerNetworkStatusArgs>? NetworkStatusReceived;
    public event ElementEventHandler<Player, PlayerTeamChangedArgs>? TeamChanged;
    public event ElementEventHandler<Player, PlayerMoneyChangedEventArgs>? MoneyChanged;
    public event ElementEventHandler<Player, PlayerBindKeyArgs>? KeyBound;
    public event ElementEventHandler<Player, PlayerBindKeyArgs>? KeyUnbound;
    public event ElementEventHandler<Player, PlayerResourceStartedEventArgs>? ResourceStarted;
    public event ElementEventHandler<Player, PlayerBindExecutedEventArgs>? BindExecuted;
    public event ElementEventHandler<Player, PlayerCursorClickedEventArgs>? CursorClicked;
    public event ElementEventHandler<Player, EventArgs>? Joined;
}
