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
            mainCanvas.Children.Add(statusBarInfo.canvas);

        }

        private delegate void MouseAction(SceneNode sceneNode);

        private void AddEvent(RoutedEvent mouseEvent, MouseAction Action)
        {
            view.AddHandler(mouseEvent, new RoutedEventHandler((s, e) =>
            {
                var arg = e as MouseDown3DEventArgs;

                if (arg == null)
                    return;

                if (arg.HitTestResult == null)
                {
                    return;
                }
                if (arg.HitTestResult.ModelHit is SceneNode node && node.Tag is AttachedNodeViewModel vm)
                {
                    vm.Selected = !vm.Selected;
                    Action(node);
                }
            }));
        }

    }
}
