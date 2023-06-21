using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace POOII_Monogame {

    internal class GameManager {
        private float _currentTime = 0;
        private List<Enemy> _enemyList;
        private Player _player;
        private Physics _physics;
        private Turret _turret;
        private LevelGenerator _levelGenerator;
        private List<PowerUp> _powerUps;

        public GameManager() {
            _enemyList = new List<Enemy>();
            _player = new Player(new Vector2(100, 100));
            _physics = new Physics();
            _turret = new Turret(new Vector2(400, 500));
            _powerUps = new List<PowerUp>();
        }

        public void Initialize() {
            _levelGenerator = new LevelGenerator(16, 9);
        }

        public void LoadContent() {
            _player.LoadContent();
            _turret.LoadContent();
            _levelGenerator.LoadContent();
        }

        public void Update(GameTime gameTime) {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _currentTime += elapsedTime;
            if (_currentTime >= 3f) {
                SpawnEnemy();
                _currentTime = 0;
            }
            foreach (Enemy enemy in _enemyList) {
                enemy.Update(gameTime);
            }
            _player.Update(gameTime);
            _turret?.Update(gameTime, _player.position);
            Enemy.SetTarget(_player.position);
            CollisionDetection();
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Terrain tile in _levelGenerator.GetTerrainList()) {
                spriteBatch.Draw(tile.GetSprite(), tile.position, Color.White);
            }
            foreach (Enemy enemy in _enemyList) {
                spriteBatch.Draw(enemy.GetSprite(), enemy.position, Color.White);
            }
            foreach (PowerUp powerUp in _powerUps) {
                powerUp.Draw(spriteBatch);
            }
            _player.Draw(spriteBatch);
            _turret.Draw(spriteBatch);
        }

        private void SpawnEnemy() {
            List<Terrain> cavesTemp = new List<Terrain>();
            cavesTemp = _levelGenerator.GetCaveList();
            Random random = new Random();
            int randomIndex = random.Next(0, cavesTemp.Count);
            Vector2 spawnPos = cavesTemp[randomIndex].position;
            Enemy tempEnemy = new Enemy(spawnPos);
            _enemyList.Add(tempEnemy);
        }

        private void CollisionDetection() {
            List<Projectile> playerPorjectiles = _player.GetProjectiles();
            for (int i = playerPorjectiles.Count - 1; i >= 0; i--) {
                Projectile projectile = playerPorjectiles[i];
                foreach (Enemy enemy in _enemyList) {
                    if (_physics.IsColliding(projectile.boxCollider, enemy.boxCollider)) {
                        enemy.isAlive = false;
                        _player.score++;
                        _player.RemovePorjectile(projectile);
                    }
                } //Hit enemy
                if (_physics.IsColliding(projectile.boxCollider, _turret.boxCollider)) {
                    if (_turret.isAlive) {
                        _turret.isAlive = false;
                        _player.score++;
                        Random random = new Random();
                        double randomNumber = random.NextDouble();
                        if (randomNumber < 0.5) {
                            HealthBuff tempHealthBuff = new HealthBuff(_turret.position);
                            _powerUps.Add(tempHealthBuff);
                        } else {
                            SpeedBuff tempSpeedBuff = new SpeedBuff(_turret.position);
                            _powerUps.Add(tempSpeedBuff);
                        }
                        _player.RemovePorjectile(projectile);
                    }
                } //Hit turret
            }
            foreach (Enemy enemy in _enemyList) {
                if (_physics.IsColliding(_player.boxCollider, enemy.boxCollider)) {
                    _player.health -= 10;
                    enemy.isAlive = false;
                    if (_player.health <= 0) {
                        PlayerData playerData = new PlayerData();
                        playerData.score = _player.score;
                        JSON.WriteJSONFile(playerData);
                        Environment.Exit(0);
                    }
                }
            }
            foreach (PowerUp powerUp in _powerUps) {
                if (_physics.IsColliding(_player.boxCollider, powerUp.boxCollider)) {
                    if (powerUp.GetType() == typeof(HealthBuff)) {
                        _player.health += 100;
                        powerUp.isAlive = false;
                    }
                    if (powerUp.GetType() == typeof(SpeedBuff)) {
                        _player.speed += 3;
                        powerUp.isAlive = false;
                    }
                }
            }

            List<Projectile> turretProjectiles = _turret.GetProjectiles();
            for (int i = turretProjectiles.Count - 1; i >= 0; i--) {
                Projectile projectile = turretProjectiles[i];
                if (_physics.IsColliding(projectile.boxCollider, _player.boxCollider)) {
                    _player.health -= 30;
                    _turret.RemovePorjectile(projectile);
                }
            }

            //Recolector de basura
            for (int i = 0; i < _enemyList.Count; i++) {
                if (!_enemyList[i].isAlive) {
                    _enemyList.RemoveAt(i);
                }
            }

            for (int i = 0; i < _powerUps.Count; i++) {
                PowerUp powerUp = _powerUps[i];
                if (!powerUp.isAlive) {
                    _powerUps.Remove(powerUp);
                }
            }
        }
    }
}