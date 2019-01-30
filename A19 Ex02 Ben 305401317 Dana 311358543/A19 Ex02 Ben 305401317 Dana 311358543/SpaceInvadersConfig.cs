﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex02_Ben_305401317_Dana_311358543
{
    public static class SpaceInvadersConfig
    {
        public enum eLevel
        {
            One,
            Two,
            tree,
            Four,
            Five,
            Six
        };

        public enum eScoreValue
        {
            MotherShip = 850,
            Soul = -1100,
            PinkEnemy = 260,
            BlueEnemy = 140,
            YellowEnemy = 110
        }

        public enum eNumOfPlayers
        {
            OnePlayer = 1,
            TwoPlayers = 2
        };

        public static eNumOfPlayers m_NumOfPlayers = eNumOfPlayers.OnePlayer; // default

        public static eLevel m_Level = eLevel.One;

        public const double m_sizeOfBulletHitEffect = 0.7;

        public const int k_EnemyScoreAddition = 120;

        public const float k_WallsVelocitiyAdditionPercent = (float)-0.7;

        public const int k_EnemyShootingFrequencyAddition = 5;

    }
}