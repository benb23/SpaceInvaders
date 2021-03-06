﻿ ///*** Guy Ronen © 2008-2011 ***//
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Infrastructure
{
    public class CollisionsManager : GameService, ICollisionsManager
    {
        protected readonly List<ICollidable> m_Collidables = new List<ICollidable>();

        public CollisionsManager(Game i_Game) :
            base(i_Game, int.MaxValue)
        {
        }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ICollisionsManager), this);
        }

        public void AddObjectToMonitor(ICollidable i_Collidable)
        {
            if (!this.m_Collidables.Contains(i_Collidable))
            {
                this.m_Collidables.Add(i_Collidable);
                i_Collidable.PositionChanged += this.collidable_Changed;
                i_Collidable.SizeChanged += this.collidable_Changed;
                i_Collidable.VisibleChanged += this.collidable_Changed;
                i_Collidable.Disposed += this.collidable_Disposed;
            }
        }

        private void collidable_Disposed(object sender, EventArgs e)
        {
            ICollidable collidable = sender as ICollidable;

            if (collidable != null
                &&
                this.m_Collidables.Contains(collidable))
            {
                collidable.PositionChanged -= this.collidable_Changed;
                collidable.SizeChanged -= this.collidable_Changed;
                collidable.VisibleChanged -= this.collidable_Changed;
                collidable.Disposed -= this.collidable_Disposed;

                this.m_Collidables.Remove(collidable);
            }
        }

        private void collidable_Changed(object sender, EventArgs e)
        {
            if (sender is ICollidable)
            {
                // to be on the safe side :)
                this.checkCollision(sender as ICollidable);
            }
        }

        private void checkCollision(ICollidable i_Source)
        {
            if (i_Source.Visible)
            {
                List<ICollidable> collidedComponents = new List<ICollidable>();

                // finding who collided with i_Source:
                foreach (ICollidable target in this.m_Collidables)
                {
                    if (i_Source != target && target.Visible)
                    {
                        if (target.CheckCollision(i_Source))
                        {
                            collidedComponents.Add(target);
                        }
                    }
                }

                foreach (ICollidable target in collidedComponents)
                {
                    target.Collided(i_Source);
                    i_Source.Collided(target);
                }
            }
        }
    }
}
