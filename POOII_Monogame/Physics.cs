namespace POOII_Monogame {

    internal class Physics {

        public Physics() {
        }

        public bool IsColliding(BoxCollider a, BoxCollider b) {
            if (a.max.X < b.min.X || a.min.X > b.max.X) return false;
            if (a.max.Y < b.min.Y || a.min.Y > b.max.Y) return false;
            return true;
        }
    }
}