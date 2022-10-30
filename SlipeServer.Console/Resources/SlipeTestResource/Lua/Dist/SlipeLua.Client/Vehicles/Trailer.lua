-- Generated by CSharp.lua Compiler
local System = System
local SlipeLuaMtaDefinitions = SlipeLua.MtaDefinitions
local SlipeLuaSharedElements = SlipeLua.Shared.Elements
local SystemNumerics = System.Numerics
local SlipeLuaClientVehicles
System.import(function (out)
  SlipeLuaClientVehicles = SlipeLua.Client.Vehicles
end)
System.namespace("SlipeLua.Client.Vehicles", function (namespace)
  --/ <summary>
  --/ Represents a towable trailer
  --/ </summary>
  namespace.class("Trailer", function (namespace)
    local getTowingVehicle, setTowingVehicle, op_Explicit1, AttachTo4, addOnAttach, removeOnAttach, addOnDetach, removeOnDetach, 
    class, __ctor1__, __ctor2__, __ctor3__
    __ctor1__ = function (this, element)
      SlipeLuaClientVehicles.BaseVehicle.__ctor__[1](this, element)
    end
    __ctor2__ = function (this, model, position)
      __ctor3__(this, model, position:__clone__(), SystemNumerics.Vector3.getZero(), "", 1, 1)
    end
    __ctor3__ = function (this, model, position, rotation, numberplate, variant1, variant2)
      SlipeLuaClientVehicles.BaseVehicle.__ctor__[2](this, model, position:__clone__(), rotation:__clone__(), numberplate, variant1, variant2)
    end
    getTowingVehicle = function (this)
      return SlipeLuaSharedElements.ElementManager.getInstance():GetElement(SlipeLuaMtaDefinitions.MtaShared.GetVehicleTowingVehicle(this.element), SlipeLuaClientVehicles.BaseVehicle)
    end
    setTowingVehicle = function (this, value)
      AttachTo4(this, value)
    end
    op_Explicit1 = function (vehicle)
      if System.is(SlipeLuaClientVehicles.VehicleModel.FromId(vehicle:getModel()), SlipeLuaClientVehicles.TrailerModel) then
        return class(vehicle:getMTAElement())
      end

      System.throw((System.InvalidCastException("The vehicle is not a trailer")))
    end
    AttachTo4 = function (this, vehicle)
      return SlipeLuaMtaDefinitions.MtaShared.AttachTrailerToVehicle(this.element, vehicle:getMTAElement())
    end
    addOnAttach, removeOnAttach = System.event("OnAttach")
    addOnDetach, removeOnDetach = System.event("OnDetach")
    class = {
      base = function (out)
        return {
          out.SlipeLua.Client.Vehicles.BaseVehicle
        }
      end,
      getTowingVehicle = getTowingVehicle,
      setTowingVehicle = setTowingVehicle,
      op_Explicit1 = op_Explicit1,
      AttachTo4 = AttachTo4,
      addOnAttach = addOnAttach,
      removeOnAttach = removeOnAttach,
      addOnDetach = addOnDetach,
      removeOnDetach = removeOnDetach,
      __ctor__ = {
        __ctor1__,
        __ctor2__,
        __ctor3__
      },
      __metadata__ = function (out)
        return {
          properties = {
            { "TowingVehicle", 0x106, out.SlipeLua.Client.Vehicles.BaseVehicle, getTowingVehicle, setTowingVehicle }
          },
          methods = {
            { ".ctor", 0x106, __ctor1__, out.SlipeLua.MtaDefinitions.MtaElement },
            { ".ctor", 0x206, __ctor2__, out.SlipeLua.Client.Vehicles.TrailerModel, System.Numerics.Vector3 },
            { ".ctor", 0x606, __ctor3__, out.SlipeLua.Client.Vehicles.TrailerModel, System.Numerics.Vector3, System.Numerics.Vector3, System.String, System.Int32, System.Int32 },
            { "AttachTo", 0x186, AttachTo4, out.SlipeLua.Client.Vehicles.BaseVehicle, System.Boolean }
          },
          class = { 0x6 }
        }
      end
    }
    return class
  end)

  --/ <summary>
  --/ Represents different trailer models
  --/ </summary>
  namespace.class("TrailerModel", function (namespace)
    local __ctor__
    __ctor__ = function (this, id)
      SlipeLuaClientVehicles.VehicleModel.__ctor__(this, id)
    end
    return {
      base = function (out)
        return {
          out.SlipeLua.Client.Vehicles.VehicleModel
        }
      end,
      __ctor__ = __ctor__,
      __metadata__ = function (out)
        return {
          methods = {
            { ".ctor", 0x104, nil, System.Int32 }
          },
          class = { 0x6 }
        }
      end
    }
  end)
end)
