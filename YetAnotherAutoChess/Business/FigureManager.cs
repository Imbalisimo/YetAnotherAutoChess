using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherAutoChess.Business.BoardFigureScripts.Pieces;
using YetAnotherAutoChess.Presentation.FigureUI;

namespace YetAnotherAutoChess.Business
{
    public static class FigureManager
    {
        public static Unit CreateUnit(string name)
        {
            Type type = Type.GetType("YetAnotherAutoChess.Business.BoardFigureScripts.Units." + name);
            Unit unit = (Unit)Activator.CreateInstance(type);
            //Unit unit = (Unit)Activator.CreateInstance("YetAnotherAutoChess.Business.BoardFigureScripts.Units", name).Unwrap();

            unit.MainViewModel = CreateModel(name);
            //unit.MainViewModel = new MainViewModel("./Models/Units/" + name + "/model.fbx");
            return unit;
        }

        public static MainViewModel CreateModel(string name)
        {
            ModelBuilder builder = new ModelBuilder();
            UnitModelBuildDirector director = new UnitModelBuildDirector(builder, name);
            director.Construct();
            return builder.GetResult();
        }

        public static Figure CreateFigure(PlayerServiceReference.BaseUnitPackage unitPackage, Enums.Piece piece)
        {
            Unit unit = CreateUnit(unitPackage.Name);
            return CreateFigure(unit, piece);
        }

        public static Figure CreateFigure(Unit unit, Enums.Piece piece)
        {
            Figure figure = new Figure();

            unit.MainViewModel.SetActive(true);

            IFigureUI figureUI = new FigureUIManager();

            figure.Unit = unit;
            figure.FigureUI = figureUI;
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

        public static Figure CreateFigureFromPackage(PlayerServiceReference.FigurePackage figurePackage)
        {
            Unit unit = CreateUnit(figurePackage.Name);
            Enums.Piece piece = (Enums.Piece)Enum.Parse(typeof(Enums.Piece), figurePackage.Piece);
            Figure figure = CreateFigure(unit, piece);
            figure.ApplyFigurePackageProperties(figurePackage);
            return figure;
        }

        public static List<PlayerServiceReference.BaseUnitPackage> Disassemble(Figure figure)
        {
            List<PlayerServiceReference.BaseUnitPackage> units = new List<PlayerServiceReference.BaseUnitPackage>();
            foreach (Figure sacrifice in figure.Sacrifices)
            {
                units.AddRange(Disassemble(sacrifice));
            }

            Unit unit = figure.Unit;
            units.Add(unit.ToUnitPackage());
            //GameObject UI = figure.FigureUIManager;

            figure.Unit.Destroy();
            figure.Unit = null;

            figure.Destroy();
            //UI.Destroy();

            return units;
        }
    }
}
