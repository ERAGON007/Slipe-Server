-- Generated by CSharp.lua Compiler
local System = System
local SlipeLuaMtaDefinitions = SlipeLua.MtaDefinitions
local SlipeLuaSharedGameWorld = SlipeLua.Shared.GameWorld
local SystemNumerics = System.Numerics
System.namespace("SlipeLua.Client.GameWorld", function (namespace)
  --/ <summary>
  --/ Class representing an object in the GTA world
  --/ </summary>
  namespace.class("WorldObject", function (namespace)
    local getMass, setMass, getBreakable, setBreakable, setRespawns, getTurnMass, setTurnMass, getAccuracy, 
    setAccuracy, getAirResistance, setAirResistance, getElasticity, setElasticity, getBuoyancy, setBuoyancy, getCenterOfMass, 
    setCenterOfMass, Break, Respawn, addOnBreak, removeOnBreak, addOnDamage, removeOnDamage, addOnExplosion, 
    removeOnExplosion, __ctor1__, __ctor2__, __ctor3__
    __ctor1__ = function (this, element)
      SlipeLuaSharedGameWorld.SharedWorldObject.__ctor__[1](this, element)
    end
    __ctor2__ = function (this, model, position)
      SlipeLuaSharedGameWorld.SharedWorldObject.__ctor__[3](this, model, position)
    end
    __ctor3__ = function (this, model, position, rotation, isLowLOD)
      SlipeLuaSharedGameWorld.SharedWorldObject.__ctor__[2](this, model, position, rotation, isLowLOD)
    end
    getMass = function (this)
      return SlipeLuaMtaDefinitions.MtaClient.GetObjectMass(this.element)
    end
    setMass = function (this, value)
      SlipeLuaMtaDefinitions.MtaClient.SetObjectMass(this.element, value)
    end
    getBreakable = function (this)
      return SlipeLuaMtaDefinitions.MtaClient.IsObjectBreakable(this.element)
    end
    setBreakable = function (this, value)
      SlipeLuaMtaDefinitions.MtaClient.SetObjectBreakable(this.element, value)
    end
    setRespawns = function (this, value)
      SlipeLuaMtaDefinitions.MtaClient.ToggleObjectRespawn(this.element, value)
    end
    getTurnMass = function (this)
      return SlipeLuaMtaDefinitions.MtaClient.GetObjectProperty(this.element, "turn_mass")
    end
    setTurnMass = function (this, value)
      SlipeLuaMtaDefinitions.MtaClient.SetObjectProperty(this.element, "turn_mass", value)
    end
    getAccuracy = function (this)
      return SlipeLuaMtaDefinitions.MtaClient.GetObjectProperty(this.element, "accuracy")
    end
    setAccuracy = function (this, value)
      SlipeLuaMtaDefinitions.MtaClient.SetObjectProperty(this.element, "accuracy", value)
    end
    getAirResistance = function (this)
      return SlipeLuaMtaDefinitions.MtaClient.GetObjectProperty(this.element, "air_resistance")
    end
    setAirResistance = function (this, value)
      SlipeLuaMtaDefinitions.MtaClient.SetObjectProperty(this.element, "air_resistance", value)
    end
    getElasticity = function (this)
      return SlipeLuaMtaDefinitions.MtaClient.GetObjectProperty(this.element, "elasticity")
    end
    setElasticity = function (this, value)
      SlipeLuaMtaDefinitions.MtaClient.SetObjectProperty(this.element, "elasticity", value)
    end
    getBuoyancy = function (this)
      return SlipeLuaMtaDefinitions.MtaClient.GetObjectProperty(this.element, "buoyancy")
    end
    setBuoyancy = function (this, value)
      SlipeLuaMtaDefinitions.MtaClient.SetObjectProperty(this.element, "buoyancy", value)
    end
    getCenterOfMass = function (this)
      local r = SlipeLuaMtaDefinitions.MtaClient.GetObjectProperty(this.element, "center_of_mass")
      return SystemNumerics.Vector3(r[1], r[2], r[3])
    end
    setCenterOfMass = function (this, value)
      SlipeLuaMtaDefinitions.MtaClient.SetObjectProperty(this.element, "center_of_mass", System.Tuple(value.X, value.Y, value.Z))
    end
    Break = function (this)
      SlipeLuaMtaDefinitions.MtaClient.BreakObject(this.element)
    end
    Respawn = function (this)
      SlipeLuaMtaDefinitions.MtaClient.RespawnObject(this.element)
    end
    addOnBreak, removeOnBreak = System.event("OnBreak")
    addOnDamage, removeOnDamage = System.event("OnDamage")
    addOnExplosion, removeOnExplosion = System.event("OnExplosion")
    return {
      base = function (out)
        return {
          out.SlipeLua.Shared.GameWorld.SharedWorldObject
        }
      end,
      getMass = getMass,
      setMass = setMass,
      getBreakable = getBreakable,
      setBreakable = setBreakable,
      setRespawns = setRespawns,
      getTurnMass = getTurnMass,
      setTurnMass = setTurnMass,
      getAccuracy = getAccuracy,
      setAccuracy = setAccuracy,
      getAirResistance = getAirResistance,
      setAirResistance = setAirResistance,
      getElasticity = getElasticity,
      setElasticity = setElasticity,
      getBuoyancy = getBuoyancy,
      setBuoyancy = setBuoyancy,
      getCenterOfMass = getCenterOfMass,
      setCenterOfMass = setCenterOfMass,
      Break = Break,
      Respawn = Respawn,
      addOnBreak = addOnBreak,
      removeOnBreak = removeOnBreak,
      addOnDamage = addOnDamage,
      removeOnDamage = removeOnDamage,
      addOnExplosion = addOnExplosion,
      removeOnExplosion = removeOnExplosion,
      __ctor__ = {
        __ctor1__,
        __ctor2__,
        __ctor3__
      },
      __metadata__ = function (out)
        return {
          properties = {
            { "Accuracy", 0x106, System.Single, getAccuracy, setAccuracy },
            { "AirResistance", 0x106, System.Single, getAirResistance, setAirResistance },
            { "Breakable", 0x106, System.Boolean, getBreakable, setBreakable },
            { "Buoyancy", 0x106, System.Single, getBuoyancy, setBuoyancy },
            { "CenterOfMass", 0x106, System.Numerics.Vector3, getCenterOfMass, setCenterOfMass },
            { "Elasticity", 0x106, System.Single, getElasticity, setElasticity },
            { "Mass", 0x106, System.Single, getMass, setMass },
            { "Respawns", 0x306, System.Boolean, setRespawns },
            { "TurnMass", 0x106, System.Single, getTurnMass, setTurnMass }
          },
          methods = {
            { ".ctor", 0x106, __ctor1__, out.SlipeLua.MtaDefinitions.MtaElement },
            { ".ctor", 0x206, __ctor2__, System.Int32, System.Numerics.Vector3 },
            { ".ctor", 0x406, __ctor3__, System.Int32, System.Numerics.Vector3, System.Numerics.Vector3, System.Boolean },
            { "Break", 0x6, Break },
            { "Respawn", 0x6, Respawn }
          },
          class = { 0x6, System.new(out.SlipeLua.Shared.Elements.DefaultElementClassAttribute, 2, 4 --[[ElementType.Object]]) }
        }
      end
    }
  end)
end)
