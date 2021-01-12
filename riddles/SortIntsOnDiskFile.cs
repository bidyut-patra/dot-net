using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Riddles
{
    public class SortIntsOnDiskFile : RiddleBase<string, string>
    {
        public override void GenerateInput()
        {
            Console.Write("Enter min value: ");
            var minValue = int.Parse(Console.ReadLine());

            Console.Write("Enter max value: ");
            var maxValue = int.Parse(Console.ReadLine());

            this.MeasureTimeStart(MethodBase.GetCurrentMethod().Name, "Create unique random number object");

            var uniqueRandom = new UniqueRandom(minValue, maxValue);

            this.MeasureTimeEnd(MethodBase.GetCurrentMethod().Name, "unique random number object creation");
            this.MeasureTimeStart(MethodBase.GetCurrentMethod().Name, "Generate file with unique random numbers");

            //this.WriteNumbersToDiskFileForFullRange(uniqueRandom);
            this.GenerateAndMergeFilesAsync(uniqueRandom);

            this.MeasureTimeEnd(MethodBase.GetCurrentMethod().Name, "writing unique random numbers");
        }

        private struct ThreadData
        {
            public string Directory { get; set; }
            public string FileName { get; set; }
            public string Extension { get; set; }
            public NumberRange Range { get; set; }
            public int RangeIndex { get; set; }
        }

        private void GenerateAndMergeFilesAsync(UniqueRandom uniqueRandom)
        {
            var inputFile = new FileInfo(this.Input);
            var inputDir = inputFile.DirectoryName;
            var fileExtn = inputFile.Extension;
            var inputFileName = inputFile.Name;
            var inputFileNameWithoutExtn = inputFileName.Substring(0, inputFileName.IndexOf('.') - 1);

            ThreadPool.SetMaxThreads(10, 10);

            for (var i = 0; i < uniqueRandom.SubRanges.Count; i++)
            {
                var subRange = uniqueRandom.SubRanges[i];
                var threadData = new ThreadData()
                {
                    Directory = inputDir,
                    FileName = inputFileNameWithoutExtn,
                    Extension = fileExtn,
                    Range = subRange,
                    RangeIndex = i
                };
                ThreadPool.QueueUserWorkItem(this.WriteNumbersToDiskFileForSubRanges, threadData);
            }

            int workerThreads, portThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out portThreads);

            while ((workerThreads < 10) || (portThreads < 10))
            {
                Thread.Sleep(10);
                ThreadPool.GetAvailableThreads(out workerThreads, out portThreads);
            }

            var inputStream = new FileStream(this.Input, FileMode.Create);
            for (var i = 0; i < uniqueRandom.SubRanges.Count; i++)
            {
                var filePath = inputDir + @"\" + inputFileNameWithoutExtn + "_" + i + fileExtn;
                var fileStream = new FileStream(filePath, FileMode.Open);
                fileStream.CopyTo(inputStream);
                inputStream.Flush();
                fileStream.Close();
                File.Delete(filePath);
            }
            inputStream.Close();
        }

        private void WriteNumbersToDiskFileForSubRanges(object state)
        {
            var threadData = (ThreadData)state;
            var subRange = threadData.Range;
            var filePath = threadData.Directory + @"\" + threadData.FileName + "_" + threadData.RangeIndex + threadData.Extension;
            var input = new StreamWriter(filePath);

            int? number = null;
            var counter = 0;

            do
            {
                number = subRange.NextUnique();
                if (number != null)
                {
                    input.Write("{0} ", (int)number);
                    counter++;

                    if ((counter % 5) == 0)
                    {
                        input.Flush();
                    }
                }
            }
            while ((number != null) && (counter < subRange.MaxValue));

            input.Close();
        }

        private void WriteNumbersToDiskFileForFullRange(UniqueRandom uniqueRandom)
        {
            var input = new StreamWriter(this.Input);

            int? number = null;
            var counter = 0;

            do
            {
                number = uniqueRandom.Next();
                if (number != null)
                {
                    input.Write("{0} ", (int)number);
                    counter++;

                    if ((counter % 5) == 0)
                    {
                        input.Flush();
                    }
                }
            }
            while ((number != null) && (counter < uniqueRandom.Range.MaxValue));

            input.Close();
        }

        private void MergeFiles()
        {

        }

        public override void Process()
        {
            this.MeasureTimeStart(MethodBase.GetCurrentMethod().Name, "Sort the numbers on the disk file");

            var input = new StreamReader(this.Input);
            input.Close();

            var memory = new int[(1024 * 1024) / 4];

            var output = new StreamWriter(this.Output);
            output.Close();

            this.MeasureTimeEnd(MethodBase.GetCurrentMethod().Name, "sorting the numbers");
        }
    }
}
