using YetAnotherAutoChess.Business;

namespace YetAnotherAutoChess.Presentation
{
    class UnitImageClick
    {
        public GameObject DisplayedUnit { get; set; }

        public delegate void Buy(UnitImageClick image, GameObject unit, Enums.Piece piece);
        public event Buy OnBuy;

        public void DisplayUnit(GameObject unit)
        {
            DisplayedUnit = unit;
            //Sprite unitImage = Resources.Load(unit.GetComponent<Unit>().GetType().Name +
            //        "/shop/" + unit.GetComponent<Unit>().GetType().Name, typeof(Sprite)) as Sprite;
            //Owner.sprite = unitImage;

            //TextName.text = unit.GetComponent<Unit>().GetType().Name;
            //string synergies = "";
            //foreach (Enums.Synergy synergy in unit.GetComponent<Unit>().Stats.Synergies)
            //    synergies += synergy.ToString();
            //TextSynergy.text = synergies;

            //TextCost.text = unit.GetComponent<Unit>().Cost.ToString();
            //switch (unit.GetComponent<Unit>().Cost)
            //{
            //    case 1:
            //        Frame.sprite = CommonFrame;
            //        break;
            //    case 2:
            //    case 3:
            //        Frame.sprite = UncommonFrame;
            //        break;
            //    case 4:
            //    case 5:
            //        Frame.sprite = RareFrame;
            //        break;
            //    case 6:
            //    case 7:
            //        Frame.sprite = EpicFrame;
            //        break;
            //    default:
            //        Frame.sprite = LegendaryFrame;
            //        break;
            //}
            //if (Frame.gameObject.active != true)
            //    Frame.gameObject.SetActive(true);
            //if (Owner.gameObject.active != true)
            //    Owner.gameObject.SetActive(true);
        }
    }
}
