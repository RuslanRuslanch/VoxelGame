using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace VoxelGame.Graphics
{
    public sealed class Shader
    {
        public readonly int ID;

        public Shader(string vertexPath, string fragmentPath)
        {
            ID = GL.CreateProgram();

            var vertex = CreateShader(ShaderType.VertexShader, vertexPath);
            var fragment = CreateShader(ShaderType.FragmentShader, fragmentPath);

            GL.AttachShader(ID, vertex);
            GL.AttachShader(ID, fragment);

            GL.LinkProgram(ID);

            GL.GetProgram(ID, GetProgramParameterName.LinkStatus, out int error);

            if (error != (int)All.True)
            {
                throw new Exception("Программа не слинкована!");
            }

            GL.DeleteShader(vertex);
            GL.DeleteShader(fragment);
        }

        private int CreateShader(ShaderType type, string path)
        {
            var shaderCode = File.ReadAllText(path);
            var shader = GL.CreateShader(type);

            GL.ShaderSource(shader, shaderCode);

            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int error);

            if (error != (int)All.True)
            {
                throw new Exception("Шейдер не скомпилирован!");
            }

            return shader;
        }

        public void Load(string name, ref Matrix4 matrix)
        {
            GL.ProgramUniformMatrix4(ID, GL.GetUniformLocation(ID, name), false, ref matrix);
        }

        public void Load(string name, ref Vector3 vector)
        {
            GL.ProgramUniform3(ID, GL.GetUniformLocation(ID, name), ref vector);
        }

        public void Load(string name, ref Vector2 vector)
        {
            GL.ProgramUniform2(ID, GL.GetUniformLocation(ID, name), ref vector);
        }

        public void Load(string name, int number)
        {
            GL.ProgramUniform1(ID, GL.GetUniformLocation(ID, name), number);
        }

        public int GetLocation(string name)
        {
            return GL.GetAttribLocation(ID, name);
        }

        public void Enable()
        {
            GL.UseProgram(ID);
        }

        public void Disable()
        {
            GL.UseProgram(0);
        }

        public void Delete()
        {
            GL.DeleteProgram(ID);
        }
    }
}
