using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Tool.CreateNewMicroservice.Extensions;

namespace Tool.CreateNewMicroservice
{
    class Program
    {
        const string sourcePath = @"C:\work\marcoslima\aspnetcore-microservices-boilerplate\backend\MainProduct";
        const string destinationPath = @"C:\work\marcoslima\aspnetcore-microservices-boilerplate\backend\MicroserviceBaseProject";

        static void Main(string[] args)
        {
            //Console.Clear();
            //Console.Write("Enter the name of the new microservice:");
            //var solutionPath = GetSolutionPath();
            //var projectName = Console.ReadLine();

            CopyFolder();
            RenameFolder(destinationPath, "MainProduct", "MicroserviceBaseProject");
            RenameFile(destinationPath, "MainProduct", "MicroserviceBaseProject");
            ReplaceText(destinationPath, "MainProduct", "MicroserviceBaseProject");
            AddSharedDetails("MicroserviceBaseProject");
        }

        static void CopyFolder()
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
        }


        static void RenameFolder(string path, string source, string dest)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (DirectoryInfo si in di.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                RenameFolder(si.Parent.FullName + @"\" + si.Name, source, dest);

                string strFoldername = si.Name;
                if (strFoldername.Contains(source))
                {
                    strFoldername = strFoldername.Replace(source, dest);
                    string strFolderRoot = si.Parent.FullName + "\\" + strFoldername;

                    si.MoveTo(strFolderRoot);
                }
            }
        }

        static void RenameFile(string path, string source, string dest)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                File.Move(file.FullName, file.FullName.Replace(source, dest));
            }

            foreach (DirectoryInfo si in di.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                RenameFile(si.Parent.FullName + @"\" + si.Name, source, dest);
            }
        }

        static void ReplaceText(string path, string source, string dest)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                StreamReader reader = new StreamReader(file.FullName, Encoding.Default);
                string content = reader.ReadToEnd();
                reader.Close();

                content = Regex.Replace(content, source, dest);
                content = Regex.Replace(content, source.ToLowerFirstLetter(), dest.ToLowerFirstLetter());

                StreamWriter writer = new StreamWriter(file.FullName);
                writer.Write(content);
                writer.Close();
            }

            foreach (DirectoryInfo si in di.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                ReplaceText(si.Parent.FullName + @"\" + si.Name, source, dest);
            }
        }

        static void AddSharedDetails(string projectName)
        {
            DirectoryInfo di = new DirectoryInfo(GetSolutionPath());

            var sharedCoreFolder = Path.Combine(di.FullName, $"Shared{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}Shared.Core");
            if (!Directory.Exists(sharedCoreFolder))
            {
                throw new Exception("Could not find shared core project folder!");
            }

            var fileSharedConsts = Path.Combine(sharedCoreFolder, "SharedConsts.cs");
            if (!File.Exists(fileSharedConsts))
            {
                throw new Exception("Could not find SharedConsts file!");
            }

            var fileAppConfigurations = Path.Combine(sharedCoreFolder, $"Configuration{Path.DirectorySeparatorChar}AppConfigurations.cs");
            if (!File.Exists(fileAppConfigurations))
            {
                throw new Exception("Could not find AppConfigurations file!");
            }

            StringBuilder sbText = new StringBuilder();
            using (var reader = new StreamReader(fileSharedConsts, Encoding.Default))
            {
                var line = string.Empty;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("SharedConnectionStringName"))
                    {
                        sbText.AppendLine(line);
                        sbText.AppendLine(string.Empty);
                        sbText.AppendLine($"        public const string {projectName}ConnectionStringName = \"{projectName}\";");
                    }
                    else
                    {
                        sbText.AppendLine(line);
                    }
                }
            }

            StreamWriter writer = new StreamWriter(fileSharedConsts);
            writer.Write(sbText.ToString());
            writer.Close();

            sbText = new StringBuilder();
            using (var reader = new StreamReader(fileAppConfigurations, Encoding.Default))
            {
                var line = string.Empty;
                var firstExec = true;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("break;") && firstExec)
                    {
                        sbText.AppendLine(line);
                        sbText.AppendLine($"                case \"{projectName}.EntityFrameworkCore.{projectName}DbContext\":");
                        sbText.AppendLine($"                    connectionString = configuration.GetConnectionString(SharedConsts.{projectName}ConnectionStringName);");
                        sbText.AppendLine($"                    break;");
                        firstExec = false;
                    }
                    else
                    {
                        sbText.AppendLine(line);
                    }
                }
            }

            writer = new StreamWriter(fileAppConfigurations);
            writer.Write(sbText.ToString());
            writer.Close();
        }


        static bool FolderExist(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            return di.Exists;
        }

        static string GetSolutionPath()
        {
            var programAssemblyDirectoryPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var di = new DirectoryInfo(programAssemblyDirectoryPath);
            while (!DirectoryContains(di.FullName, "All.sln"))
            {
                if (di.Parent == null)
                {
                    throw new Exception("Could not find solution folder!");
                }

                di = di.Parent;
            }

            return di.FullName;
        }

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
