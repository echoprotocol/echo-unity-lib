using System;
using CustomTools.Extensions.Core;
using Newtonsoft.Json;


namespace Base.Data
{
    public class IdObject : SerializableObject, IDisposable, IEquatable<IdObject>
    {
        [JsonProperty("id", Order = -2)]
        public SpaceTypeId Id { get; private set; }                  // "x.x.x"

        [JsonIgnore]
        public SpaceType SpaceType => Id.SpaceType;

        public void Dispose() => GC.SuppressFinalize(this);

        public bool Equals(IdObject other) => !other.IsNull() && Id.Equals(other.Id);
    }
}