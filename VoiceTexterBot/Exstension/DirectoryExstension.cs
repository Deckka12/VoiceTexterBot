using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceTexterBot.Exstension {
    class DirectoryExstension {
        /// <summary>
        /// Получаем путь до каталога с .sln файлом
        /// </summary>
        public static string GetSolutionRoot () {
            var thisDirectory = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            var fullname = Directory.GetParent(thisDirectory).FullName;
            var projectRoot = fullname.Substring(0, fullname.Length - 4);
            return Directory.GetParent(projectRoot)?.FullName;
        }
    }
}
