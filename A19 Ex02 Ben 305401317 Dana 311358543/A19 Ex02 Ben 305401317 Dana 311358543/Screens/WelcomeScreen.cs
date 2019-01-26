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

using Infrastructure;

namespace A19_Ex02_Ben_305401317_Dana_311358543
{
    public class WelcomeScreen : MenuScreen
    {
        private Background m_Background;
        private MenuHeader m_MenuHeader;

        public WelcomeScreen(Game i_Game) : base(i_Game, new Vector2(250 ,250), 15f)
        {
            this.m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            this.m_MenuHeader = new MenuHeader(this, @"Screens\Wellcome\SpaceInvadersLogo");
        }

        public override void Initialize()
        {
            int index = 0;
            AddMenuItem(new MenuItem(@"Screens\Wellcome\PlayGame", this, index++));
            AddMenuItem(new MenuItem(@"Screens\Wellcome\PlayGame", this, index++));
            AddMenuItem(new MenuItem(@"Screens\Wellcome\PlayGame", this, index++));
            AddMenuItem(new MenuItem(@"Screens\Wellcome\PlayGame", this, index++));

            m_MenuHeader.Scales *= 0.8f;
            m_MenuHeader.Position = new Vector2(GraphicsDevice.Viewport.Width / 10, 20);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.KeyPressed(Keys.Enter))
            {
                ExitScreen();
            }
            if (InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }

            base.Update(gameTime);
        }
    }
}
