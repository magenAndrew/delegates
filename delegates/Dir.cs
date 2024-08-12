using System.IO;
using System.Runtime;

namespace Delegates
{
    /// <summary>
    /// Класс для иллюстрации для работы с делегатами и событиями
    // </summary>
    public class Dir
    {
        public DirectoryInfo DirInfo { get; private set; }

        public Dir(string path) => DirInfo = new DirectoryInfo(path);

        public event EventHandler<FileArgs> FileFound;
 
        public FileInfo MaxFile
        {
            get
            {
                return DirInfo.GetFiles().GetMax(GetSFileSize);
            }
        }
        public class FileArgs : EventArgs
        {
            public string? Name { get; set; }
            public long? Size { get; set; }
        }


        public Func<FileInfo, float> GetSFileSize = x => x.Length;

        public void ProcessFile()
        {

            foreach (FileInfo file in DirInfo.GetFiles())
            {
                OnFileToched(file);
            }
        }
        protected virtual void OnFileToched(FileInfo f)
        {
            FileFound?.Invoke(this, new FileArgs { Name = f.Name, Size = f.Length });
        }

    }
}
