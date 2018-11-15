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
    class MotherSpaceShip : Sprite
    {
        int rnd = 100;
        private readonly float k_MotherShipVelocity = 40;

        public MotherSpaceShip(Game i_Game) : base(i_Game)
        {
            m_AssetName = @"Sprites\MotherShip_32x120";
            m_Tint = Color.Red;
            Visible = false;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void initPosition()
        {
            Position = new Vector2(-m_Texture.Width, m_Texture.Height);
        }

        public override void Update(GameTime i_GameTime)
        {
            if (!Visible)
                rnd =SpaceInvaders.m_RandomNum.Next(0, 55555);
            
            if (rnd <= 40)
            {
                Visible = true; 
                m_Position.X += k_MotherShipVelocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
                if (m_Position.X >= SpaceInvaders.graphics.GraphicsDevice.Viewport.Width)
                {
                    Visible = false;
                    initPosition();
                }
            }
        }
    }
}
