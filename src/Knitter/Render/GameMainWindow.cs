using System.Numerics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Knitter.Platform.Graphics.OpenGL4;


internal class GameMainWindow : GameWindow
{
    private BufferObject<float> Vbo;
    private BufferObject<uint> Ebo;
    private VertexArrayObject<float, uint> Vao;
    private Texture Texture;
    private Shader Shader;
    //Creating transforms for the transformations
    private Transform Transforms = new Transform();

    private readonly float[] Vertices =
    {
        //X    Y      Z     U   V
         0.5f,  0.5f, 0.0f, 1f, 0f,
         0.5f, -0.5f, 0.0f, 1f, 1f,
        -0.5f, -0.5f, 0.0f, 0f, 1f,
        -0.5f,  0.5f, 0.5f, 0f, 0f
    };

    private readonly uint[] Indices =
    {
        0, 1, 3,
        1, 2, 3
    };

    public GameMainWindow(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
    {
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
  


        Ebo = new BufferObject<uint>(Indices, BufferTarget.ElementArrayBuffer);
        Vbo = new BufferObject<float>(Vertices, BufferTarget.ArrayBuffer);
        Vao = new VertexArrayObject<float, uint>(Vbo, Ebo);

        Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
        Vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

        Shader = new Shader("shader.vert", "shader.frag");

        Texture = new Texture("knitter-logo-128.png");

        //Unlike in the transformation, because of our abstraction, order doesn't matter here.
        //Mixed transformation.
        Transforms = new Transform();
        Transforms.Position = new Vector3(-0.5f, 0.5f, 0f);
        Transforms.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 1f);
        Transforms.Scale = Vector3.One * 0.5f;
        Transforms.Scale.X = 0.1f;

    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        //Code goes here.
        Vao.Bind();
        Texture.Bind();
        Shader.Use();
        Shader.SetUniform("uTexture0", 0);
        
            //Using the transformations.
            Shader.SetUniform("uModel", Transforms.ViewMatrix);

            GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);

        SwapBuffers();
    }
    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    private unsafe void OnRender(double obj)
    {

       
    }

    private void OnClose()
    {
        Vbo.Dispose();
        Ebo.Dispose();
        Vao.Dispose();
        Shader.Dispose();
        Texture.Dispose();
    }
}
