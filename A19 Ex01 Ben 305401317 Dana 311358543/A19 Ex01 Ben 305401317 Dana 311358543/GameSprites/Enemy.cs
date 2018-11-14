﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace A19_Ex01_Ben_305401317_Dana_311358543
{
    class Enemy : Sprite
    {
        static Random m_RandomNum = new Random();
        private Gun m_Gun = new Gun(); 

        public Enemy(Game i_Game) : base(i_Game)
        {
            
        }
        public override void Update(GameTime i_gameTime)
        {
            int rnd = m_RandomNum.Next(0, 10000);
            
            if (rnd <= 5)
            {
                m_Gun.Shoot(new Bullet(Game,Bullet.BulletType.EnemyBullet,Position));//game?
            }

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void initPosition()
        {
            
        }
    }
}
