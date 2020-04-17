using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model.Scene;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace HelixTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            view.Camera = new HelixToolkit.Wpf.SharpDX.OrthographicCamera()
            {
                LookDirection = new System.Windows.Media.Media3D.Vector3D(0, -10, -10),
                Position = new System.Windows.Media.Media3D.Point3D(0, 10, 10),
                UpDirection = new System.Windows.Media.Media3D.Vector3D(0, 1, 0),
                FarPlaneDistance = 5000,
                NearPlaneDistance = 0.1f
            };

            DirectionalLight3D directionalLight3D = new DirectionalLight3D();
            directionalLight3D.Direction = view.Camera.LookDirection;
            directionalLight3D.Color = Color.FromRgb(214, 214, 214);
            view.Items.Add(directionalLight3D);

            view.EffectsManager = new DefaultEffectsManager();

            MainViewModel mainViewModel1 =
                new MainViewModel("C:/Users/Nikola/Documents/JA_AutoChess/JA_AutoChess/GProject/Assets/Mini Legion Lich PBR HP/Animations/Lich/Attack01.fbx");
            MainViewModel mainViewModel2 =
                new MainViewModel("C:/Users/Nikola/Documents/JA_AutoChess/JA_AutoChess/GProject/Assets/Mini Legion Footman HP PBR/Animations/Footman_Attack01.fbx");
            mainViewModel2.MoveTo(new Vector3D(400, 0, 0));
            StatusBarInfo statusBarInfo = new StatusBarInfo();
            MainCanvas.Children.Add(statusBarInfo.canvas);
            List<MainViewModel> models = new List<MainViewModel>();
            models.Add(mainViewModel1);
            models.Add(mainViewModel2);

            foreach (MainViewModel model in models)
            {
                Element3DPresenter element3DPresenter = new Element3DPresenter();
                element3DPresenter.Content = model.GroupModel;
                view.Items.Add(element3DPresenter);
            }
            
            view.AddHandler(Element3D.MouseDown3DEvent, new RoutedEventHandler((s, e) =>
            {
                var arg = e as MouseDown3DEventArgs;

                if (arg.HitTestResult == null)
                {
                    return;
                }
                if (arg.HitTestResult.ModelHit is SceneNode node && node.Tag is AttachedNodeViewModel vm)
                {
                    vm.Selected = !vm.Selected;
                }
            }));
        }
    }
}
