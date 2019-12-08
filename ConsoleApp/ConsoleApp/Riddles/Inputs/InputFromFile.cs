using System;
using System.IO;

namespace ConsoleApp.Riddles
{
    public abstract class InputFromFile<T> : IInput<T>
    {
        private string GetFilePath()
        {
            Console.Write("Enter file path: ");
            return Console.ReadLine();
        }

        protected string[] Read()
        {
            var filePath = this.GetFilePath();
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                var content = fileInfo.OpenText().ReadToEnd();
                string[] words = content.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                return words;
            }
            else
            {
                Console.WriteLine("The file '{0}' does not exist!!!", fileInfo.FullName);
                return new string[0];
            }
        }

        public abstract T GetData();
    }
}
