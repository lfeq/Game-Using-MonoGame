using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace POOII_Monogame {

    internal class LevelGenerator {
        private int seed = 79;
        private int width, height;
        private List<Terrain> terrainTiles = new List<Terrain>();
        private List<Terrain> rocks = new List<Terrain>();
        private List<Terrain> caves = new List<Terrain>();

        public LevelGenerator(int _width, int _height) {
            width = _width;
            height = _height;
            GenerateLevel();
        }

        private void GenerateLevel() {
            terrainTiles = new List<Terrain>();
            rocks = new List<Terrain>();
            caves = new List<Terrain>();
            Random random = new Random(seed);

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    int type = random.Next(3);
                    Vector2 position = new Vector2(i * 64, j * 64);
                    Terrain terrain = new Terrain();

                    if (type == 0) {
                        terrain = new Grass(position);
                    } //Grass
                    else if (type == 1) {
                        terrain = new Rock(position);
                        rocks.Add(terrain);
                    } //Rock
                    else if (type == 2) {
                        terrain = new Cave(position);
                        caves.Add(terrain);
                    } //Cave

                    terrainTiles.Add(terrain);
                }
            }
        }

        public void LoadContent() {
            foreach (Terrain terrain in terrainTiles) {
                terrain.LoadContent();
            }
        }

        public List<Terrain> GetTerrainList() {
            return terrainTiles;
        }

        public List<Terrain> GetRockList() {
            return rocks;
        }

        public List<Terrain> GetCaveList() {
            return caves;
        }
    }
}