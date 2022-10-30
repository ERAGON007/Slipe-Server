-- Generated by CSharp.lua Compiler
local System = System
System.namespace("SlipeLua.Client.Browsers.Events", function (namespace)
  namespace.class("OnLoadFailEventArgs", function (namespace)
    local getUrl, getErrorCode, getDescription, __ctor__
    __ctor__ = function (this, url, errorCode, errorDescription)
      this.Url = System.cast(System.String, url)
      this.ErrorCode = System.cast(System.Int32, errorCode)
      this.Description = System.cast(System.String, errorDescription)
    end
    getUrl = System.property("Url", true)
    getErrorCode = System.property("ErrorCode", true)
    getDescription = System.property("Description", true)
    return {
      getUrl = getUrl,
      ErrorCode = 0,
      getErrorCode = getErrorCode,
      getDescription = getDescription,
      __ctor__ = __ctor__,
      __metadata__ = function (out)
        return {
          properties = {
            { "Description", 0x206, System.String, getDescription },
            { "ErrorCode", 0x206, System.Int32, getErrorCode },
            { "Url", 0x206, System.String, getUrl }
          },
          methods = {
            { ".ctor", 0x304, nil, System.Object, System.Object, System.Object }
          },
          class = { 0x6 }
        }
      end
    }
  end)
end)
