-- Generated by CSharp.lua Compiler
local System = System
local SlipeLuaCollisionShapes
local SlipeLuaSharedElements
System.import(function (out)
  SlipeLuaCollisionShapes = SlipeLua.Shared.CollisionShapes
  SlipeLuaSharedElements = SlipeLua.Shared.Elements
end)
System.namespace("SlipeLua.Shared.Elements.Events", function (namespace)
  namespace.class("OnCollisionShapeHitEventArgs", function (namespace)
    local getCollisionShape, getIsDimensionMatching, __ctor__
    __ctor__ = function (this, colshape, matchingDimension)
      this.CollisionShape = SlipeLuaSharedElements.ElementManager.getInstance():GetElement(colshape, SlipeLuaCollisionShapes.CollisionShape)
      this.IsDimensionMatching = System.cast(System.Boolean, matchingDimension)
    end
    getCollisionShape = System.property("CollisionShape", true)
    getIsDimensionMatching = System.property("IsDimensionMatching", true)
    return {
      getCollisionShape = getCollisionShape,
      IsDimensionMatching = false,
      getIsDimensionMatching = getIsDimensionMatching,
      __ctor__ = __ctor__,
      __metadata__ = function (out)
        return {
          properties = {
            { "CollisionShape", 0x206, out.SlipeLua.Shared.CollisionShapes.CollisionShape, getCollisionShape },
            { "IsDimensionMatching", 0x206, System.Boolean, getIsDimensionMatching }
          },
          methods = {
            { ".ctor", 0x204, nil, out.SlipeLua.MtaDefinitions.MtaElement, System.Object }
          },
          class = { 0x6 }
        }
      end
    }
  end)
end)
