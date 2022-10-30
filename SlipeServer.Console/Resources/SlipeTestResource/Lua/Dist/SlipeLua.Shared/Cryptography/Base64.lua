-- Generated by CSharp.lua Compiler
local System = System
local SlipeLuaMtaDefinitions = SlipeLua.MtaDefinitions
System.namespace("SlipeLua.Shared.Cryptography", function (namespace)
  --/ <summary>
  --/ Represents static wrappers for the Base64 encoding algorithm
  --/ </summary>
  namespace.class("Base64", function (namespace)
    local Encode, Decode
    Encode = function (input)
      return SlipeLuaMtaDefinitions.MtaShared.Base64Encode(input)
    end
    Decode = function (input)
      return SlipeLuaMtaDefinitions.MtaShared.Base64Decode(input)
    end
    return {
      Encode = Encode,
      Decode = Decode,
      __metadata__ = function (out)
        return {
          methods = {
            { "Decode", 0x18E, Decode, System.String, System.String },
            { "Encode", 0x18E, Encode, System.String, System.String }
          },
          class = { 0xE }
        }
      end
    }
  end)
end)
