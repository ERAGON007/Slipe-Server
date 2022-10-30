-- Generated by CSharp.lua Compiler
local System = System
local SlipeLuaMtaDefinitions = SlipeLua.MtaDefinitions
local SystemNumerics = System.Numerics
local SlipeLuaSharedElements
local SlipeLuaSharedHelpers
System.import(function (out)
  SlipeLuaSharedElements = SlipeLua.Shared.Elements
  SlipeLuaSharedHelpers = SlipeLua.Shared.Helpers
end)
System.namespace("SlipeLua.Shared.GameWorld", function (namespace)
  --/ <summary>
  --/ Class representing static, 3D models in the GTA world.
  --/ </summary>
  namespace.class("SharedWorldObject", function (namespace)
    local getScale, setScale, Move, Move1, Stop, __ctor1__, __ctor2__, __ctor3__
    __ctor1__ = function (this, element)
      SlipeLuaSharedElements.PhysicalElement.__ctor__(this, element)
    end
    __ctor2__ = function (this, model, position, rotation, isLowLOD)
      __ctor1__(this, SlipeLuaMtaDefinitions.MtaShared.CreateObject(model, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, isLowLOD))
    end
    __ctor3__ = function (this, model, position)
      __ctor2__(this, model, position:__clone__(), SystemNumerics.Vector3.getZero(), false)
    end
    getScale = function (this)
      local scale = SlipeLuaMtaDefinitions.MtaShared.GetObjectScale(this.element)
      return SystemNumerics.Vector3(scale[1], scale[2], scale[3])
    end
    setScale = function (this, value)
      SlipeLuaMtaDefinitions.MtaShared.SetObjectScale(this.element, value.X, value.Y, value.Z)
    end
    Move = function (this, milliseconds, position, rotationOffset, easingFunction)
      return SlipeLuaMtaDefinitions.MtaShared.MoveObject(this.element, milliseconds, position.X, position.Y, position.Z, rotationOffset.X, rotationOffset.Y, rotationOffset.Z, easingFunction:getName(), easingFunction:getPeriod(), easingFunction:getAmplitude(), easingFunction:getOvershoot())
    end
    Move1 = function (this, milliseconds, position)
      return Move(this, milliseconds, position:__clone__(), SystemNumerics.Vector3(0, 0, 0), SlipeLuaSharedHelpers.EasingFunction.getLinear())
    end
    Stop = function (this)
      return SlipeLuaMtaDefinitions.MtaShared.StopObject(this.element)
    end
    return {
      base = function (out)
        return {
          out.SlipeLua.Shared.Elements.PhysicalElement
        }
      end,
      getScale = getScale,
      setScale = setScale,
      Move = Move,
      Move1 = Move1,
      Stop = Stop,
      __ctor__ = {
        __ctor1__,
        __ctor2__,
        __ctor3__
      },
      __metadata__ = function (out)
        return {
          properties = {
            { "Scale", 0x106, System.Numerics.Vector3, getScale, setScale }
          },
          methods = {
            { ".ctor", 0x106, __ctor1__, out.SlipeLua.MtaDefinitions.MtaElement },
            { ".ctor", 0x406, __ctor2__, System.Int32, System.Numerics.Vector3, System.Numerics.Vector3, System.Boolean },
            { ".ctor", 0x206, __ctor3__, System.Int32, System.Numerics.Vector3 },
            { "Move", 0x486, Move, System.Int32, System.Numerics.Vector3, System.Numerics.Vector3, out.SlipeLua.Shared.Helpers.EasingFunction, System.Boolean },
            { "Move", 0x286, Move1, System.Int32, System.Numerics.Vector3, System.Boolean },
            { "Stop", 0x86, Stop, System.Boolean }
          },
          class = { 0x6 }
        }
      end
    }
  end)
end)
