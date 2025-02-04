﻿using SlipeServer.Packets.Enums;
using System;

namespace SlipeServer.Server.Elements.Events;

public class VehiclePanelStateChangedArgs : EventArgs
{
    public Vehicle Vehicle { get; set; }
    public VehiclePanel Panel { get; set; }
    public VehiclePanelState State { get; set; }

    public VehiclePanelStateChangedArgs(Vehicle vehicle, VehiclePanel panel, VehiclePanelState state)
    {
        this.Vehicle = vehicle;
        this.Panel = panel;
        this.State = state;
    }
}
