using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace POOII_Monogame {

    internal class Turret : GameObject {
        private float shootTime = 0;
        private List<Projectile> projectiles = new List<Projectile>();
        private float range = 350f;
        public bool isAlive = true;

        public Turret(Vector2 position) : base(position) {
            base.LoadContent();
        }

        public void Update(GameTime gameTime, Vector2 target) {
            if (!isAlive) {
                return;
            }
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shootTime += elapsedTime;
            if (shootTime >= 4f) {
                shootTime = 0;
                if (InRange(target)) {
                    Fire(target);
                }
            }
            foreach (Projectile p in projectiles) {
                p.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (!isAlive) {
                return;
            }
            spriteBatch.Draw(sprite, position, Color.White);
            foreach (Projectile p in projectiles) {
                spriteBatch.Draw(p.GetSprite(), p.position, Color.White);
            }
        }

        private void Fire(Vector2 target) {
            Projectile projectile = new Projectile(position, target);
            projectiles.Add(projectile);
        }

        private bool InRange(Vector2 target) {
            float x = MathF.Pow((position.X - target.X), 2);
            float y = MathF.Pow((position.Y - target.Y), 2);
            float distance = MathF.Sqrt(x + y);
            return distance < range;
        }

        public List<Projectile> GetProjectiles() {
            return projectiles;
        }

        public void RemovePorjectile(Projectile t_projectile) {
            projectiles.Remove(t_projectile);
        }
    }
}