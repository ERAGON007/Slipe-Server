-- Generated by CSharp.lua Compiler
local System = System
System.namespace("SlipeLua.Shared.Rpc", function (namespace)
  namespace.class("AsyncRpc", function (namespace)
    local getOnClientRpcFailed, setOnClientRpcFailed, getIdentifier, setIdentifier, getRpc, setRpc, Parse, __ctor1__, 
    __ctor2__
    __ctor1__ = function (this)
    end
    __ctor2__ = function (this, identifier, rpc)
      this.Identifier = identifier
      this.Rpc = rpc
    end
    getOnClientRpcFailed, setOnClientRpcFailed = System.property("OnClientRpcFailed")
    getIdentifier, setIdentifier = System.property("Identifier")
    getRpc, setRpc = System.property("Rpc")
    Parse = function (this, value)
      this.Identifier = value.Identifier
      this.Rpc = value.Rpc
    end
    return {
      base = function (out)
        return {
          out.SlipeLua.Shared.Rpc.IRpc
        }
      end,
      OnClientRpcFailed = 0,
      getOnClientRpcFailed = getOnClientRpcFailed,
      setOnClientRpcFailed = setOnClientRpcFailed,
      getIdentifier = getIdentifier,
      setIdentifier = setIdentifier,
      getRpc = getRpc,
      setRpc = setRpc,
      Parse = Parse,
      __ctor__ = {
        __ctor1__,
        __ctor2__
      },
      __metadata__ = function (out)
        return {
          properties = {
            { "Identifier", 0x106, System.String, getIdentifier, setIdentifier },
            { "OnClientRpcFailed", 0x106, System.Int32, getOnClientRpcFailed, setOnClientRpcFailed },
            { "Rpc", 0x106, out.SlipeLua.Shared.Rpc.IRpc, getRpc, setRpc }
          },
          methods = {
            { ".ctor", 0x6, __ctor1__ },
            { ".ctor", 0x206, __ctor2__, System.String, out.SlipeLua.Shared.Rpc.IRpc },
            { "Parse", 0x106, Parse, System.Object }
          },
          class = { 0x6 }
        }
      end
    }
  end)
end)
