using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Diagnostics;

namespace FormsSkiaBench
{
    public partial class Form1 : Form
    {
        private static readonly SKPaint _paint = new()
        {
            Color = SKColors.Red,
            StrokeWidth = 1.5f,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        private readonly SKGLControl _gl = new()
        {
            Dock = DockStyle.Fill,
            Size = new Size(50, 50),
            VSync = true,

        };
        private readonly Stopwatch _fpsSw = new();
        private int _fps = 0;
        private SKPoint _point = new();
        public Form1()
        {
            InitializeComponent();
            _gl.PaintSurface += gl_PaintSurface;
            _gl.MouseMove += gl_MouseMove;
            this.Controls.Add(_gl);
            _fpsSw.Start();
        }

        public void DoRender() => _gl.Refresh();

        private void gl_MouseMove(object? sender, MouseEventArgs e)
        {
            _point = new(e.X, e.Y);
        }

        private void gl_PaintSurface(object? sender, SKPaintGLSurfaceEventArgs e)
        {
            if (_fpsSw.ElapsedMilliseconds >= 1000)
            {
                int fps = Interlocked.Exchange(ref _fps, 0);
                this.Text = $"{fps} FPS";
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