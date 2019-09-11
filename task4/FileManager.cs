using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Task4
{
    public sealed class FileManager
    {
        public List<TimeInterval> GetLines(string path)
        {
            var lineList = new List<TimeInterval>();
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var textReader = new StreamReader(fileStream))
                {
                    string temp = null;
                   
                    while (!string.IsNullOrEmpty(temp = textReader.ReadLine()))
                    {
                        var regTemp = Regex.Replace(temp, @"\\n", string.Empty);
                        lineList.Add(new TimeInterval(regTemp.Split(' ')[0], regTemp.Split(' ')[1]));
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