using Microsoft.Xna.Framework;
using System;

namespace POOII_Monogame {

    internal class Enemy : GameObject {
        private static Vector2 target;
        public bool isAlive = true;

        public Enemy(Vector2 _position) : base(_position) {
            LoadContent();
        }

        public static void SetTarget(Vector2 playerPosition) {
            target = playerPosition;
        }

        public override void Update(GameTime gameTime) {
            Vector2 direction = target - position;
            float magnitud = (float)Math.Sqrt((direction.X * direction.X) + (direction.Y * direction.Y));
            direction /= magnitud;
            position += direction * 5;
            base.Update(gameTime);
        }
    }
}