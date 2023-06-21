using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace POOII_Monogame {

    internal class PowerUp : GameObject {
        public bool isAlive = true;

        public PowerUp(Vector2 position) : base(position) {
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (!isAlive) {
                return;
            }
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}