using UnityEngine;

[System.Serializable]
public class SerializableVector3
{
    float x, y, z;

    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 GetVector3()
    {
        return new Vector3(x, y, z);
    }
}
[System.Serializable]
public class SerializableTransform
{
    public SerializableVector3 Position { get; set; }
    public SerializableVector3 Rotation { get; set; }

    public SerializableTransform(Vector3 position, Vector3 rotation)
    {
        Position = new SerializableVector3(position);
        Rotation = new SerializableVector3(rotation);
    }
}

