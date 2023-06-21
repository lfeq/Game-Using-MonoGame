using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace POOII_Monogame {

    internal class GameObject {
        public static ContentManager Content;
        public BoxCollider boxCollider;

        protected Texture2D sprite;
        public Vector2 position;

        public GameObject() {
        }

        public GameObject(Vector2 _position) {
            position = _position;
        }

        public void LoadContent() {
            sprite = TextureManager.Instance.AssignTexture(this);
            boxCollider = new BoxCollider(position, new Vector2(sprite.Width, sprite.Height));
        }

        public virtual void Update(GameTime gameTime) {
            boxCollider.Update(position);
        }

        public Texture2D GetSprite() {
            return sprite;
        }
    }
}