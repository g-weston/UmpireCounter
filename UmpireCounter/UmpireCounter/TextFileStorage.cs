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
        public static string filePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "Score.txt");
        
        public static void readScore()
        {
            if (filePath == null || !File.Exists(filePath))
            {
                resetScore();
            }
            else
            {
                List<string> fileContents = new List<string>();
                StreamReader file = new StreamReader(filePath);

                string line;
                while ((line = file.ReadLine()) != null)
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
                file.Dispose();
            }
        }

        public static void writeScore()
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.AutoFlush = true;

                file.WriteLine(Score.Total.ToString());
                file.WriteLine(Score.Wickets.ToString());
                file.WriteLine(Score.Overs.ToString());

                file.Flush();
                file.Close();
                file.Dispose();
            }
            
        }

        public static void resetScore()
        {
            Score.Total = 0;
            Score.Wickets = 0;
            Score.Overs = 0;
            Score.ValidDeliveriesInOver = 0;
            writeScore();
        }
    }

}
