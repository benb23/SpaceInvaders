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
    class Bullet : Sprite
    {
        public enum BulletType { SpaceShipBullet = -1, EnemyBullet = 1 };
        private BulletType m_Type;
        private readonly float r_BulletVelocity = 155;

        public override void Update(GameTime gameTime)
        {
            Sprite hittenSprite;//TODO:name

            hittenSprite = isBulletHitElement();

            if (isBulletHitTheScreenBorder() || hittenSprite != null)
            {
                //remove bullet
                RemoveComponent();

                Visible = false;
                //Dispose();

                if (hittenSprite != null)
                {
                    hittenSprite.RemoveComponent();
                }
            }
            else
            {
                Position = new Vector2(Position.X, Position.Y + (float)m_Type * r_BulletVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        private bool isBulletHitTheScreenBorder()
        {
            if(m_Position.Y <= 0 || m_Position.Y >= Game.GraphicsDevice.Viewport.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Bullet(Game game, BulletType bulletType, Sprite shooter ) :base(game)
        {
            m_AssetName = @"Sprites\Bullet";
            m_Type = bulletType;

            if(m_Type == BulletType.EnemyBullet)
            {
                m_Tint = Color.Blue;
            }
            else
            {
                m_Tint = Color.Red;
            }

            initBulletPosition(shooter);

            Visible = true;
            m_Type = bulletType;
        }

        private Sprite isBulletHitElement()//TODO: change name!
        {
            Sprite hittenSprite = null;
            Rectangle BulletRectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

            foreach (DrawableGameComponent sprite in Game.Components)
            {
                if (isOpponent(sprite))
                {
                    Rectangle elementRectangle = new Rectangle((int)((Sprite)sprite).Position.X, (int)((Sprite)sprite).Position.Y, (int)((Sprite)sprite).Texture.Width, (int)((Sprite)sprite).Texture.Height);

                    if (BulletRectangle.Intersects(elementRectangle))
                    {
                        hittenSprite = (Sprite)sprite;
                    }
                }
            }

            return hittenSprite;
        }

        private bool isOpponent(DrawableGameComponent sprite)
        {
            bool isOpponent;

            if((m_Type==BulletType.EnemyBullet && sprite is SpaceShip) || (m_Type == BulletType.SpaceShipBullet && (sprite is Enemy || sprite is MotherSpaceShip)))
            {
                isOpponent = true;
            }
            else
            {
                isOpponent = false;
            }

            return isOpponent;
        }
        //(Position == ((Sprite)element).Position && !(element is Bullet)

        public void initBulletPosition(Sprite i_Shooter)
        {
            Position=new Vector2(i_Shooter.Position.X + i_Shooter.Texture.Width/ 2, i_Shooter.Position.Y +(float)m_Type*(1+i_Shooter.Texture.Height));//TODO: CONST 32 SHOOTER WIDTH
        }

        public override void initPosition()
        {
           
        }
    }
}
