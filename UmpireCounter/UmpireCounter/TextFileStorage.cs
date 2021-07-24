using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;

namespace UmpireCounter
{
    class TextFileStorage
    {
        public static string FilePathScore = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "Score.txt");
        public static string FilePathSettings = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "Settings.txt");

        public static void ReadScore()
        {
            if (FilePathScore == null || !File.Exists(FilePathScore))
            {
                ResetScore();
            }
            else
            {
                List<string> fileContents = new List<string>();
                StreamReader fileScoreRead = new StreamReader(FilePathScore);

                string line;
                while ((line = fileScoreRead.ReadLine()) != null)
                {
                    fileContents.Add(line);
                }

                if (int.TryParse(fileContents[0], out int total))
                {
                    Score.Total = total;
                }

                if (int.TryParse(fileContents[1], out int wickets))
                {
                    Score.Wickets = wickets;
                }

                if (int.TryParse(fileContents[2], out int overs))
                {
                    Score.OversCompleted = overs;
                }

                if (int.TryParse(fileContents[2], out int ballsInOver))
                {
                    Score.ValidDeliveriesInOver = ballsInOver;
                }

                fileScoreRead.Dispose();
            }
        }

        public static void WriteScore()
        {
            using (StreamWriter fileScoreWrite = new StreamWriter(FilePathScore))
            {
                fileScoreWrite.AutoFlush = true;

                fileScoreWrite.WriteLine(Score.Total.ToString());
                fileScoreWrite.WriteLine(Score.Wickets.ToString());
                fileScoreWrite.WriteLine(Score.OversCompleted.ToString());

                fileScoreWrite.Flush();
                fileScoreWrite.Close();
                fileScoreWrite.Dispose();
            }
            
        }

        public static void ResetScore()
        {
            Score.ResetScore();
            WriteScore();
        }

        public static void ReadSettings()
        {
            if (FilePathSettings == null || !File.Exists(FilePathSettings))
            {
                Score.BallsInOver = 6;
                SettingsPage.Vibrate = true;
                TextFileStorage.WriteSettings();
            }
            else
            {
                List<string> fileSettingsContents = new List<string>();
                StreamReader fileSettingsRead = new StreamReader(FilePathSettings);

                string line;
                while ((line = fileSettingsRead.ReadLine()) != null)
                {
                    fileSettingsContents.Add(line);
                }

                if (fileSettingsContents[0] == "True")
                {
                    SettingsPage.Vibrate = true;
                }
                else if (fileSettingsContents[0] == "False")
                {
                    SettingsPage.Vibrate = false;
                }

                if (int.TryParse(fileSettingsContents[1], out int ballsOver))
                {
                    Score.BallsInOver = ballsOver;
                }
            }
            
        }

        public static void WriteSettings()
        {
            using (StreamWriter fileSettingsWrite = new StreamWriter(FilePathSettings))
            {
                fileSettingsWrite.AutoFlush = true;

                fileSettingsWrite.WriteLine(SettingsPage.Vibrate.ToString());
                fileSettingsWrite.WriteLine(Score.BallsInOver.ToString());
                
                fileSettingsWrite.Flush();
                fileSettingsWrite.Close();
                fileSettingsWrite.Dispose();
            }

        }
    }

}
