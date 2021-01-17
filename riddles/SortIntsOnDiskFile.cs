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

            this.GenerateAndMergeFilesAsync(uniqueRandom);

            this.MeasureTimeEnd(MethodBase.GetCurrentMethod().Name, "writing unique random numbers");
        }

        private class ThreadData
        {
            public FileDetails FileDetails { get; set; }
            public NumberRange Range { get; set; }
            public int RangeIndex { get; set; }
        }

        private class FileDetails
        {
            public string Directory { get; private set; }
            public string Extension { get; private set; }
            public string FileName { get; private set; }
            public FileInfo FileInfo { get; private set; }

            public FileDetails(string fileName)
            {
                this.FileInfo = new FileInfo(fileName);
                this.Directory = this.FileInfo.DirectoryName;
                this.Extension = this.FileInfo.Extension;
                this.FileName = this.FileInfo.Name;
            }

            public string GetIntermeditateFile(int counter)
            {
                var fileNameWithoutExtn = this.FileName.Substring(0, this.FileName.IndexOf('.') - 1);
                var intermediateFilePath = this.Directory + @"\" + fileNameWithoutExtn + "_" + counter + this.Extension;
                return intermediateFilePath;
            }
        }

        private class LinkedFile
        {
            public static LinkedFile Root { get; private set; }
            public static string Path { get; private set; }
            public static int CurrentIndex { get; private set; }
            public static NumberRange CurrentRange { get; private set; }

            public LinkedFile Child { get; private set; }
            public LinkedFile Next { get; private set; }
            public int FileIndex { get; private set; }
            public int NumbersCount { get; private set; }
            public NumberRange NumberRange { get; private set; }

            private StreamWriter writer;
            private StreamWriter Writer
            {
                get
                {
                    if (this.writer == null)
                    {
                        var filePath = Path + @"\sorted_" + this.FileIndex + ".txt";
                        var fileStream = new StreamWriter(filePath);
                        writer = fileStream;
                    }
                    return writer;
                }
            }

            private FileStream reader;
            private FileStream Reader
            {
                get
                {
                    if (this.reader == null)
                    {
                        var filePath = Path + @"\sorted_" + this.FileIndex + ".txt";
                        var fileStream = new FileStream(filePath, FileMode.Open);
                        reader = fileStream;
                    }
                    return reader;
                }
            }

            public LinkedFile(int index, NumberRange range)
            {
                this.FileIndex = index;
                this.NumberRange = range;
                this.Child = null;
                this.Next = null;

                if(Root == null)
                {
                    Root = this;
                }
            }

            public static void Build(int fileCount, int minValue, int maxValue, string directory)
            {
                Path = directory;

                LinkedFile current = null;
                if (Root == null)
                {
                    CurrentIndex = 0;
                    CurrentRange = new NumberRange(minValue, maxValue);
                    Root = new LinkedFile(CurrentIndex++, CurrentRange++);
                    current = Root;
                }

                for (var i = 1; i < fileCount; i++)
                {
                    var linkedFile = new LinkedFile(CurrentIndex++, CurrentRange++);
                    current.Next = linkedFile;
                    current = current.Next;
                }
            }

            public static void Write(int[] numbers)
            {
                // distribute the numbers in file based on the bucket
                foreach(var number in numbers)
                {
                    var linkedFile = GetFile(number);
                    if(linkedFile != null)
                    {
                        linkedFile.Write(number);
                    }
                }
            }

            private void Write(int number)
            {
                this.Writer.Write("{0} ", number);
                this.NumbersCount++;
                if((this.NumbersCount % 5) == 0)
                {
                    this.Writer.Flush();
                }
            }

            public static void Save()
            {
                LinkedFile linkedFile = LinkedFile.Root;
                while(linkedFile != null)
                {
                    linkedFile.Writer.Close();
                    linkedFile = linkedFile.Next;
                }
            }

            private static LinkedFile GetFile(int number)
            {
                LinkedFile linkedFile = LinkedFile.Root;
                while((linkedFile != null) && linkedFile.NumberRange.ValueWithinRange(number) == false)
                {
                    linkedFile = linkedFile.Next;
                }
                return linkedFile;
            }

            public static void Merge(string outputFile)
            {
                var outputFileStream = new FileStream(outputFile, FileMode.Create);
                LinkedFile linkedFile = LinkedFile.Root;
                while (linkedFile != null)
                {
                    linkedFile.Reader.CopyTo(outputFileStream);
                    linkedFile.Reader.Close();
                    File.Delete(linkedFile.Reader.Name);
                    linkedFile = linkedFile.Next;
                }
                outputFileStream.Close();
            }
        }

        private void GenerateAndMergeFilesAsync(UniqueRandom uniqueRandom)
        {
            var inputFileDetails = new FileDetails(this.Input);
            this.GenerateFiles(uniqueRandom, inputFileDetails);
            this.MergeFiles(inputFileDetails, uniqueRandom.SubRanges.Count);
        }

        private void GenerateFiles(UniqueRandom uniqueRandom, FileDetails fileDetails)
        {
            ThreadPool.SetMaxThreads(10, 10);

            GenerateRandomNumbersFromRangesRandomly(uniqueRandom, fileDetails);
            //GenerateRandomNumbersFromRangesSequentially(uniqueRandom, fileDetails);

            int workerThreads, portThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out portThreads);

            while ((workerThreads < 10) || (portThreads < 10))
            {
                Thread.Sleep(10);
                ThreadPool.GetAvailableThreads(out workerThreads, out portThreads);
            }
        }

        private void GenerateRandomNumbersFromRangesRandomly(UniqueRandom uniqueRandom, FileDetails fileDetails)
        {
            var randomSubRange = new NumberRange(0, uniqueRandom.SubRanges.Count);
            var randomIndex = randomSubRange.NextUnique();
            var fileIndex = 0;
            while(randomIndex != null)
            {
                var subRange = uniqueRandom.SubRanges[(int)randomIndex];
                var threadData = new ThreadData()
                {
                    FileDetails = fileDetails,
                    Range = subRange,
                    RangeIndex = fileIndex++
                };
                ThreadPool.QueueUserWorkItem(this.WriteNumbersToDiskFileForSubRanges, threadData);
                randomIndex = randomSubRange.NextUnique();
            }
        }

        private void GenerateRandomNumbersFromRangesSequentially(UniqueRandom uniqueRandom, FileDetails fileDetails)
        {
            for (var i = 0; i < uniqueRandom.SubRanges.Count; i++)
            {
                var subRange = uniqueRandom.SubRanges[i];
                var threadData = new ThreadData()
                {
                    FileDetails = fileDetails,
                    Range = subRange,
                    RangeIndex = i
                };
                ThreadPool.QueueUserWorkItem(this.WriteNumbersToDiskFileForSubRanges, threadData);
            }
        }

        private void MergeFiles(FileDetails fileDetails, int fileCount)
        {
            var inputStream = new FileStream(this.Input, FileMode.Create);
            for (var i = 0; i < fileCount; i++)
            {
                var filePath = fileDetails.GetIntermeditateFile(i);
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
            var filePath = threadData.FileDetails.GetIntermeditateFile(threadData.RangeIndex);
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

        public override void Process()
        {
            ByteRead();
        }

        private void ByteRead()
        {
            this.MeasureTimeStart(MethodBase.GetCurrentMethod().Name, "Sort the numbers on the disk file");

            var outputFileDetails = new FileDetails(this.Output);

            // 1 MB memory available
            var memorySize = 1024 * 1024;
            var memoryLength = memorySize / 4;
            var memory = new int[memoryLength];

            var inputStream = new FileStream(this.Input, FileMode.Open);
            var dataLength = inputStream.Length;
            var numberStr = string.Empty;
            var memoryCounter = 0;

            LinkedFile.Build(500, 1, 20000, outputFileDetails.Directory);

            while (inputStream.Position < dataLength)
            {
                var byteData = inputStream.ReadByte();
                if((byteData == 32) && (numberStr != string.Empty))
                {
                    memory[memoryCounter++] = Convert.ToInt32(numberStr);
                    numberStr = string.Empty;
                }
                else
                {
                    numberStr += (char)byteData;
                }

                if((memoryCounter == memory.Length) || (inputStream.Position == dataLength))
                {
                    Array.Sort(memory);
                    LinkedFile.Write(memory);
                    memoryCounter = 0;
                }
            }

            inputStream.Close();

            LinkedFile.Save();
            LinkedFile.Merge(this.Output);

            this.MeasureTimeEnd(MethodBase.GetCurrentMethod().Name, "sorting the numbers");
        }

        private void ChunkRead()
        {
            this.MeasureTimeStart(MethodBase.GetCurrentMethod().Name, "Sort the numbers on the disk file");

            var outputFileDetails = new FileDetails(this.Output);

            var memorySize = 1024 * 1024;
            var memory = new int[memorySize / 4];

            var input = new FileStream(this.Input, FileMode.Open);
            var dataLength = input.Length;

            var bufferSize = memorySize - 10;
            int offset = 0, counter = 0;
            while (offset < dataLength)
            {
                var bufferLength = (dataLength - bufferSize) > 0 ? bufferSize : dataLength;
                var bytes = new byte[memorySize];
                var bytesRead = input.Read(bytes, offset, (int)bufferLength);
                offset += bytesRead;
                // Short loop to stop when next space is found
                while (bytes[bytesRead - 1] != 32)
                {
                    bytes[bytesRead++] = (byte)input.ReadByte();
                }

                int numberCount = 0;
                var numbers = this.GetNumbers(bytes, bytesRead, ref numberCount);
                Array.Sort(numbers);

                var filePath = outputFileDetails.GetIntermeditateFile(counter);
                var outFile = new StreamWriter(filePath);
                outFile.Close();
                counter++;
            }

            input.Close();

            var output = new StreamWriter(this.Output);
            output.Close();

            this.MeasureTimeEnd(MethodBase.GetCurrentMethod().Name, "sorting the numbers");
        }

        private int[] GetNumbers(byte[] bytes, int bytesRead, ref int numberCount)
        {
            var numbers = new int[bytesRead / 4];
            string numberStr = string.Empty;
            for(var i = 0; i < bytesRead; i++)
            {
                var byteData = bytes[i];
                if((byteData == 32) && (numberStr != string.Empty))
                {
                    numbers[numberCount++] = Convert.ToInt32(numberStr);
                    numberStr = string.Empty;
                }
                else
                {
                    numberStr += (char)byteData;
                }
            }

            return numbers;
        }
    }
}
