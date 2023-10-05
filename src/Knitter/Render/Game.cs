using System.Numerics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Knitter.Render
{
    internal class Game : GameWindow
    {
        private BufferObject<float> Vbo;
        private BufferObject<uint> Ebo;
        private VertexArrayObject<float, uint> Vao;
        private Texture Texture;
        private Shader Shader;
        //Creating transforms for the transformations
        private Transform[] Transforms = new Transform[4];

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

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
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

        //public void Run()
        //{
        //    var options = ;
        //    options.
        //    options.Size = new Vector2D<int>(800, 600);
        //    options.Title = "LearnOpenGL with Silk.NET";
        //    window = Window.Create(options);

        //    window.Load += OnLoad;
        //    window.Render += OnRender;
        //    window.Closing += OnClose;

        //    window.Run();

        //    window.Dispose();
        //}


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
            //Translation.
            Transforms[0] = new Transform();
            Transforms[0].Position = new Vector3(0.5f, 0.5f, 0f);
            //Rotation.
            Transforms[1] = new Transform();
            Transforms[1].Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 1f);
            //Scaling.
            Transforms[2] = new Transform();
            Transforms[2].Scale = 0.5f;
            //Mixed transformation.
            Transforms[3] = new Transform();
            Transforms[3].Position = new Vector3(-0.5f, 0.5f, 0f);
            Transforms[3].Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 1f);
            Transforms[3].Scale = 0.5f;
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

            for (int i = 0; i < Transforms.Length; i++)
            {
                //Using the transformations.
                Shader.SetUniform("uModel", Transforms[i].ViewMatrix);

                GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
            }

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
}
