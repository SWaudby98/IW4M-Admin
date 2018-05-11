﻿using SharedLibraryCore.Helpers;
using SharedLibraryCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IW4MAdmin.Plugins.Stats.Cheat
{
    class Strain : ITrackable
    {
        private static double StrainDecayBase = 0.15;
        private double CurrentStrain;
        private Vector3 LastAngle;
        private double LastDeltaTime;
        private double LastDistance;

        public int TimesReachedMaxStrain { get; private set; }

        public double GetStrain(Vector3 newAngle, double deltaTime)
        {
            if (LastAngle == null)
                LastAngle = newAngle;

            LastDeltaTime = deltaTime;

            double decayFactor = GetDecay(deltaTime);
            CurrentStrain *= decayFactor;

            double[] distance = Helpers.Extensions.AngleStuff(newAngle, LastAngle);
            LastDistance = distance[0] + distance[1];

            // this happens on first kill
            if ((distance[0] == 0 && distance[1] == 0) ||
                deltaTime == 0 || 
                double.IsNaN(CurrentStrain))
            {
                return CurrentStrain;
            }

            double newStrain = Math.Pow(distance[0] + distance[1], 0.99) / deltaTime;
            CurrentStrain += newStrain;

            if (CurrentStrain > Thresholds.MaxStrainFlag)
                TimesReachedMaxStrain++;

            LastAngle = newAngle;
            return CurrentStrain;
        }

        public string GetTrackableValue()
        {
            return $"Strain - {CurrentStrain}, Angle - {LastAngle}, Delta Time - {LastDeltaTime}, Distance - {LastDistance}";
        }

        private double GetDecay(double deltaTime) => Math.Pow(StrainDecayBase, deltaTime / 1000.0);
    }
}