using System;
using System.IO;
using System.Reflection;

namespace Testing
{
    internal static class Resources
    {
        internal static Stream GetResource(string name) =>
            Assembly.GetExecutingAssembly().GetManifestResourceStream($"Testing.Resources.{name}") ??
            throw new Exception($"Resource not found {name}");

        internal static string GetString(string name) => new StreamReader(GetResource(name)).ReadToEnd();

        internal static byte[] GetBytes(string name)
        {
            var stream = GetResource(name);
            using var output = new MemoryStream();
            stream.CopyTo(output);
            return output.ToArray();
        }
    }
}
