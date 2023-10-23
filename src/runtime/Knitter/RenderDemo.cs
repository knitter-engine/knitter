using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Numerics;
using Silk.NET.Maths;
using Knitter.Platform.Graphics.OpenGL;
using Knitter.Common.Utils;
using Silk.NET.SDL;
using Knitter.Utils;

namespace Knitter;

public class RenderDemo
{
    private GL Gl;

    private const int Width = 800;
    private const int Height = 700;

    private BufferObject<float> Vbo;
    private BufferObject<uint> Ebo;
    private VertexArrayObject<float, uint> Vao;
    private Knitter.Platform.Graphics.OpenGL.Texture Texture;
    private Knitter.Platform.Graphics.OpenGL.Shader Shader;
    //Creating transforms for the transformations
    private Transform[] Transforms = new Transform[4];

    //Setup the camera's location, and relative up and right directions
    private static Vector3 CameraPosition = new Vector3(0.0f, 0.0f, 3.0f);
    private static Vector3 CameraTarget = Vector3.Zero;
    private static Vector3 CameraDirection = Vector3.Normalize(CameraPosition - CameraTarget);
    private static Vector3 CameraRight = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, CameraDirection));
    private static Vector3 CameraUp = Vector3.Cross(CameraDirection, CameraRight);

    public RenderDemo(GL gl)
    {
        Gl = gl;
        OnLoad();
    }

    private static readonly float[] Vertices =
    {
            //X    Y      Z     U   V
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
    };

    private static readonly uint[] Indices =
    {
        0, 1, 3,
        1, 2, 3
    };

    public void OnLoad()
    {
        Ebo = new BufferObject<uint>(Indices, BufferTargetARB.ElementArrayBuffer);
        Vbo = new BufferObject<float>(Vertices, BufferTargetARB.ArrayBuffer);
        Vao = new VertexArrayObject<float, uint>(Vbo, Ebo);

        Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
        Vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

        Shader = new Knitter.Platform.Graphics.OpenGL.Shader("shader.vert", "shader.frag");

        Texture = new Knitter.Platform.Graphics.OpenGL.Texture("knitter-logo-128.png");

        ////Unlike in the transformation, because of our abstraction, order doesn't matter here.
        ////Translation.
        //Transforms[0] = new Transform();
        //Transforms[0].Position = new Vector3(0.5f, 0.5f, 0f);
        ////Rotation.
        //Transforms[1] = new Transform();
        //Transforms[1].Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 1f);
        ////Scaling.
        //Transforms[2] = new Transform();
        //Transforms[2].Scale = new Vector3(0.5f, 0.5f, 0.5f);
        ////Mixed transformation.
        //Transforms[3] = new Transform();
        //Transforms[3].Position = new Vector3(-0.5f, 0.5f, 0f);
        //Transforms[3].Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 1f);
        //Transforms[3].Scale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public unsafe void OnRender()
    {
        Gl.Enable(EnableCap.DepthTest);
        Gl.Clear((uint)ClearBufferMask.ColorBufferBit | (uint)ClearBufferMask.DepthBufferBit);

        Vao.Bind();
        Texture.Bind();
        Shader.Use();
        Shader.SetUniform("uTexture0", 0);

        //for (int i = 0; i < Transforms.Length; i++)
        //{
        //    //Using the transformations.
        //    Shader.SetUniform("uModel", Transforms[i].ViewMatrix);

        //    Gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);
        //}
        //Use elapsed time to convert to radians to allow our cube to rotate over time
        var difference = (float)(Time.MillisecondsFromStartup / 200f);

        var model = Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(difference)) * Matrix4x4.CreateRotationX(MathHelper.DegreesToRadians(difference));
        var view = Matrix4x4.CreateLookAt(CameraPosition, CameraTarget, CameraUp);
        var projection = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Width / Height, 0.1f, 100.0f);

        Shader.SetUniform("uModel", model);
        Shader.SetUniform("uView", view);
        Shader.SetUniform("uProjection", projection);

        //We're drawing with just vertices and no indicies, and it takes 36 verticies to have a six-sided textured cube
        Gl.DrawArrays(PrimitiveType.Triangles, 0, 36);//TODO: 记录Torturtile 1.5中DrawElements话三角形，需要用到Indices。DrawArrays就不用
    }

    public void OnClose()
    {
        Vbo.Dispose();
        Ebo.Dispose();
        Vao.Dispose();
        Shader.Dispose();
        Texture.Dispose();
    }
}
