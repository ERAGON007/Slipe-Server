﻿using SlipeServer.Packets.Definitions.Lua;
using SlipeServer.Server.ServerBuilders;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace SlipeServer.Server.Mappers;
public static class LuaMapperBuilderExtensions
{
    public static void AddLuaMapping<T>(this ServerBuilder builder, Func<T, LuaValue> mapper) where T: class
    {
        builder.AddBuildStep((x) =>
        {
            x.GetRequiredService<LuaValueMapper>().DefineMapper<T>(mapper);
        }, ServerBuildStepPriority.Low);
    }

    public static void AddStructLuaMapping<T>(this ServerBuilder builder, Func<T, LuaValue> mapper) where T: struct
    {
        builder.AddBuildStep((x) =>
        {
            x.GetRequiredService<LuaValueMapper>().DefineStructMapper<T>(mapper);
        }, ServerBuildStepPriority.Low);
    }

    public static void AddLuaMapping(this ServerBuilder builder, Type type, Func<object, LuaValue> mapper)
    {
        builder.AddBuildStep((x) =>
        {
            x.GetRequiredService<LuaValueMapper>().DefineMapper(type, mapper);
        }, ServerBuildStepPriority.Low);
    }

    public static void AddFromLuaMapping<T>(this ServerBuilder builder, Func<LuaValue, T> mapper) where T : class
    {
        builder.AddBuildStep((x) =>
        {
            x.GetRequiredService<FromLuaValueMapper>().DefineMapper<T>(mapper);
        }, ServerBuildStepPriority.Low);
    }

    public static void AddFromLuaMapping(this ServerBuilder builder, Func<LuaValue, object> mapper, Type type)
    {
        builder.AddBuildStep((x) =>
        {
            x.GetRequiredService<FromLuaValueMapper>().DefineMapper(mapper, type);
        }, ServerBuildStepPriority.Low);
    }

    public static void AddVectorMappings(this ServerBuilder builder)
    {
        builder.AddStructLuaMapping<Vector2>(x => new Dictionary<LuaValue, LuaValue>()
        {
            ["X"] = x.X,
            ["Y"] = x.Y,
        });
        builder.AddStructLuaMapping<Vector3>(x => new Dictionary<LuaValue, LuaValue>()
        {
            ["X"] = x.X,
            ["Y"] = x.Y,
            ["Z"] = x.Z,
        });
        builder.AddStructLuaMapping<Vector4>(x => new Dictionary<LuaValue, LuaValue>()
        {
            ["X"] = x.X,
            ["Y"] = x.Y,
            ["Z"] = x.Z,
            ["W"] = x.W,
        });

        builder.AddFromLuaMapping(x => new Vector2(
            (float)(x.TableValue?["X"].FloatValue ?? x.TableValue?["X"].DoubleValue ?? x.TableValue?["X"].IntegerValue ?? 0),
            (float)(x.TableValue?["Y"].FloatValue ?? x.TableValue?["Y"].DoubleValue ?? x.TableValue?["Y"].IntegerValue ?? 0)
        ), typeof(Vector2));

        builder.AddFromLuaMapping(x => new Vector3(
            (float)(x.TableValue?["X"].FloatValue ?? x.TableValue?["X"].DoubleValue ?? x.TableValue?["X"].IntegerValue ?? 0),
            (float)(x.TableValue?["Y"].FloatValue ?? x.TableValue?["Y"].DoubleValue ?? x.TableValue?["Y"].IntegerValue ?? 0),
            (float)(x.TableValue?["Z"].FloatValue ?? x.TableValue?["Z"].DoubleValue ?? x.TableValue?["Z"].IntegerValue ?? 0)
        ), typeof(Vector3));

        builder.AddFromLuaMapping(x => new Vector4(
            (float)(x.TableValue?["X"].FloatValue ?? x.TableValue?["X"].DoubleValue ?? x.TableValue?["X"].IntegerValue ?? 0),
            (float)(x.TableValue?["Y"].FloatValue ?? x.TableValue?["Y"].DoubleValue ?? x.TableValue?["Y"].IntegerValue ?? 0),
            (float)(x.TableValue?["Z"].FloatValue ?? x.TableValue?["Z"].DoubleValue ?? x.TableValue?["Z"].IntegerValue ?? 0),
            (float)(x.TableValue?["W"].FloatValue ?? x.TableValue?["W"].DoubleValue ?? x.TableValue?["W"].IntegerValue ?? 0)
        ), typeof(Vector4));
    }


    public static void AddDefaultLuaMappings(this ServerBuilder builder)
    {
        builder.AddVectorMappings();

        builder.AddStructLuaMapping<Guid>(x => x.ToString());
        builder.AddFromLuaMapping(x => Guid.Parse(x.StringValue ?? ""), typeof(Guid));
    }
}
