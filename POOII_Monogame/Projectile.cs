using Microsoft.Xna.Framework;
using System;

namespace POOII_Monogame {

    internal class Projectile : GameObject {
        private Vector2 direction;
        private float baseSpeed = 2f;

        public Projectile(Vector2 position, Vector2 target) : base(position) {
            base.LoadContent();
            direction = target - position;
            float magnitude = (float)MathF.Sqrt((direction.X * direction.X) + (direction.Y * direction.Y));
            direction /= magnitude;
            direction *= baseSpeed;
        }

        public override void Update(GameTime gameTime) {
            position += direction;
            base.Update(gameTime);
        }
    }
}