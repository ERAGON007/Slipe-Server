-- Generated by CSharp.lua Compiler
local System = System
System.namespace("SlipeLua.Client.GameWorld", function (namespace)
  --/ <summary>
  --/ Represents an ambient sound
  --/ </summary>
  namespace.enum("AmbientSound", function ()
    return {
      GunFire = 0,
      General = 1,
      __metadata__ = function (out)
        return {
          fields = {
            { "General", 0xE, System.Int32 },
            { "GunFire", 0xE, System.Int32 }
          },
          class = { 0x6 }
        }
      end
    }
  end)
end)
