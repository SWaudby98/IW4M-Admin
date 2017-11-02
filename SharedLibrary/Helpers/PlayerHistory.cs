﻿using System;

namespace SharedLibrary.Helpers
{
    public class PlayerHistory
    {
        // how many minutes between updates
        public static readonly int UpdateInterval = 5;

        public PlayerHistory(int cNum)
        {
            DateTime t = DateTime.UtcNow;
            When = new DateTime(t.Year, t.Month, t.Day, t.Hour, Math.Min(59, UpdateInterval * (int)Math.Round(t.Minute / (float)UpdateInterval)), 0);
            PlayerCount = cNum;
        }

#if DEBUG
        public PlayerHistory(DateTime t, int cNum)
        {
            When = new DateTime(t.Year, t.Month, t.Day, t.Hour, Math.Min(59, UpdateInterval * (int)Math.Round(t.Minute / (float)UpdateInterval)), 0);
            PlayerCount = cNum;
        }
#endif 

        private DateTime When;
        private int PlayerCount;

        /// <summary>
        /// Used by CanvasJS as a point on the x axis
        /// </summary>
        public string x
        {
            get
            {
                return When.ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
        }


        /// <summary>
        /// Used by CanvasJS as a point on the y axis
        /// </summary>
        public int y
        {
            get
            {
                return PlayerCount;
            }
        }
    }
}
