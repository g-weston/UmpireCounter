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
        public static double Overs { get; set; }

        public static void IncreaseBalls()
        {
            Score.Overs += 0.1;
            Score.ValidDeliveriesInOver++;

            if (Score.ValidDeliveriesInOver == 6)
            {
                Score.Overs += 0.4;
                Score.ValidDeliveriesInOver = 0;
            }

            Score.Overs = Math.Round(Score.Overs, 1);
        }

        public static void DecreaseBalls()
        {
            if (Score.Overs % 1 == 0)
            {
                Score.ValidDeliveriesInOver = 5;
                Score.Overs -= 0.5;
            }
            else
            {
                Score.ValidDeliveriesInOver--;
                Score.Overs -= 0.1;
            }

            if (Score.Overs < 0)
            {
                Score.Overs = 0;
                Score.ValidDeliveriesInOver = 0;
            }

            Score.Overs = Math.Round(Score.Overs, 1);
        }

        public static void IncreaseTotal()
        {
            Score.Total++;
        }

        public static void DecreaseTotal()
        {
            Score.Total--;
            if (Score.Total < 0)
            {
                Score.Total = 0;
            }
        }

        public static void IncreaseWickets()
        {
            Score.Wickets++;
        }

        public static void DecreaseWickets()
        {
            Score.Wickets--;
            if (Score.Wickets < 0)
            {
                Score.Wickets = 0;
            }
        }
    }
}
