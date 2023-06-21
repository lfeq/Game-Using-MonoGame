using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace POOII_Monogame {

    internal struct UIElement {
        public string text = "";
        public Texture2D texture = null;
        public Vector2 position = Vector2.Zero;

        public UIElement(Vector2 position) {
            this.position = position;
        }
    }

    internal class UIManager {
        private static readonly UIManager instance = new UIManager();
        public static UIManager Instance { get { return instance; } }
        private List<UIElement> uiElements = new List<UIElement>();
        private SpriteFont spriteFont;
        private int id = 0;

        private UIManager() {
        }

        public void LoadContent() {
            spriteFont = GameObject.Content.Load<SpriteFont>("Fonts/Basic");
        }

        public int AddElement(string message, Vector2 position) {
            UIElement element = new UIElement(position);
            element.text = message;
            uiElements.Add(element);
            return id++;
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (UIElement element in uiElements) {
                spriteBatch.DrawString(spriteFont, element.text, element.position, Color.White);
            }
        }

        public void UpdateElement(int id, string message) {
            UIElement uiElement = uiElements[id];
            uiElement.text = message;
            uiElements[id] = uiElement;
        }
    }
}