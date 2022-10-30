-- Generated by CSharp.lua Compiler
local System = System
local SlipeLuaMtaDefinitions = SlipeLua.MtaDefinitions
local SlipeLuaSharedGameWorld = SlipeLua.Shared.GameWorld
local SystemNumerics = System.Numerics
System.namespace("SlipeLua.Client.GameWorld", function (namespace)
  --/ <summary>
  --/ Class wrapping a garage as seen in singleplayer
  --/ </summary>
  namespace.class("Garage", function (namespace)
    local getBoundingBox, getPosition, getSize, __ctor__
    __ctor__ = function (this, garage)
      SlipeLuaSharedGameWorld.SharedGarage.__ctor__(this, garage)
    end
    getBoundingBox = function (this)
      local result = SlipeLuaMtaDefinitions.MtaClient.GetGarageBoundingBox(this._garageID)
      return SystemNumerics.Vector4(result[1], result[2], result[3], result[4])
    end
    getPosition = function (this)
      local result = SlipeLuaMtaDefinitions.MtaClient.GetGaragePosition(this._garageID)
      return SystemNumerics.Vector3(result[1], result[2], result[3])
    end
    getSize = function (this)
      local result = SlipeLuaMtaDefinitions.MtaClient.GetGarageSize(this._garageID)
      return SystemNumerics.Vector3(result[1], result[2], result[3])
    end
    return {
      base = function (out)
        return {
          out.SlipeLua.Shared.GameWorld.SharedGarage
        }
      end,
      getBoundingBox = getBoundingBox,
      getPosition = getPosition,
      getSize = getSize,
      __ctor__ = __ctor__,
      __metadata__ = function (out)
        return {
          properties = {
            { "BoundingBox", 0x206, System.Numerics.Vector4, getBoundingBox },
            { "Position", 0x206, System.Numerics.Vector3, getPosition },
            { "Size", 0x206, System.Numerics.Vector3, getSize }
          },
          methods = {
            { ".ctor", 0x106, nil, System.Int32 }
          },
          class = { 0x6 }
        }
      end
    }
  end)
end)
