using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace POOII_Monogame {

    internal class TextureManager {
        private static readonly TextureManager instance = new TextureManager();

        private List<Texture2D> textureList;
        private string[] texturePaths;

        private TextureManager() {
            textureList = new List<Texture2D>();
            //string filePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "GameTextures.txt");
            texturePaths = File.ReadAllLines("GameTextures.txt");
        }

        public void LoadTexture() {
            foreach (string path in texturePaths) {
                Texture2D texture2D;
                try {
                    texture2D = GameObject.Content.Load<Texture2D>(path);
                } catch {
                    texture2D = GameObject.Content.Load<Texture2D>("Sprites/T_Missing");
                }

                textureList.Add(texture2D);
            }
        }

        public Texture2D AssignTexture(GameObject gameObject) {
            if (gameObject.GetType() == typeof(Player)) {
                return textureList[0];
            }
            if (gameObject.GetType() == typeof(Enemy)) {
                return textureList[1];
            }
            if (gameObject.GetType() == typeof(Cave)) {
                return textureList[2];
            }
            if (gameObject.GetType() == typeof(Grass)) {
                return textureList[3];
            }
            if (gameObject.GetType() == typeof(Rock)) {
                return textureList[4];
            }
            if (gameObject.GetType() == typeof(Projectile)) {
                return textureList[5];
            }
            if (gameObject.GetType() == typeof(Turret)) {
                return textureList[6];
            }
            if (gameObject.GetType() == typeof(HealthBuff)) {
                return textureList[7];
            }
            if (gameObject.GetType() == typeof(SpeedBuff)) {
                return textureList[8];
            } else {
                return null;
            }
        }

        public static TextureManager Instance {
            get { return instance; }
        }
    }
}