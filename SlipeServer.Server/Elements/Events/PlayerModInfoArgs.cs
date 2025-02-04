﻿using SlipeServer.Packets.Definitions.Lua;
using System;
using System.Collections.Generic;

namespace SlipeServer.Server.Elements.Events;

public class PlayerModInfoArgs : EventArgs
{
    public string InfoType { get; set; }
    public IEnumerable<ModInfoItem> ModInfoItems { get; set; }

    public PlayerModInfoArgs(string infoType, IEnumerable<ModInfoItem> modInfoItems)
    {
        this.InfoType = infoType;
        this.ModInfoItems = modInfoItems;
    }
}
