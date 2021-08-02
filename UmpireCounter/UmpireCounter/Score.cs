 using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace UmpireCounter
{
    public static class Score
    {
        public static int Total { get; set; }
        public static int Wickets { get; set; }
        public static int ValidDeliveriesInOver { get; set; }
        public static int OversCompleted { get; set; }
        public static int BallsInOver { get; set; }
        public static bool InningsInPlay { get; set; }
        public static DateTime InningsStartTime { get; set; }
        public static string InningsTime { get; set; }

        public static void IncreaseBalls()
        {
            Score.ValidDeliveriesInOver++;

            if (Score.ValidDeliveriesInOver == Score.BallsInOver)
            {
                Score.OversCompleted++;
                Score.ValidDeliveriesInOver = 0;
            }

            if (!Score.InningsInPlay)
            {
                Score.InningsStartTime = DateTime.Now;
                Score.InningsInPlay = true;
            }

            TextFileStorage.WriteScore();
        }

        public static void DecreaseBalls()
        {
            if (Score.ValidDeliveriesInOver == 0)
            {
                Score.ValidDeliveriesInOver = (Score.BallsInOver - 1);
                Score.OversCompleted--;
            }
            else
            {
                Score.ValidDeliveriesInOver--;
            }

            if (Score.OversCompleted < 0)
            {
                Score.OversCompleted = 0;
                Score.ValidDeliveriesInOver = 0;
            }

            TextFileStorage.WriteScore();
        }

        public static void IncreaseTotal()
        {
            Score.Total++;
            TextFileStorage.WriteScore();
        }

        public static void DecreaseTotal()
        {
            Score.Total--;
            if (Score.Total < 0)
            {
                Score.Total = 0;
            }

            TextFileStorage.WriteScore();
        }

        public static void IncreaseWickets()
        {
            Score.Wickets++;
            TextFileStorage.WriteScore();
        }

        public static void DecreaseWickets()
        {
            Score.Wickets--;
            if (Score.Wickets < 0)
            {
                Score.Wickets = 0;
            }

            TextFileStorage.WriteScore();
        }

        public static void ResetScore()
        {
            Score.Total = 0;
            Score.Wickets = 0;
            Score.OversCompleted = 0;
            Score.ValidDeliveriesInOver = 0;
            Score.InningsTime = "Innings hasn't started";
            Score.InningsInPlay = false;
        }

        public static void UpdateInningsTimer()
        {
            Score.InningsTime = DateTime.Now.Subtract(Score.InningsStartTime).Minutes.ToString() + " minute(s)";
        }
    }
}
