using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;

namespace UmpireCounter
{
    class TextFileStorage
    {
        public static string FilePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "Score.txt");
        
        public static void ReadScore()
        {
            if (FilePath == null || !File.Exists(FilePath))
            {
                ResetScore();
            }
            else
            {
                List<string> fileContents = new List<string>();
                StreamReader fileRead = new StreamReader(FilePath);

                string line;
                while ((line = fileRead.ReadLine()) != null)
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

                if (double.TryParse(fileContents[2], out double overs))
                {
                    Score.Overs = overs;
                    Score.ValidDeliveriesInOver = Convert.ToInt32((overs - Math.Floor(Score.Overs)) * 10);
                }
                fileRead.Dispose();
            }
        }

        public static void WriteScore()
        {
            using (StreamWriter fileWrite = new StreamWriter(FilePath))
            {
                fileWrite.AutoFlush = true;

                fileWrite.WriteLine(Score.Total.ToString());
                fileWrite.WriteLine(Score.Wickets.ToString());
                fileWrite.WriteLine(Score.Overs.ToString());

                fileWrite.Flush();
                fileWrite.Close();
                fileWrite.Dispose();
            }
            
        }

        public static void ResetScore()
        {
            Score.ResetScore();
            WriteScore();
        }
    }

}
