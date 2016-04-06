 /*
  * ----------------------------------------------------------------------------------
  * This is the "DIFFERENT-BEER-WARE LICENSE" -> (v0.1):
  * I (Chronyms / <chronyms@outlook.com>) wrote this Licence-File on the Basement of
  * "THE BEER-WARE LICENSE" (Revision 42). Based on the Sense of "THE BEER-WARE LICENSE",
  * can you do whatever you want with this stuff, as long as you didn't remove the 
  * Licence-File / Licence-Section.
  * The only condition is, that you say thank you, to the creator. You can therefore send
  * a E-Mail with some nice words, send a Letter with some words and images or buy me a
  * Beer, if we meet someday.
  * And btw., please do crazy stuff.
  * -----------------------------------------------------------------------------------
  */

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

class main
{
	static void Main(string[] args)
	{
		string filelistExist = filelist.createFilelistFile();
		if (filelistExist != null)
		{
			filelist.writeFilelist();
		}
		else
		{
			styling.WriteErrorLine("Won't write filelist because list-file doesn't exist!");
		}

		Console.WriteLine("Press any key to complete.....");
		Console.ReadLine();
	}
}

class filelist
{
	internal static void writeFilelist()
	{
		List<string> files = new List<string>();

		string basePath = getBasePath();
		String[] allfiles = Directory.GetFiles(basePath, "*.*", SearchOption.AllDirectories);
		
		foreach ( var file in allfiles){
			FileInfo info = new FileInfo(file);

			files.Add(file);
		 }

		File.WriteAllLines(basePath + "\\configuration\\filelist.txt", files, Encoding.UTF8);
	}
	internal static string createFilelistFile()
	{
		string basePath = getBasePath();

		if(File.Exists(basePath + "\\configuration\\filelist.txt"))
		{
			Console.WriteLine("Tool found filelist in {0}\\configuration\\filelist.txt", basePath);
			styling.WriteSuccessLine("Recreating the file to secure the integraty!");

			File.Delete(basePath + "\\configuration\\filelist.txt");
			File.Create(basePath + "\\configuration\\filelist.txt").Close();

			return basePath;
		}
		else if(Directory.Exists(basePath+"\\configuration\\"))
		{
			Console.WriteLine("Creating the file {0}\\configuration\\filelist.txt.", basePath);

			File.Create(basePath+"\\configuration\\filelist.txt").Close();

			return basePath;
		}
		else
		{
			styling.WriteErrorLine("It look like, you started the tool in a false directory!");

			return null;
		}
	}
	protected static string getBasePath()
	{
		string basePath = Directory.GetCurrentDirectory();
		Console.WriteLine("Tool get Directory {0} as Basepath.", basePath);

		return basePath;
	}
}

class styling
{
	public static void WriteErrorLine(string value)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(value);
		Console.ResetColor();
	}
	public static void WriteSuccessLine(string value)
	{
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine(value);
		Console.ResetColor();
	}
}