using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace POOII_Monogame {

    public class PlayerData {
        public int score { get; set; }
    }

    internal class Player : GameObject {
        private Vector2 direction;
        private PlayerData data;
        public float speed = 5;
        public int health = 100;
        public int score = 0;
        private int uiTracker = 0;
        private List<Projectile> projectiles = new List<Projectile>();

        public Player(Vector2 _position) : base(_position) {
            data = JSON.CreatePlayerFromJSON();
            score = data.score;
            uiTracker = UIManager.Instance.AddElement("HP " + health.ToString() + " Score " + score.ToString(), new Vector2(400, 400));
        }

        public override void Update(GameTime gameTime) {
            //direction = ControllerMovement();
            direction = KeyboardMovement();
            position += direction;
            UIManager.Instance.UpdateElement(uiTracker, "HP " + health.ToString() + " Score " + score.ToString());
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                Fire();
            }
            foreach (Projectile p in projectiles) {
                p.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, position, Color.White);
            foreach (Projectile p in projectiles) {
                spriteBatch.Draw(p.GetSprite(), p.position, Color.White);
            }
        }

        public List<Projectile> GetProjectiles() {
            return projectiles;
        }

        public void RemovePorjectile(Projectile t_projectile) {
            projectiles.Remove(t_projectile);
        }

        private Vector2 ControllerMovement() {
            float x = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * speed;
            float y = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * speed;
            return new Vector2(x, -y);
        }

        private Vector2 KeyboardMovement() {
            float x, y;
            x = 0;
            y = 0;
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up)) {
                y = speed;
            }
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) {
                x = -speed;
            }
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down)) {
                y = -speed;
            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) {
                x = speed;
            }

            return new Vector2(x, -y);
        }

        private void Fire() {
            Vector2 target = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Projectile projectile = new Projectile(position, target);
            projectiles.Add(projectile);
        }
    }
}