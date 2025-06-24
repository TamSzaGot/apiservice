namespace ApiService.Utils;

using Microsoft.OpenApi.Readers;
using System.Reflection;

public static class ApiSpecReader
{

  public static string ReadApiVersion()
  {
    try
    {
      var assembly = Assembly.GetExecutingAssembly();
      var resourceName = assembly.GetManifestResourceNames()
          .FirstOrDefault(n => n.EndsWith("api-spec.yaml"));

      if (resourceName == null) return "0.0.0";

      using var stream = assembly.GetManifestResourceStream(resourceName);
      using var reader = new StreamReader(stream!);
      var openApiDoc = new OpenApiStreamReader().Read(reader.BaseStream, out _);
      return openApiDoc?.Info?.Version ?? "0.0.0";
    }
    catch
    {
      return "0.0.0";
    }
  }
}