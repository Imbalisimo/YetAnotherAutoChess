using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model.Scene;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using YetAnotherAutoChess.Business.GameAssets.BoardAssets;

namespace YetAnotherAutoChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Business.EngineCore.View.AddView(view);

            InitializeAssets();

            AddEvent(Element3D.MouseDown3DEvent, PieceMover.OnLeftMouseButtonDown);
            AddEvent(Element3D.MouseMove3DEvent, PieceMover.OnLeftMouseButtonHold);
            AddEvent(Element3D.MouseUp3DEvent, PieceMover.OnLeftMouseButtonUp);
        }

        private void InitializeAssets()
        {
            Business.GameAssets.MatchManager matchManager = new Business.GameAssets.MatchManager();
            DPSmanager dpsManager = new DPSmanager();
            Business.GameAssets.Board.Initialize();
            Business.GameAssets.Player player = new Business.GameAssets.Player();
            Business.GameAssets.UnitShop.Initialize(mainGrid);
            PlayersUI playersUI = new PlayersUI(mainGrid);
            TextGrid.Children.Add(matchManager.Text);
            StatusBarInfo statusBarInfo = new StatusBarInfo();
            mainGrid.Children.Add(statusBarInfo.canvas);
        }

        private delegate void MouseAction(SceneNode sceneNode);

        private void AddEvent(RoutedEvent mouseEvent, MouseAction Action)
        {
            view.AddHandler(mouseEvent, new RoutedEventHandler((s, e) =>
            {
                var arg = e as Mouse3DEventArgs;
                if (arg == null)
                    return;

                var hits = view.FindHits(arg.Position.ToVector2());
                if (hits == null)
                    return;

                foreach(var hit in hits)
                    if (hit.ModelHit is SceneNode node && node.Tag is AttachedNodeViewModel vm)
                    {
                        vm.Selected = !vm.Selected;
                        Action(node);
                    }
            }));
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)

        {

            //----------------< Canvas_SizeChanged() >----------------

            Canvas canvas = sender as Canvas;

            SizeChangedEventArgs canvas_Changed_Args = e;



            //< check >

            //*if size=0 then initial

            if (canvas_Changed_Args.PreviousSize.Width == 0) return;

            //</ check >



            //< init >

            double old_Height = canvas_Changed_Args.PreviousSize.Height;

            double new_Height = canvas_Changed_Args.NewSize.Height;

            double old_Width = canvas_Changed_Args.PreviousSize.Width;

            double new_Width = canvas_Changed_Args.NewSize.Width;



            double scale_Width = new_Width / old_Width;

            double scale_Height = new_Height / old_Height;

            //</ init >





            //----< adapt all children >----

            foreach (FrameworkElement element in canvas.Children)

            {

                //< get >

                double old_Left = Canvas.GetLeft(element);

                double old_Top = Canvas.GetTop(element);

                //</ get >



                // < set Left-Top>

                Canvas.SetLeft(element, old_Left * scale_Width);

                Canvas.SetTop(element, old_Top * scale_Height);

                // </ set Left-Top >



                //< set Width-Heigth >

                element.Width = element.Width * scale_Width;

                element.Height = element.Height * scale_Height;

                //</ set Width-Heigth >

            }

            //----</ adapt all children >----



            //----------------</ Canvas_SizeChanged() >----------------

        }
    }
}
