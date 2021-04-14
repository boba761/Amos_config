using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NMR.TypeFiles.FileTNT;

namespace NMR.TypeFiles
{
    public class SequenceFileBuilder : FileBuilder
    {
        private FileData _fileData;

        public override void Open(Stream stream)
        {
            _fileData = new FileData(new BinaryReader(stream));
        }

        public override void Save(System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
