using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MuonLab.Validation.Reports
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string assemblyPath, outputPath;

			assemblyPath = @"D:\Projects\KeepingChildrenSafe\trunk\src\KCS.Web.Desktop\bin\KCS.Web.Desktop.dll";
			outputPath = @"d:\report.html";

//			if (args.Length == 2 && !string.IsNullOrEmpty(args[0]) && !string.IsNullOrEmpty(args[1]))
//			{
//				assemblyPath = args[0];
//				outputPath = args[1];
//			}
//			else
//			{
//				Console.WriteLine("Error: Invalid arguments");
//				Console.WriteLine("Usage: MuonLab.Validation.Reports.exe <assembly-name.dll> <output-file.html>");
//				return;
//			}

			Run(getRootedPath(assemblyPath), getRootedPath(outputPath));

		}

		private static string getRootedPath(string path)
		{
			if (!Path.IsPathRooted(path))
				path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path));

			return path;
		}

		private static void Run(string fullPathToAssembly, string fullPathToOutput)
		{
			var analyser = new AssemblyAnalyser();

			var assembly = Assembly.LoadFrom(fullPathToAssembly);
			
			var report = analyser.Analyse(assembly);

			var xslt = new XPathDocument(getRootedPath("ToHtml.xslt"));

			var transform = new XslCompiledTransform();
			transform.Load(xslt);

			using (var writer = new XmlTextWriter(fullPathToOutput, Encoding.UTF8))
			{
				writer.Formatting = Formatting.Indented;
				transform.Transform(report, null, writer);
			}
		}

	}
}