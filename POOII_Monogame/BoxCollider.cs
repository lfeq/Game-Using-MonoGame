using Microsoft.Xna.Framework;

namespace POOII_Monogame {

    internal class BoxCollider {
        public Vector2 min, max, size;

        public BoxCollider(Vector2 _position, Vector2 _size) {
            min = _position;
            size = _size;
            max = min + size;
        }

        public void Update(Vector2 position) {
            min = position;
            max = position + size;
        }
    }
}