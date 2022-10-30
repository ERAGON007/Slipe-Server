-- Generated by CSharp.lua Compiler
local System = System
local SlipeLuaMtaDefinitions = SlipeLua.MtaDefinitions
local SlipeLuaSharedMarkers = SlipeLua.Shared.Markers
local SlipeLuaSharedUtilities = SlipeLua.Shared.Utilities
System.namespace("SlipeLua.Client.Markers", function (namespace)
  --/ <summary>
  --/ Class that represents different types of markers
  --/ </summary>
  namespace.class("Marker", function (namespace)
    local __ctor1__, __ctor2__, __ctor3__
    __ctor1__ = function (this, element)
      SlipeLuaSharedMarkers.SharedMarker.__ctor__(this, element)
    end
    __ctor2__ = function (this, position, type, color, size)
      __ctor1__(this, SlipeLuaMtaDefinitions.MtaClient.CreateMarker(position.X, position.Y, position.Z, type:EnumToString(SlipeLuaSharedMarkers.MarkerType), size, color:getR(), color:getG(), color:getB(), color:getA()))
    end
    __ctor3__ = function (this, position, type)
      __ctor2__(this, position:__clone__(), type, SlipeLuaSharedUtilities.Color.getRed(), 4)
    end
    return {
      base = function (out)
        return {
          out.SlipeLua.Shared.Markers.SharedMarker
        }
      end,
      __ctor__ = {
        __ctor1__,
        __ctor2__,
        __ctor3__
      },
      __metadata__ = function (out)
        return {
          methods = {
            { ".ctor", 0x106, __ctor1__, out.SlipeLua.MtaDefinitions.MtaElement },
            { ".ctor", 0x406, __ctor2__, System.Numerics.Vector3, System.Int32, out.SlipeLua.Shared.Utilities.Color, System.Single },
            { ".ctor", 0x206, __ctor3__, System.Numerics.Vector3, System.Int32 }
          },
          class = { 0x6, System.new(out.SlipeLua.Shared.Elements.DefaultElementClassAttribute, 2, 6 --[[ElementType.Marker]]) }
        }
      end
    }
  end)
end)
