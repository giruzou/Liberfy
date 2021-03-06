﻿using System.Runtime.Serialization;

namespace SocialApis.Twitter
{
    [DataContract]
    public struct IdObject
    {
        [DataMember(Name = "id")]
        public long Id { get; private set; }
    }

    [DataContract]
    public struct IdObject<T>
    {
        [DataMember(Name = "id")]
        public T Id { get; private set; }
    }
}
