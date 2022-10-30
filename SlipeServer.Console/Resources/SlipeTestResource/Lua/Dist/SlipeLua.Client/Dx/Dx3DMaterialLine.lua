-- Generated by CSharp.lua Compiler
local System = System
local SlipeLuaMtaDefinitions = SlipeLua.MtaDefinitions
local SlipeLuaSharedUtilities = SlipeLua.Shared.Utilities
local SystemNumerics = System.Numerics
local SlipeLuaClientDx
System.import(function (out)
  SlipeLuaClientDx = SlipeLua.Client.Dx
end)
System.namespace("SlipeLua.Client.Dx", function (namespace)
  namespace.class("Dx3DMaterialLine", function (namespace)
    local getFaceToward, setFaceToward, getMaterial, setMaterial, Draw, internal, __ctor1__, __ctor2__, 
    __ctor3__
    internal = function (this)
      this.FaceToward = System.default(SystemNumerics.Vector3)
    end
    __ctor1__ = function (this, startPos, endPos, material, width, color, faceToward, postGUI)
      internal(this)
      SlipeLuaClientDx.Dx3DLine.__ctor__[1](this, startPos:__clone__(), endPos:__clone__(), color, width, postGUI)
      this.Material = material
      this.FaceToward = faceToward:__clone__()
    end
    __ctor2__ = function (this, startPos, endPos, material, width, color)
      __ctor1__(this, startPos:__clone__(), endPos:__clone__(), material, width, color, SystemNumerics.Vector3.getZero(), false)
    end
    __ctor3__ = function (this, startPos, endPos, material, width)
      __ctor2__(this, startPos:__clone__(), endPos:__clone__(), material, width, SlipeLuaSharedUtilities.Color.getWhite())
    end
    getFaceToward, setFaceToward = System.property("FaceToward")
    getMaterial, setMaterial = System.property("Material")
    Draw = function (this, source, eventArgs)
      if SystemNumerics.Vector3.op_Equality(this.FaceToward:__clone__(), SystemNumerics.Vector3.getZero()) then
        local default = this.Material
        if default ~= nil then
          default = default:getMaterialElement()
        end
        return SlipeLuaMtaDefinitions.MtaClient.DxDrawMaterialLine3D(this:getStartPosition().X, this:getStartPosition().Y, this:getStartPosition().Z, this:getEndPosition().X, this:getEndPosition().Y, this:getEndPosition().Z, default, this.Width, this.Color:getHex(), this.PostGUI)
      else
        local default = this.Material
        if default ~= nil then
          default = default:getMaterialElement()
        end
        return SlipeLuaMtaDefinitions.MtaClient.DxDrawMaterialLine3D(this:getStartPosition().X, this:getStartPosition().Y, this:getStartPosition().Z, this:getEndPosition().X, this:getEndPosition().Y, this:getEndPosition().Z, default, this.Width, this.Color:getHex(), this.PostGUI, this.FaceToward:__clone__().X, this.FaceToward:__clone__().Y, this.FaceToward:__clone__().Z)
      end
    end
    return {
      base = function (out)
        return {
          out.SlipeLua.Client.Dx.Dx3DLine,
          out.SlipeLua.Client.Dx.IDrawable
        }
      end,
      getFaceToward = getFaceToward,
      setFaceToward = setFaceToward,
      getMaterial = getMaterial,
      setMaterial = setMaterial,
      Draw = Draw,
      __ctor__ = {
        __ctor1__,
        __ctor2__,
        __ctor3__
      },
      __metadata__ = function (out)
        return {
          properties = {
            { "FaceToward", 0x106, System.Numerics.Vector3, getFaceToward, setFaceToward },
            { "Material", 0x106, out.SlipeLua.Client.Dx.Material, getMaterial, setMaterial }
          },
          methods = {
            { ".ctor", 0x706, __ctor1__, System.Numerics.Vector3, System.Numerics.Vector3, out.SlipeLua.Client.Dx.Material, System.Single, out.SlipeLua.Shared.Utilities.Color, System.Numerics.Vector3, System.Boolean },
            { ".ctor", 0x506, __ctor2__, System.Numerics.Vector3, System.Numerics.Vector3, out.SlipeLua.Client.Dx.Material, System.Single, out.SlipeLua.Shared.Utilities.Color },
            { ".ctor", 0x406, __ctor3__, System.Numerics.Vector3, System.Numerics.Vector3, out.SlipeLua.Client.Dx.Material, System.Single },
            { "Draw", 0x286, Draw, out.SlipeLua.Client.Elements.RootElement, out.SlipeLua.Client.Rendering.Events.OnRenderEventArgs, System.Boolean }
          },
          class = { 0x6 }
        }
      end
    }
  end)
end)
