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

                if (int.TryParse(fileContents[3], out int ballsInOver))
                {
                    Score.ValidDeliveriesInOver = ballsInOver;
                }

                if (fileContents[5] != null)
                {
                    if (fileContents[5] == "True")
                    {
                        if (fileContents[4] != null)
                        {
                            Score.InningsStartTime = DateTime.Parse(fileContents[4]);
                            if (Score.InningsStartTime != null)
                            {
                                Score.UpdateInningsTimer();
                                Score.InningsInPlay = true;
                            }
                        }
                    }
                    else
                    {
                        Score.InningsTime = "Innings hasn't started";
                        Score.InningsInPlay = false;
                    }
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
                fileScoreWrite.WriteLine(Score.ValidDeliveriesInOver.ToString());
                fileScoreWrite.WriteLine(Score.InningsStartTime.ToString());
                fileScoreWrite.WriteLine(Score.InningsInPlay.ToString());

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
                SettingsPage.TimerOnOff = true;
                SettingsPage.LoadInPage = false;
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
                fileSettingsRead.Dispose();
                if (fileSettingsContents.Count == 4)
                {
                                        
                    if (fileSettingsContents[0] == "True")
                    {
                        SettingsPage.Vibrate = true;
                    }
                    else if (fileSettingsContents[0] == "False")
                    {
                        SettingsPage.Vibrate = false;
                    }

                    if (fileSettingsContents[2] == "True")
                    {
                        SettingsPage.TimerOnOff = true;
                    }
                    else if (fileSettingsContents[2] == "False")
                    {
                        SettingsPage.TimerOnOff = false;
                    }

                    if (int.TryParse(fileSettingsContents[1], out int ballsOver))
                    {
                        Score.BallsInOver = ballsOver;
                    }

                    if (fileSettingsContents[3] == "True")
                    {
                        SettingsPage.LoadInPage = true;
                    }
                    else if (fileSettingsContents[3] == "False")
                    {
                        SettingsPage.LoadInPage = false;
                    }
                }
                else
                {
                    Score.BallsInOver = 6;
                    SettingsPage.Vibrate = true;
                    SettingsPage.TimerOnOff = true;
                    SettingsPage.LoadInPage = false;
                    TextFileStorage.WriteSettings();
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
                fileSettingsWrite.WriteLine(SettingsPage.TimerOnOff.ToString());
                fileSettingsWrite.WriteLine(SettingsPage.LoadInPage.ToString());

                fileSettingsWrite.Flush();
                fileSettingsWrite.Close();
                fileSettingsWrite.Dispose();
            }

        }
    }

}
