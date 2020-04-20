using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YetAnotherAutoChess.Presentation
{
    class FigureUIManager
    {
        private string absolutePath;
        private bool _isPawn;
        StatusBarInfo StatusBarInfo;
        public Image OneStar;
        public Image TwoStar;
        public Image ThreeStar;
        public FigureUIManager()
        {
            StatusBarInfo = new StatusBarInfo();
            setImageSources();
        }
        private void setImageSources()
        {
            OneStar.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(absolutePath + "/Images/UIstuff/Stars/bronze.png", UriKind.Absolute));
            TwoStar.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(absolutePath + "/Images/UIstuff/Stars/silver.png", UriKind.Absolute));
            ThreeStar.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(absolutePath + "/Images/UIstuff/Stars/gold.png", UriKind.Absolute));
        }

        public void PromoteToOneStar()
        {
            this.StatusBarInfo.Stars.Source = OneStar.Source;
        }

        public void PromoteToTwoStar()
        {
            this.StatusBarInfo.Stars.Source = TwoStar.Source;
        }

        public void PromoteToThreeStar()
        {
            this.StatusBarInfo.Stars.Source = ThreeStar.Source;
        }

        public void SetPieceToggleText(Enums.Piece piece)
        {
            System.Text.StringBuilder tooltip = new System.Text.StringBuilder();
            switch (piece)
            {
                case Enums.Piece.Pawn:
                    //ToggleDisable();
                    _isPawn = true;
                    tooltip.Append("Pawn");
                    tooltip.AppendLine("No bonuses");
                    //PieceToggle.image.sprite = ImgPawn;
                    break;
                case Enums.Piece.Bishop:
                    tooltip.Append("Bishop");
                    tooltip.AppendLine("OFF: Start battle with 100% mana");
                    tooltip.AppendLine("ON: Get 2X mana");
                    //PieceToggle.image.sprite = ImgBishop;
                    break;
                case Enums.Piece.Knight:
                    tooltip.Append("Knight");
                    tooltip.AppendLine("Passive: get bonus attack speed");
                    tooltip.AppendLine("OFF: Transcend grand distances quickly");
                    tooltip.AppendLine("ON: Double the Range");
                    //PieceToggle.image.sprite = ImgKnight;
                    break;
                case Enums.Piece.Rook:
                    tooltip.Append("Rook");
                    tooltip.AppendLine("Passive: get bonus attack damage");
                    tooltip.AppendLine("OFF: Bonus armor, magic resist and attack damage");
                    tooltip.AppendLine("ON: Higher bonuses but unable to cast abilities");
                    //PieceToggle.image.sprite = ImgRook;
                    break;
                case Enums.Piece.Queen:
                    tooltip.Append("Queen");
                    tooltip.AppendLine("OFF: Bonus on all stats");
                    tooltip.AppendLine("ON: On start of the battle get shield equal to 100% of HP");
                    //PieceToggle.image.sprite = ImgQueen;
                    break;
            }
            //PieceToggle.GetComponent<FigureTooltip>().SetTooltip(tooltip.ToString());
        }
    }
}
