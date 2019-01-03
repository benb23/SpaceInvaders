﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace A19_Ex02_Ben_305401317_Dana_311358543
{
    public class Gun
    {
        public void Shoot(Bullet i_Bullet,Vector2 i_ShooterOrigin, Game i_Game )
        {
            i_Bullet.Initialize();
            i_Bullet.Position = new Vector2(i_ShooterOrigin.X , i_ShooterOrigin.Y - i_Bullet.Height / 2);
        }
    }
}
