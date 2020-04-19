using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Animations;
using HelixToolkit.Wpf.SharpDX.Assimp;
using HelixToolkit.Wpf.SharpDX.Controls;
using HelixToolkit.Wpf.SharpDX.Model;
using HelixToolkit.Wpf.SharpDX.Model.Scene;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using OrthographicCamera = HelixToolkit.Wpf.SharpDX.OrthographicCamera;

namespace YetAnotherAutoChess
{
    class MainViewModel : BaseViewModel
    {
        private string OpenFileFilter = $"{HelixToolkit.Wpf.SharpDX.Assimp.Importer.SupportedFormatsString}";
        private bool showWireframe = false;
        public bool ShowWireframe
        {
            set
            {
                if (SetValue(ref showWireframe, value))
                {
                    ShowWireframeFunct(value);
                }
            }
            get
            {
                return showWireframe;
            }
        }

        private bool renderFlat = false;
        public bool RenderFlat
        {
            set
            {
                if (SetValue(ref renderFlat, value))
                {
                    RenderFlatFunct(value);
                }
            }
            get
            {
                return renderFlat;
            }
        }

        private bool renderEnvironmentMap = true;
        public bool RenderEnvironmentMap
        {
            set
            {
                if (SetValue(ref renderEnvironmentMap, value) && scene != null && scene.Root != null)
                {
                    foreach (var node in scene.Root.Traverse())
                    {
                        if (node is MaterialGeometryNode m && m.Material is PBRMaterialCore material)
                        {
                            material.RenderEnvironmentMap = value;
                            material.EmissiveColor = new SharpDX.Color4(255,0, 0, 0);
                        }
                    }
                }
            }
            get => renderEnvironmentMap;
        }

        private bool isLoading = false;
        public bool IsLoading
        {
            private set => SetValue(ref isLoading, value);
            get => isLoading;
        }

        private bool enableAnimation = false;
        public bool EnableAnimation
        {
            set
            {
                if (SetValue(ref enableAnimation, value))
                {
                    if (value)
                    {
                        StartAnimation();
                    }
                    else
                    {
                        StopAnimation();
                    }
                }
            }
            get { return enableAnimation; }
        }

        public ObservableCollection<Animation> Animations { get; } = new ObservableCollection<Animation>();

        public SceneNodeGroupModel3D GroupModel { get; } = new SceneNodeGroupModel3D();

        private Animation selectedAnimation = null;
        public Animation SelectedAnimation
        {
            set
            {
                if (SetValue(ref selectedAnimation, value))
                {
                    StopAnimation();
                    if (value != null)
                    {
                        animationUpdater = new NodeAnimationUpdater(value);
                    }
                    else
                    {
                        animationUpdater = null;
                    }
                    if (enableAnimation)
                    {
                        StartAnimation();
                    }
                }
            }
            get
            {
                return selectedAnimation;
            }
        }

        public TextureModel EnvironmentMap { get; }

        private SynchronizationContext context = SynchronizationContext.Current;
        private HelixToolkitScene scene;
        private NodeAnimationUpdater animationUpdater;
        private List<BoneSkinMeshNode> boneSkinNodes = new List<BoneSkinMeshNode>();
        private List<BoneSkinMeshNode> skeletonNodes = new List<BoneSkinMeshNode>();
        private CompositionTargetEx compositeHelper = new CompositionTargetEx();


        public MainViewModel(string path)
        {
            OriginalPositions = new List<SharpDX.Matrix>();
            OpenFile(path);
        }

        private List<SharpDX.Matrix> OriginalPositions;

        private void OpenFile(string path)
        {
            if (isLoading)
            {
                return;
            }
            if (path == null)
            {
                return;
            }
            StopAnimation();

            var loader = new Importer();
            scene = loader.Load(path);
            Animations.Clear();
            GroupModel.Clear();

            if (scene != null)
            {
                if (scene.Root != null)
                {
                    foreach (var node in scene.Root.Traverse())
                    {
                        if (node is MaterialGeometryNode m)
                        {
                            OriginalPositions.Add(m.ModelMatrix);

                            if (m.Material is PBRMaterialCore pbr)
                            {
                                pbr.RenderEnvironmentMap = RenderEnvironmentMap;
                            }
                            else if (m.Material is PhongMaterialCore phong)
                            {
                                phong.RenderEnvironmentMap = RenderEnvironmentMap;
                            }
                        }
                    }
                }
                GroupModel.AddNode(scene.Root);
                if (scene.HasAnimation)
                {
                    foreach (var ani in scene.Animations)
                    {
                        Animations.Add(ani);
                    }
                }
                foreach (var n in scene.Root.Traverse())
                {
                    n.Tag = new AttachedNodeViewModel(n);
                }
            }
            GroupModel.IsHitTestVisible = false;
        }

        public void EnableHitTest(bool isHittable)
        {
            GroupModel.IsHitTestVisible = isHittable;
        }

        public void SetActive(bool active)
        {
            GroupModel.IsEnabled = active;
        }

        public void MoveTo(Point3D point)
        {
            int i = 0;

            foreach (var node in scene.Root.Traverse())
            {
                if (node is MaterialGeometryNode m)
                {
                    SharpDX.Matrix originalPosition = OriginalPositions[i++];
                    m.ModelMatrix = new SharpDX.Matrix(originalPosition.M11,
                                                       originalPosition.M12,
                                                       originalPosition.M13,
                                                       originalPosition.M14,
                                                       originalPosition.M21,
                                                       originalPosition.M22,
                                                       originalPosition.M23,
                                                       originalPosition.M24,
                                                       originalPosition.M31,
                                                       originalPosition.M32,
                                                       originalPosition.M33,
                                                       originalPosition.M34,
                                                       originalPosition.M41 + (float)point.X,
                                                       originalPosition.M42 + (float)point.Y,
                                                       originalPosition.M43,
                                                       originalPosition.M44 + (float)point.Z);
                }
            }
        }

        public Point3D Position()
        {
            return new Point3D(
                scene.Root.ModelMatrix.ToMatrix3D().OffsetX,
                scene.Root.ModelMatrix.ToMatrix3D().OffsetY,
                scene.Root.ModelMatrix.ToMatrix3D().OffsetZ);
        }

        public void ChangeColor(Vector3D color)
        {
            foreach (var node in scene.Root.Traverse())
            {
                if (node is MaterialGeometryNode m)
                {
                    if (m.Material is PBRMaterialCore pbr)
                    {
                        pbr.RenderEnvironmentMap = RenderEnvironmentMap;
                    }
                    else if (m.Material is PhongMaterialCore phong)
                    {
                        phong.DiffuseColor = new SharpDX.Color4((float)color.X, (float)color.Y, (float)color.Z, 1);
                        phong.RenderEnvironmentMap = RenderEnvironmentMap;
                    }
                }
            }
        }

        public static void ChangeColorForNode(Vector3D color, SceneNode sceneNode)
        {
            foreach (var node in sceneNode.Traverse())
            {
                if (node is MaterialGeometryNode m)
                {
                    if (m.Material is PBRMaterialCore pbr)
                    {
                        //pbr.RenderEnvironmentMap = RenderEnvironmentMap;
                    }
                    else if (m.Material is PhongMaterialCore phong)
                    {
                        phong.DiffuseColor = new SharpDX.Color4(0, 255, 0, 1);
                        //phong.RenderEnvironmentMap = RenderEnvironmentMap;
                    }
                }
            }
        }

        public void StartAnimation()
        {
            compositeHelper.Rendering += CompositeHelper_Rendering;
        }

        public void StopAnimation()
        {
            compositeHelper.Rendering -= CompositeHelper_Rendering;
        }

        private void CompositeHelper_Rendering(object sender, System.Windows.Media.RenderingEventArgs e)
        {
            if (animationUpdater != null)
            {
                animationUpdater.Update(Stopwatch.GetTimestamp(), Stopwatch.Frequency);
                animationUpdater.Animation.RootNode.ModelMatrix = new SharpDX.Matrix(
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M11,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M12,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M13,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M14,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M21,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M22,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M23,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M24,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M31,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M32,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M33,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M34,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M41+4,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M42,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M43,
                                                                       animationUpdater.Animation.RootNode.ModelMatrix.M44);
            }
        }

        private string OpenFileDialog(string filter)
        {
            var d = new OpenFileDialog();
            d.CustomPlaces.Clear();

            d.Filter = filter;

            if (!d.ShowDialog().Value)
            {
                return null;
            }

            return d.FileName;
        }

        private void ShowWireframeFunct(bool show)
        {
            foreach (var node in GroupModel.GroupNode.Items.PreorderDFT((node) =>
            {
                return node.IsRenderable;
            }))
            {
                if (node is MeshNode m)
                {
                    m.RenderWireframe = show;
                }
            }
        }

        private void RenderFlatFunct(bool show)
        {
            foreach (var node in GroupModel.GroupNode.Items.PreorderDFT((node) =>
            {
                return node.IsRenderable;
            }))
            {
                if (node is MeshNode m)
                {
                    if (m.Material is PhongMaterialCore phong)
                    {
                        phong.EnableFlatShading = show;
                    }
                    else if (m.Material is PBRMaterialCore pbr)
                    {
                        pbr.EnableFlatShading = show;
                    }
                }
            }
        }
    }
}
