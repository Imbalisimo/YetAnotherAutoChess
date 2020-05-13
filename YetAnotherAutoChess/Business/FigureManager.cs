using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherAutoChess.Business.BoardFigureScripts.Pieces;

namespace YetAnotherAutoChess.Business
{
    public static class FigureManager
    {
        public static Unit CreateUnit(string name)
        {
            Type type = Type.GetType("YetAnotherAutoChess.Data.Units." + name);
            Unit unit = (Unit)Activator.CreateInstance(type);
            //Unit unit = (Unit)Activator.CreateInstance("YetAnotherAutoChess.Data.Units", name).Unwrap();

            unit.MainViewModel = new MainViewModel("./Models/Units/" + name + "/model.fbx");
            return unit;
        }

        public static Figure CreateFigure(Unit unit, Enums.Piece piece)
        {
            Figure figure = new Figure();

            unit.MainViewModel.SetActive(true);

            //GameObject unitUI = Resources.Load("FigureUI", typeof(GameObject)) as GameObject;
            //GameObject UI = Instantiate(unitUI, Vector3.zero, Quaternion.identity) as GameObject;

            figure.Unit = unit;
            //figure.FigureUIManager = UI;
            figure.Piece = MakePiece(piece);
            figure.Cost = Calculators.CostCalculator.CalculateFinalCost(unit, piece);

            return figure;
        }

        private static Piece MakePiece(Enums.Piece piece)
        {
            switch (piece)
            {
                case Enums.Piece.Pawn:
                    return new Pawn();
                case Enums.Piece.Bishop:
                    return new Bishop();
                case Enums.Piece.Knight:
                    return new Knight();
                case Enums.Piece.Rook:
                    return new Rook();
                case Enums.Piece.Queen:
                    return new Queen();
                default:
                    return null;
            }
        }

        public static void Disassemble(Figure figure)
        {
            foreach (Figure sacrifice in figure.Sacrifices)
            {
                Disassemble(sacrifice);
            }

            Unit unit = figure.Unit;
            //GameObject UI = figure.FigureUIManager;

            figure.Unit.Destroy();
            figure.Unit = null;

            figure.Destroy();
            //UI.Destroy();
            
        }
    }
}
