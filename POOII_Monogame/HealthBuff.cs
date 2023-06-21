using Microsoft.Xna.Framework;

namespace POOII_Monogame {

    internal class HealthBuff : PowerUp {

        public HealthBuff(Vector2 t_position) : base(t_position) {
            base.LoadContent();
        }
    }
}