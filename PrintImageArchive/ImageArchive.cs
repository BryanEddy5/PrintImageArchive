using System;
using System.IO;
using System.Linq;

namespace PrintImageArchive
{

    class Program
    {
        static void Main(string[] args)
        {
            new ImageArchive();
        }
    }
    class ImageArchive
    {
        private readonly string Dir = @"\\naa.fujikurausa.com\dfs\SPB\MFGSHARE\";
        private readonly string sourceExtension = @"\Fails\";
        private readonly string targetExtension = @"\BAckupFails\";
        private string _machineName;
        private DirectoryInfo _source;
        private DirectoryInfo _target;

        public ImageArchive()
        {
            GetMachineName();
        }

        void ArchiveImageFiles()
        {
            try
            { 

                TargetFile();
                Source();

                //Determine whether the source directory exists.
                if (!_source.Exists)
                {
                    return;
                }

                //Create directory if it does not exist
           
                if (Directory.EnumerateFiles(_source.ToString()).Any())
                {
                    if (!_target.Exists)
                    {
                        _target.Create();
                    }
                    CopyFiles(_source.ToString(), _target.ToString());
                }
                //Move files to new location
                


            }
            catch (Exception e)
            {
                Console.WriteLine(e + "for machine" + _machineName);
                throw;
            }
        }
        //Define source directory
        private void Source() => _source = new DirectoryInfo(Dir + _machineName + @sourceExtension);

        //Define target directory
        private void TargetFile()
        {
            _target = new DirectoryInfo(Dir + _machineName + targetExtension + DateTime.Now.ToString("yyyy-MM-dd"));
        }

        //Move files to new location
        private void CopyFiles(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string targetDirectory = Path.Combine(targetDir, Path.GetFileName(file));
                if (!File.Exists(targetDirectory))
                {
                    File.Move(file, targetDirectory);
                }
                else
                {
                    File.Delete(file);
                }
            }
                

        }
        //Iterate through each folder and identify possible line folders
        private void GetMachineName()
        {
            Directory.CreateDirectory(Dir);

            foreach (var folder in Directory.GetDirectories(Dir))
            {

                _machineName = Path.GetFileName(folder);
                if (_machineName.StartsWith("line"))
                {
                    ArchiveImageFiles();
                }
            }

        }

    }
}
