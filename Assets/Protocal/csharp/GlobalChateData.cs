// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: GlobalChateData.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace NetGameRunning {

  /// <summary>Holder for reflection information generated from GlobalChateData.proto</summary>
  public static partial class GlobalChateDataReflection {

    #region Descriptor
    /// <summary>File descriptor for GlobalChateData.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GlobalChateDataReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVHbG9iYWxDaGF0ZURhdGEucHJvdG8SDk5ldEdhbWVSdW5uaW5nIiQKDkds",
            "b2JhbENoYXREYXRhEhIKCmNoYXRfd29yZHMYASABKAkiVwoQRW1wdHlNZXNz",
            "YWdlRGF0YRIJCgF4GAEgASgCEgkKAXkYAiABKAISCQoBehgDIAEoAhIKCgJl",
            "eBgEIAEoAhIKCgJleRgFIAEoAhIKCgJlehgGIAEoAmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::NetGameRunning.GlobalChatData), global::NetGameRunning.GlobalChatData.Parser, new[]{ "ChatWords" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::NetGameRunning.EmptyMessageData), global::NetGameRunning.EmptyMessageData.Parser, new[]{ "X", "Y", "Z", "Ex", "Ey", "Ez" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class GlobalChatData : pb::IMessage<GlobalChatData>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GlobalChatData> _parser = new pb::MessageParser<GlobalChatData>(() => new GlobalChatData());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<GlobalChatData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::NetGameRunning.GlobalChateDataReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GlobalChatData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GlobalChatData(GlobalChatData other) : this() {
      chatWords_ = other.chatWords_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GlobalChatData Clone() {
      return new GlobalChatData(this);
    }

    /// <summary>Field number for the "chat_words" field.</summary>
    public const int ChatWordsFieldNumber = 1;
    private string chatWords_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ChatWords {
      get { return chatWords_; }
      set {
        chatWords_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as GlobalChatData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(GlobalChatData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ChatWords != other.ChatWords) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (ChatWords.Length != 0) hash ^= ChatWords.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (ChatWords.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ChatWords);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (ChatWords.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ChatWords);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (ChatWords.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ChatWords);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(GlobalChatData other) {
      if (other == null) {
        return;
      }
      if (other.ChatWords.Length != 0) {
        ChatWords = other.ChatWords;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            ChatWords = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            ChatWords = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class EmptyMessageData : pb::IMessage<EmptyMessageData>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<EmptyMessageData> _parser = new pb::MessageParser<EmptyMessageData>(() => new EmptyMessageData());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<EmptyMessageData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::NetGameRunning.GlobalChateDataReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public EmptyMessageData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public EmptyMessageData(EmptyMessageData other) : this() {
      x_ = other.x_;
      y_ = other.y_;
      z_ = other.z_;
      ex_ = other.ex_;
      ey_ = other.ey_;
      ez_ = other.ez_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public EmptyMessageData Clone() {
      return new EmptyMessageData(this);
    }

    /// <summary>Field number for the "x" field.</summary>
    public const int XFieldNumber = 1;
    private float x_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float X {
      get { return x_; }
      set {
        x_ = value;
      }
    }

    /// <summary>Field number for the "y" field.</summary>
    public const int YFieldNumber = 2;
    private float y_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float Y {
      get { return y_; }
      set {
        y_ = value;
      }
    }

    /// <summary>Field number for the "z" field.</summary>
    public const int ZFieldNumber = 3;
    private float z_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float Z {
      get { return z_; }
      set {
        z_ = value;
      }
    }

    /// <summary>Field number for the "ex" field.</summary>
    public const int ExFieldNumber = 4;
    private float ex_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float Ex {
      get { return ex_; }
      set {
        ex_ = value;
      }
    }

    /// <summary>Field number for the "ey" field.</summary>
    public const int EyFieldNumber = 5;
    private float ey_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float Ey {
      get { return ey_; }
      set {
        ey_ = value;
      }
    }

    /// <summary>Field number for the "ez" field.</summary>
    public const int EzFieldNumber = 6;
    private float ez_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float Ez {
      get { return ez_; }
      set {
        ez_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as EmptyMessageData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(EmptyMessageData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(X, other.X)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Y, other.Y)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Z, other.Z)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Ex, other.Ex)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Ey, other.Ey)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Ez, other.Ez)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (X != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(X);
      if (Y != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Y);
      if (Z != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Z);
      if (Ex != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Ex);
      if (Ey != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Ey);
      if (Ez != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Ez);
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (X != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(Y);
      }
      if (Z != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Z);
      }
      if (Ex != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Ex);
      }
      if (Ey != 0F) {
        output.WriteRawTag(45);
        output.WriteFloat(Ey);
      }
      if (Ez != 0F) {
        output.WriteRawTag(53);
        output.WriteFloat(Ez);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (X != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(Y);
      }
      if (Z != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Z);
      }
      if (Ex != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Ex);
      }
      if (Ey != 0F) {
        output.WriteRawTag(45);
        output.WriteFloat(Ey);
      }
      if (Ez != 0F) {
        output.WriteRawTag(53);
        output.WriteFloat(Ez);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (X != 0F) {
        size += 1 + 4;
      }
      if (Y != 0F) {
        size += 1 + 4;
      }
      if (Z != 0F) {
        size += 1 + 4;
      }
      if (Ex != 0F) {
        size += 1 + 4;
      }
      if (Ey != 0F) {
        size += 1 + 4;
      }
      if (Ez != 0F) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(EmptyMessageData other) {
      if (other == null) {
        return;
      }
      if (other.X != 0F) {
        X = other.X;
      }
      if (other.Y != 0F) {
        Y = other.Y;
      }
      if (other.Z != 0F) {
        Z = other.Z;
      }
      if (other.Ex != 0F) {
        Ex = other.Ex;
      }
      if (other.Ey != 0F) {
        Ey = other.Ey;
      }
      if (other.Ez != 0F) {
        Ez = other.Ez;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 13: {
            X = input.ReadFloat();
            break;
          }
          case 21: {
            Y = input.ReadFloat();
            break;
          }
          case 29: {
            Z = input.ReadFloat();
            break;
          }
          case 37: {
            Ex = input.ReadFloat();
            break;
          }
          case 45: {
            Ey = input.ReadFloat();
            break;
          }
          case 53: {
            Ez = input.ReadFloat();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 13: {
            X = input.ReadFloat();
            break;
          }
          case 21: {
            Y = input.ReadFloat();
            break;
          }
          case 29: {
            Z = input.ReadFloat();
            break;
          }
          case 37: {
            Ex = input.ReadFloat();
            break;
          }
          case 45: {
            Ey = input.ReadFloat();
            break;
          }
          case 53: {
            Ez = input.ReadFloat();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
