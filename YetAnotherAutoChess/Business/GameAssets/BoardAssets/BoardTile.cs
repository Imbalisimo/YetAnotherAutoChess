using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace YetAnotherAutoChess.Business.GameAssets.BoardAssets
{
    class BoardTile : GameObject
    {
        public Point Position;
        private static int TileSize = 2;
        private static int TileSizeForUnits = 300;
        //public Figure Figure;
        public BoardTile() { }

        public BoardTile(Point Position, int typeOfTile = 0)
        {
            this.Position = Position;
            this.MainViewModel = new MainViewModel("C:/Users/Marin/Desktop/Unitythings/platforma4.fbx");//("./Tile/" + (typeOfTile == 0 ? "BenchTile" : "Tile"));
            this.MainViewModel.MoveTo(new Point3D(Position.X * TileSize, Position.Y * TileSize, 0));
            ChangeColor(BoardTile.DefaultColor);
        }

        public static Color DefaultColor = Colors.Gray;
        public void ChangeColor(Color color)
        {
            this.MainViewModel.ChangeColor(new Vector3D(color.R, color.G, color.B));
        }

        private static BoardTile[,] _boardTiles = new BoardTile[9, 8];

        public BoardTile this[int i, int j]
        {
            get { return _boardTiles[i + 1, j]; }
            set { _boardTiles[i + 1, j] = value; }
        }

        public Point GetTileCenter()
        {
            return new Point(Position.X * TileSizeForUnits, Position.Y * TileSizeForUnits);
        }

        public override void Update()
        {
            
        }
    }
}

