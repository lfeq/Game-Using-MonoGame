using Microsoft.Xna.Framework;

namespace POOII_Monogame {

    internal class Terrain : GameObject {

        public Terrain() {
        }

        public Terrain(Vector2 position) : base(position) {
        }
    }

    internal class Grass : Terrain {

        public Grass(Vector2 position) : base(position) {
        }
    }

    internal class Rock : Terrain {

        public Rock(Vector2 position) : base(position) {
        }
    }

    internal class Cave : Terrain {

        public Cave(Vector2 position) : base(position) {
        }
    }
}