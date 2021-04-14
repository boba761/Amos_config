using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Amos.TypeFiles
{
    public abstract class FileBuilder
    {
        /// <summary>
        /// Устанавливает или возврашает объект документа для загрузки или сохранения
        /// </summary>
        public static Document Document { get; set; }

        /// <summary>
        /// Загружает данные из потока
        /// </summary>
        /// <param name="stream">Поток данных на чтение</param>
        public abstract void Load(Stream stream);

        /// <summary>
        /// Сохраняет данные в поток
        /// </summary>
        /// <param name="stream">Поток данных на запись</param>
        public abstract void Save(Stream stream);
    }
}
