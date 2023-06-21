using SkiaSharp;
using SkiaSharp.Views.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfSkia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly SKPaint _paint = new()
        {
            Color = SKColors.Red,
            StrokeWidth = 1.5f,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        private readonly SKGLElement _gl = new()
        {
            RenderContinuously = true
        };
        private readonly Stopwatch _fpsSw = new();
        private int _fps = 0;
        private SKPoint _point = new();

        public MainWindow()
        {
            InitializeComponent();
            GLContainer.Child = _gl;
            _gl.PaintSurface += gl_PaintSurface;
            _gl.MouseMove += gl_MouseMove;
            _fpsSw.Start();

        }

        private void gl_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(_gl);
            _point = new SKPoint((float)pos.X, (float)pos.Y);
        }

        private void gl_PaintSurface(object? sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            if (_fpsSw.ElapsedMilliseconds >= 1000)
            {
                int fps = Interlocked.Exchange(ref _fps, 0);
                this.Title = $"{fps} FPS";
                _fpsSw.Restart();
            }
            else
                _fps++;
            var canvas = e.Surface.Canvas;
            canvas.Clear();
            canvas.DrawCircle(_point, 5f, _paint);
            canvas.Flush();
        }
    }
}
