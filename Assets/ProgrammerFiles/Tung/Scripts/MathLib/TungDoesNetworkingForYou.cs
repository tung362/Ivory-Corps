using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;

//Tung's math library with networking
public class TungDoesNetworkingForyou : NetworkBehaviour
{
    public class SyncListGameObject : SyncList<GameObject>
    {
        protected override GameObject DeserializeItem(NetworkReader reader)
        {
            return reader.ReadGameObject();
        }

        protected override void SerializeItem(NetworkWriter writer, GameObject item)
        {
            writer.Write(item);
        }
    }

    public class SyncListVector3 : SyncList<Vector3>
    {
        protected override Vector3 DeserializeItem(NetworkReader reader)
        {
            return reader.ReadVector3();
        }

        protected override void SerializeItem(NetworkWriter writer, Vector3 item)
        {
            writer.Write(item);
        }
    }

    public class SyncListQuaternion : SyncList<Quaternion>
    {
        protected override Quaternion DeserializeItem(NetworkReader reader)
        {
            return reader.ReadQuaternion();
        }

        protected override void SerializeItem(NetworkWriter writer, Quaternion item)
        {
            writer.Write(item);
        }
    }

    public class SyncListColor : SyncList<Color>
    {
        protected override Color DeserializeItem(NetworkReader reader)
        {
            return reader.ReadColor();
        }

        protected override void SerializeItem(NetworkWriter writer, Color item)
        {
            writer.Write(item);
        }
    }

    public class SyncListString : SyncList<string>
    {
        protected override string DeserializeItem(NetworkReader reader)
        {
            return reader.ReadString();
        }

        protected override void SerializeItem(NetworkWriter writer, string item)
        {
            writer.Write(item);
        }
    }
}
