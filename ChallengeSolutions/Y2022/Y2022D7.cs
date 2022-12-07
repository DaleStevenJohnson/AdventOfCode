using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Schema;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2022
{
    public class Folder
    {
        public List<Folder> Directories { get; set; }
        public List<File> Files { get; set; }
        public Folder Parent { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int MAX_SIZE = 100000;
        public bool ValidSize = true;
        public Folder(List<string> data, Folder parent)
        {
            Directories = new List<Folder>();
            Files = new List<File>();
            Name = data[1];
            Parent = parent;
            Size = 0;
        }
        public Folder? FindDirectory(string name)
        {
            if (name == "..") return Parent;

            foreach (var folder in Directories)
            {
                if (folder.Name == name) return folder;
            }
            return null;
        }
        public int CalculateSize()
        {
            if (Directories.Count == 0)
                return Size;    
            
            foreach (var child in Directories)
            {
               Size += child.CalculateSize();
            }
            return Size;
        }
        public void AddSize(int size)
        { 
            Size += size;
        }

        public void AddFile(File file)
        {
            AddSize(file.Size);
            Files.Add(file);
        }
    }

    public class File
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public File(List<string> data)
        {
            Name = data[1];
            Size = int.Parse(data[0]);
        }
    }
  
    public class Y2022D7 : ISolution
    {
       
        public List<string> TestData { get; set; }
        public List<string> PuzzleData { get; set; }
        public Y2022D7()
        {
            TestData = MyIO.ReadStringsFromFile(2022, 7, 1, true);
            PuzzleData = MyIO.ReadStringsFromFile(2022, 7, 1, false);
        }

        public Folder CreateFileSystem(List<string> data)
        {
            var root = new Folder(new List<string>() { "", "/" }, null);
            Folder currentDirectory = root;
            foreach (var command in data)
            {
                var components = command.Split(" ").ToList();
                if (components[0] == "$")
                {
                     if (components[1] == "cd")
                        if (components[2] == "/")
                            currentDirectory = root;
                        else
                            currentDirectory = currentDirectory.FindDirectory(components[2]);
                }
                else if (components[0] == "dir")
                {
                    var dir = new Folder(components, currentDirectory);
                    currentDirectory.Directories.Add(dir);
                }
                else
                {
                    var file = new File(components);
                    currentDirectory.AddFile(file);
                }
            }
            return root;
        }

        public void CalculateSizes(Folder folder)
        {
            foreach (var dir in folder.Directories)
            {
                folder.CalculateSize();
                CalculateSizes(dir);
            }
        }
        public int SumValidFolders(Folder folder)
        {
            var MAX_SIZE = 100000;
            var total = 0;
            foreach (var dir in folder.Directories)
            { 
                if (dir.Size <= MAX_SIZE)
                    total += dir.Size;
                total += SumValidFolders(dir);
            }
            return total;

        }
        public string SolvePart1(List<string> data)
        {
            
            var fileSystem = CreateFileSystem(data);
            CalculateSizes(fileSystem);
            var size = SumValidFolders(fileSystem);
            return size.ToString();
        }

        

        public string SolvePart2(List<string> data)
        {
            return "";
        }
    }
}
 