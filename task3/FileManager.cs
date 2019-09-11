using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Task3
{
    public sealed class FileManager
    {
        public List<double> GetLines(string path)
        {
            var lineList = new List<double>();
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var textReader = new StreamReader(fileStream))
                {
                    string temp = null;
                   
                    while (!string.IsNullOrEmpty(temp = textReader.ReadLine()))
                    {
                        lineList.Add(Convert.ToDouble(Regex.Replace(temp, @"\\n", string.Empty)));
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