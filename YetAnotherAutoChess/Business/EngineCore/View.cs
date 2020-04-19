using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YetAnotherAutoChess.Business.EngineCore
{
    public static class View
    {
        private static Viewport3DX _view;

        public static void AddView(Viewport3DX view)
        {
            _view = view;
            _view.Camera = new HelixToolkit.Wpf.SharpDX.OrthographicCamera()
            {
                LookDirection = new System.Windows.Media.Media3D.Vector3D(0, -7, -10),
                Position = new System.Windows.Media.Media3D.Point3D(10, 5, 0),
                Width = 50,
                UpDirection = new System.Windows.Media.Media3D.Vector3D(0, 100, 0),
                FarPlaneDistance = 5000,
                NearPlaneDistance = -10f
            };

            DirectionalLight3D directionalLight3D = new DirectionalLight3D();
            directionalLight3D.Direction = view.Camera.LookDirection;
            directionalLight3D.Color = Color.FromRgb(214, 214, 214);
            _view.Items.Add(directionalLight3D);

            view.EffectsManager = new DefaultEffectsManager();
        }

        internal static void AddModel(MainViewModel model)
        {
            Element3DPresenter element3DPresenter = new Element3DPresenter();
            element3DPresenter.Content = model.GroupModel;
            _view.Items.Add(element3DPresenter);
        }
    }
}
