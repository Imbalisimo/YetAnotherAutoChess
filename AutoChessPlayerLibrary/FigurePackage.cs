using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoChessPlayerLibrary
{
    [DataContract]
    public class FigurePackage
    {
        private string _unitName;
        private string _star;
        private string _piece;
        private bool _pieceToggled;
        private int _originalRow;
        private int _originalColumn;
        private int _newRow;
        private int _newColumn;

        [DataMember]
        public string Name { get => _unitName; set => _unitName = value; }
        [DataMember]
        public string Star { get => _star; set => _star = value; }
        [DataMember]
        public string Piece { get => _piece; set => _piece = value; }
        [DataMember]
        public bool PieceToggled { get => _pieceToggled; set => _pieceToggled = value; }
        [DataMember]
        public int OriginalRow { get => _originalRow; set => _originalRow = value; }
        [DataMember]
        public int OriginalColumn { get => _originalColumn; set => _originalColumn = value; }
        [DataMember]
        public int NewRow { get => _newRow; set => _newRow = value; }
        [DataMember]
        public int NewColumn { get => _newColumn; set => _newColumn = value; }
    }
}
