using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace POOII_Monogame {

    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameStates _gameState;
        private GameManager _gameManager;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameObject.Content = Content;
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            TextureManager.Instance.LoadTexture();
            _gameManager = new GameManager();
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 576;
            _graphics.ApplyChanges();
            _gameManager.Initialize();
            //UIManager.instance.AddElement("Level 1", new Vector2(300, 300));

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameState = GameStates.GamePlay;
            _gameManager.LoadContent();
            UIManager.Instance.LoadContent();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            if (_gameState == GameStates.GamePlay) {
                _gameManager.Update(gameTime);
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (_gameState == GameStates.Loading) {
                Console.WriteLine("Loading...");
            }

            if (_gameState == GameStates.GamePlay) {
                _gameManager.Draw(_spriteBatch);
                UIManager.Instance.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

public enum GameStates {
    GamePlay,
    Loading,
    Pause
}