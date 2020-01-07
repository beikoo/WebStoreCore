using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Services.Common
{
    public class DirectoryManagement
    {
        private IConfigurationRoot configuration;
        private string CurrentPath;

        public string GetFolderPath()
        {
            configuration = new ConfigurationBuilder().SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).AddJsonFile("servicesconfigurator.json").Build();
            var path = configuration.GetSection("Path").Value;
            var folder = configuration.GetSection("Folder").Value;

            CurrentPath = Path.Combine(path, folder);


            bool checkDirectory = Directory.Exists(CurrentPath);
            if (checkDirectory == false)
            {
                Directory.CreateDirectory(CurrentPath);
                var defaultNumber = configuration.GetSection("StartFolderName").Value;
                folder = CreateSubFolder(defaultNumber);
            }
            else
            {
                folder = GetLastCreatedFolder(CurrentPath);
            }
            return CurrentPath + "\\" + folder;
        }
        private string GetLastCreatedFolder(string path)
        {
            //default folder value
            DateTime lastHigh = new DateTime(1900, 1, 1);

            string lastFolderDir = "";
            string folderName = "";

            var getAllFolders = Directory.GetDirectories(path);

            //get all subfolders and find the last modified
            foreach (var folder in getAllFolders)
            {
                DirectoryInfo di = new DirectoryInfo(folder);

                DateTime created = di.LastWriteTimeUtc;
                if (created > lastHigh)
                {
                    lastHigh = created;
                    lastFolderDir = folder;
                    folderName = di.Name;
                }
            }
            //checks count of files in folder
            DirectoryInfo dir = new DirectoryInfo(lastFolderDir);
            var filesInFolder = dir.GetFiles().Count();
            var maxFiles = configuration.GetSection("FilesCount");

            if (filesInFolder == int.Parse(maxFiles.Value))
            {
                folderName = CreateSubFolder(folderName);

            }
            return folderName;
        }
        private string CreateSubFolder(string folderName)
        {
            var path = configuration.GetSection("Path").Value;
            var folder = configuration.GetSection("Folder").Value;
            int currNumber = int.Parse(folderName);
            currNumber++;
            Directory.CreateDirectory(Path.Combine(CurrentPath, currNumber.ToString()));

            return currNumber.ToString();
        }
    }
}