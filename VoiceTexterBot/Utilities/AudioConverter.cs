using FFMpegCore;
using VoiceTexterBot.Exstension;


namespace VoiceTexterBot.Utilities {
    class AudioConverter {
        public static void TryConvert(string inputFile, string outputFile) {
            // Задаём путь, где лежит вспомогательная программа - конвертер
            GlobalFFOptions.Configure(options => options.BinaryFolder = Path.Combine(DirectoryExstension.GetSolutionRoot(), "FFmpeg-win64", "bin"));

            // Вызываем Ffmpeg, передав требуемые аргументы.
            FFMpegArguments
              .FromFileInput(inputFile)
              .OutputToFile(outputFile, true, options => options
                .WithFastStart())
              .ProcessSynchronously();
        }
    }
}
