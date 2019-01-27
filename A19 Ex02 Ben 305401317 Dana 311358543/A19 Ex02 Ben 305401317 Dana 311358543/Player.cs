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
    public class Player : GameComponent, IScoreable
    {
        private const int k_NumOfSouls = 3;
        private GameScreen m_GameScreen;
        private IInputManager m_InputManager;
        private bool m_IsAllowedToUseMouse;
        private Keys m_RightMoveKey;
        private Keys m_LeftMoveKey;
        private Keys m_ShootKey;
        private PlayerIndex m_PlayerType;
        private SpaceShip m_SpaceShip;
        private List<Soul> m_Souls = new List<Soul>(3);
        private bool m_Initialized = false;
        private int m_Score;
        private int m_CurrentSoulsNumber;

        public List<Soul> Souls
        {
            get { return m_Souls; }
        }

        public SpaceShip SpaceShip
        {
            get{ return m_SpaceShip; }
        }

        private int CurrentSoulsNum
        {
            get { return m_CurrentSoulsNumber; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        // TODO: move createSpaceShip & createSouls out from ctor
        public Player(GameScreen i_GameScreen, PlayerIndex i_PlayerType, Keys i_LeftKey, Keys i_RightKey, Keys i_ShootKey, bool i_IsAllowdToUseMouse, Vector2 initialPosition)
            : base(i_GameScreen.Game)
        {
            m_Souls = new List<Soul>(k_NumOfSouls);
            m_LeftMoveKey = i_LeftKey;
            m_RightMoveKey = i_RightKey;
            m_ShootKey = i_ShootKey;
            m_IsAllowedToUseMouse = i_IsAllowdToUseMouse;
            m_PlayerType = i_PlayerType;
            m_GameScreen = i_GameScreen;
            createSpaceShip(i_PlayerType);
            createSouls();
            m_CurrentSoulsNumber = m_Souls.Capacity;
            i_GameScreen.Add(this);
        }

        private void createSouls()
        {
            for (int i = 0; i < m_Souls.Capacity; i++)
            {
                this.m_Souls.Add(new Soul(m_GameScreen, new Vector2(0.5f), 0.5f, SpaceShip.AssetName, m_PlayerType, i));
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            if(!m_Initialized)
            {
                m_SpaceShip.Position = new Vector2(((int)m_PlayerType * m_SpaceShip.Texture.Width / 2) + m_SpaceShip.Texture.Width, Game.GraphicsDevice.Viewport.Height);
                m_Initialized = true;
            }

            if (m_IsAllowedToUseMouse)
            {
                moveSpaceShipUsingMouse(i_GameTime);
            }

            moveSpaceShipUsingKeyboard(i_GameTime, m_LeftMoveKey, m_RightMoveKey);

            if(isPlayerAskedToShoot(m_ShootKey) && m_SpaceShip.PermitionToShoot())
            {
                m_SpaceShip.Shoot();
            }

            m_CurrentSoulsNumber = m_Souls.Count;
            m_SpaceShip.Position = new Vector2(MathHelper.Clamp(m_SpaceShip.Position.X, m_SpaceShip.Texture.Width / 2, Game.GraphicsDevice.Viewport.Width - (m_SpaceShip.Texture.Width / 2)), m_SpaceShip.Position.Y);
        }

        private bool isPlayerAskedToShoot(Keys i_shootKey)
        {
            bool isPlayerAskedToShoot = false;

            if (m_InputManager.KeyReleased(i_shootKey) || 
                (m_InputManager.ButtonReleased(eInputButtons.Left) && m_IsAllowedToUseMouse))
            {
                isPlayerAskedToShoot = true;
            }
            
            return isPlayerAskedToShoot;
        }

        private void moveSpaceShipUsingMouse(GameTime i_GameTime)
        {
            m_SpaceShip.Position = new Vector2(m_SpaceShip.Position.X + m_InputManager.MousePositionDelta.X, m_SpaceShip.Position.Y);
        }

        private void moveSpaceShipUsingKeyboard(GameTime i_GameTime, Keys i_LeftKey, Keys i_RightKey)
        {
            if (m_InputManager.KeyboardState.IsKeyDown(i_LeftKey))
            {
                m_SpaceShip.Position = new Vector2(m_SpaceShip.Position.X - (m_SpaceShip.Speed * (float)i_GameTime.ElapsedGameTime.TotalSeconds), m_SpaceShip.Position.Y);
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(i_RightKey))
            {
                m_SpaceShip.Position = new Vector2(m_SpaceShip.Position.X + (m_SpaceShip.Speed * (float)i_GameTime.ElapsedGameTime.TotalSeconds), m_SpaceShip.Position.Y);
            }
        }

        private void createSpaceShip(PlayerIndex i_PlayerType)
        {
            if(i_PlayerType == PlayerIndex.One)
            {
                m_SpaceShip = new SpaceShip(m_GameScreen, @"Sprites\Ship01_32x32", Bullet.eBulletType.PlayerOneBullet, PlayerIndex.One);
            }
            else
            {
                m_SpaceShip = new SpaceShip(m_GameScreen, @"Sprites\Ship02_32x32", Bullet.eBulletType.PlayerTwoBullet, PlayerIndex.Two);
            }
        }

        public void destroyed_Finished(object sender, EventArgs e)
        {
            SpaceShip.Enabled = false;
            SpaceShip.Visible = false;  
        }

        public override void Initialize()
        {
            if (m_InputManager == null)
            {
                m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            }

            base.Initialize();
        }
    }
}
