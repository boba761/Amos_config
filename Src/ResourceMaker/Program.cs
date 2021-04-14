using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Drawing;

namespace ResourceMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Запуск формирования ресурсов.");
            if (args.Length != 4)
            {
                Console.WriteLine("Ошибка формирования ресурсов. Не правильные параметры командной строки.");
                return;
            }

            FileInfo sourceFile = new FileInfo(args[0]);
            DirectoryInfo imageDir = new DirectoryInfo(args[1]);
            DirectoryInfo targetDir = new DirectoryInfo(args[2]);

            Console.WriteLine("sourceFile = {0}\nimageDir = {1}\ntargetDir = {2}\nbaseFileName = {3}",
                sourceFile.FullName, imageDir.FullName, targetDir.FullName, args[3]);
            ResourceBuilder builder = new ResourceBuilder(args[0], args[1], args[2], args[3]);
            builder.Start();
            builder.Dispose();
            Console.WriteLine("Ресурсы успешно сформированны.");
        }
    }

    public class ResourceBuilder
    {
        private string _sourceFile;
        private string _imageDir;
        private string _targetDir;
        private string _baseFileName;

        private Dictionary<string, ResXResourceWriter> _hashResourceWriter;

        public ResourceBuilder( string sourceFile, string imageDir, string targetDir, string baseFileName )
        {
            _sourceFile = sourceFile;
            _imageDir = imageDir;
            _targetDir = targetDir;
            _baseFileName = baseFileName;
            _hashResourceWriter = new Dictionary<string, ResXResourceWriter>();
        }

        public void Dispose()
        {
            foreach (ResXResourceWriter obj in _hashResourceWriter.Values)
                obj.Close();
        }

        public void Start()
        {
            OleDbConnection connectSource = new OleDbConnection(string.Format(
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};" +
                @"Extended Properties=Excel 8.0;", _sourceFile));
            try
            {
                connectSource.Open();
                DataSet dataSet = new DataSet();
                OleDbDataAdapter adapterString = new OleDbDataAdapter("SELECT * FROM [Strings$]", connectSource);
                adapterString.Fill(dataSet);
                SetStrings(dataSet);
                SetImages( new DirectoryInfo( _imageDir ) );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connectSource.Close();
            }
        }

        private void SetStrings(DataSet dataSet)
        {
            DataTable table = dataSet.Tables[0];
            for (int i = 1; i < table.Columns.Count; i++)
            {
                Console.WriteLine( "Column name {0}", table.Columns[i].ColumnName );
                ResXResourceWriter writer = GetResourceWriter(table.Columns[i].ColumnName);
                foreach (DataRow row in table.Rows)
                {
                    if (row[table.Columns[i]].ToString().Trim() != string.Empty)
                        writer.AddResource(row[0].ToString(), row[table.Columns[i]].ToString());
                }
            }
        }

        private void SetImages(DirectoryInfo dirInfo)
        {
            Console.WriteLine("----> Dirrectory Images: " + dirInfo.FullName);
            SetImagesDir(null, dirInfo);
            foreach ( DirectoryInfo dir in dirInfo.GetDirectories() )
            {
                if ( dir.Name.ToUpper() == ".SVN" )
                    continue;
                SetImagesDir( dir.Name, dir );
            }
        }

        private void SetImagesDir(string name, DirectoryInfo dirInfo)
        {
            //Console.WriteLine("----> Dirrectory:" + name);
            ResXResourceWriter writer = GetResourceWriter(name);
            foreach (FileInfo file in dirInfo.GetFiles("*.png"))
            {
                Console.WriteLine("Image file: {0}", file.FullName);
                string nameRes = "image." + Path.GetFileNameWithoutExtension(file.Name);
                ResXDataNode node = new ResXDataNode(nameRes, new ResXFileRef(file.FullName, 
                        "System.Drawing.Bitmap, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
                writer.AddResource(node);
                Console.WriteLine(nameRes);
            }
            foreach (FileInfo file in dirInfo.GetFiles("*.ico"))
            {
                Console.WriteLine("Icon file: {0}", file.FullName);
                string nameRes = "icon." + Path.GetFileNameWithoutExtension(file.Name);
                ResXDataNode node = new ResXDataNode(nameRes, new ResXFileRef(file.FullName,
                        "System.Drawing.Icon, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
                writer.AddResource(node);
                Console.WriteLine(nameRes);
            }
            foreach ( FileInfo file in dirInfo.GetFiles( "*.cur" ) )
            {
                Console.WriteLine( "Cursor file: {0}", file.FullName );
                string nameRes = "cur." + Path.GetFileNameWithoutExtension( file.Name );
                ResXDataNode node = new ResXDataNode( nameRes, new ResXFileRef( file.FullName,
                        "System.Byte[], mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" ) );
                writer.AddResource( node );
                Console.WriteLine( nameRes );
            }
        }

        private ResXResourceWriter GetResourceWriter(string name)
        {
            string fileName = Path.Combine(_targetDir, _baseFileName);
            if (name != null && name.ToUpper() != "NEUTRAL")
                fileName += "." + name;
            fileName += ".resx";

            ResXResourceWriter rWriter = null;
            if (_hashResourceWriter.TryGetValue(fileName, out rWriter) == false)
            {
                rWriter = new ResXResourceWriter(fileName);
                _hashResourceWriter.Add(fileName, rWriter);
            }
            return rWriter;
        }
    } 
}
