using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyMobile.Internal
{
    [Serializable]
    public class StringStringSerializableDictionary : SerializableDictionary<string, string>
    {
    }

    [Serializable]
    public class StringAdIdSerializableDictionary : SerializableDictionary<string, AdId>
    {
    }

    [Serializable]
    public class Dictionary_AdPlacement_AdId : SerializableDictionary<AdPlacement, AdId>
    {
        
    }
}
