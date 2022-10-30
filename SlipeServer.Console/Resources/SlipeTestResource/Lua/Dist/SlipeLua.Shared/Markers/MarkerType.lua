-- Generated by CSharp.lua Compiler
local System = System
System.namespace("SlipeLua.Shared.Markers", function (namespace)
  --/ <summary>
  --/ Represents different types of markers
  --/ </summary>
  namespace.enum("MarkerType", function ()
    return {
      Checkpoint = 0,
      Ring = 1,
      Cylinder = 2,
      Arrow = 3,
      Corona = 4,
      __metadata__ = function (out)
        return {
          fields = {
            { "Arrow", 0xE, System.Int32 },
            { "Checkpoint", 0xE, System.Int32 },
            { "Corona", 0xE, System.Int32 },
            { "Cylinder", 0xE, System.Int32 },
            { "Ring", 0xE, System.Int32 }
          },
          class = { 0x6 }
        }
      end
    }
  end)
end)
