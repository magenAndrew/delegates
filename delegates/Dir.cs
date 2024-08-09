namespace Delegates
{
    /// <summary>
    /// сласс для иллюстрации для работы с делегатами и событиями
    // </summary>
    public class Dir
    {
        IEnumerable<FileInfo> Files { get; set; }
        private FileInfo _maxFile;
        public Dir(string path)
        {

            DirectoryInfo DirInfo = new DirectoryInfo(path);
            Files = DirInfo.GetFiles();

        }
        public event EventHandler<FileArgs> FileFound;
        protected virtual void OnFileToched(FileInfo f)
        {
            var eventArg = new FileArgs { Name = f.Name, Size = f.Length };
            EventHandler<FileArgs> handler = FileFound;
            if (handler != null)
            {
                handler?.Invoke(this, eventArg);
            }
        }

        public FileInfo MaxFile
        {
            get
            {
                if (_maxFile == null)
                    _maxFile = Files?.GetMax(getSFileSize);
                return _maxFile;
            }
        }
        public class FileArgs : EventArgs
        {
            public string Name { get; set; }
            public long Size { get; set; }
        }


        public Func<FileInfo, float> getSFileSize = x => x.Length;
        public void ProcessFile()
        {

            foreach (FileInfo file in Files)
            {
                OnFileToched(file);
            }
        }
    }
}
