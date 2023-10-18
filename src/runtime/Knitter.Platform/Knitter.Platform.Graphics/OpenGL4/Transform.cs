using System.Numerics;

namespace Knitter.Platform.Graphics.OpenGL4;

public class Transform
{
    //A transform abstraction.
    //For a transform we need to have a position a scale and a rotation,
    //depending on what application you are creating, the type for these may vary.

    //Here we have chosen a vec3 for position and scale, quaternion for rotation,
    //as that is the most normal to go with.
    //Another example could have been vec3, vec3, vec4, so the rotation is an axis angle instead of a quaternion

    public Vector3 Position = Vector3.Zero;

    public Vector3 Scale = Vector3.One;

    public Quaternion Rotation = Quaternion.Identity;

    //Note: The order here does matter.
    public Matrix4x4 ViewMatrix => Matrix4x4.Identity * Matrix4x4.CreateFromQuaternion(Rotation) * Matrix4x4.CreateScale(Scale) * Matrix4x4.CreateTranslation(Position);
}
