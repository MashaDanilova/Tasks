using System;
using System.IO;
using System.Collections.Generic;

namespace TestTask
{
    public sealed class FileManager
    {
        public List<short> GetLines(string path)
        {
            var lineList = new List<short>();
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var textReader = new StreamReader(fileStream))
                {
                    string temp = null;
                   
                    while (!string.IsNullOrEmpty(temp = textReader.ReadLine()))
                    {
                        lineList.Add(Convert.ToInt16(temp));
                    }
                }
                return lineList;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}