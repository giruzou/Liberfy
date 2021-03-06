﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SocialApis.Twitter
{
    [DataContract]
    public class MediaDescription
    {
        [DataMember(Name = "media_id")]
        public long MediaId { get; private set; }

        [DataMember(Name = "all_text")]
        public MediaDescriptionText AllText { get; private set; }

        [DataContract]
        public class MediaDescriptionText
        {
            [DataMember(Name = "text")]
            public string Text { get; private set; }
        }
    }
}
