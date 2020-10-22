using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business
{
    public interface IModelBuilder
    {
        string Prefix { get; set; }
        string Name { get; set; }
        string Suffix { get; set; }

        MainViewModel GetResult();
    }

    class ModelBuilder : IModelBuilder
    {
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Suffix { get; set; }

        public MainViewModel GetResult()
        {
            return new MainViewModel(Prefix + Name + Suffix);
        }
    }

    public class TileModelBuildDirector
    {
        private IModelBuilder _builder;
        private Enums.Tile _tile;
        public TileModelBuildDirector(IModelBuilder builder, Enums.Tile tile)
        {
            _builder = builder;
            _tile = tile;
        }

        public void Construct()
        {
            _builder.Prefix = "./Models/Tiles/";
            _builder.Name = _tile == Enums.Tile.BenchTile ? "BenchTile" : "Tile";
            _builder.Suffix = ".fbx";
        }
    }

    public class UnitModelBuildDirector
    {
        private IModelBuilder _builder;
        private string _name;
        public UnitModelBuildDirector(IModelBuilder builder, string name)
        {
            _builder = builder;
            _name = name;
        }

        public void Construct()
        {
            _builder.Prefix = "./Models/Units/";
            _builder.Name = _name;
            _builder.Suffix = "/model.fbx";
        }
    }
}
