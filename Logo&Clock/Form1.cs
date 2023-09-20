namespace Logo_Clock
{
    public partial class LogoForm : Form
    {
        public LogoForm()
        {
            InitializeComponent();
            Paint += new PaintEventHandler(LogoForm_Paint);

            Paint += new PaintEventHandler(ClockForm_Paint);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void LogoForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen blackPen = new Pen(Color.Black, 10);
            g.DrawEllipse(blackPen, 40, 40, 320, 320);

            Brush greenBrush = new SolidBrush(Color.Green);
            g.FillEllipse(greenBrush, 50, 50, 300, 300);

            Font fontT = new Font("Arial", 120, FontStyle.Bold);
            Brush whiteBrush = new SolidBrush(Color.Black);
            g.DrawString("T", fontT, whiteBrush, 90, 80);

            Font font = new Font("Arial", 9);
            Brush blackBrush = new SolidBrush(Color.Black);
            PointF pointF = new PointF(200f, 110f);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString("ФК ТОРПЕДО МИКОЛАЇВ", font, blackBrush, pointF, stringFormat);

            string imagePath = Path.Combine(Application.StartupPath, "..\\..\\..\\logo\\football-ball.png");

            Image image1 = Image.FromFile(imagePath);

            Rectangle destRect1 = new Rectangle(240, 180, 100, 100);
            e.Graphics.DrawImage(image1, destRect1);

            Image image2 = Image.FromFile(imagePath);

            Rectangle destRect2 = new Rectangle(60, 180, 100, 100);
            e.Graphics.DrawImage(image2, destRect2);

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void ClockForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Малювання циферблата
            Pen blackPen = new Pen(Color.Black, 10);
            g.DrawEllipse(blackPen, 400, 50, 300, 300);

            // Малювання розділень
            for (int i = 0; i < 60; i++)
            {
                double angleD = i * 6 - 90;
                int innerLength = i % 5 == 0 ? 130 : 140;
                int outerLength = 150;
                DrawHandForDivision(g, angleD, innerLength, outerLength, Color.Black);
            }

            // Малювання цифр
            Font font = new Font("Arial", 20);
            Brush blackBrush = new SolidBrush(Color.Black);
            for (int i = 1; i <= 12; i++)
            {
                double angleN = i * 29.5 - 90;
                double radian = angleN * Math.PI / 180;
                PointF center = new PointF(530f, 180f);
                float x = center.X + (130 - font.Size) * (float)Math.Cos(radian);
                float y = center.Y + (130 - font.Size) * (float)Math.Sin(radian);
                g.DrawString(i.ToString(), font, blackBrush, x, y);
            }

            // Малювання стрілок
            DateTime now = DateTime.Now;
            int second = now.Second;
            int minute = now.Minute;
            int hour = now.Hour;

            double angle = second * 6 - 90;
            DrawHandForArrows(g, angle, 140, Color.Red);

            angle = minute * 6 + second * 0.1 - 90;
            DrawHandForArrows(g, angle, 120, Color.Black);

            angle = hour * 30 + minute * 0.5 - 90;
            DrawHandForArrows(g, angle, 80, Color.Black);
        }

        private void DrawHandForDivision(Graphics g, double angle, int innerLength, int outerLength, Color color)
        {
            double radian = angle * Math.PI / 180;
            PointF center = new PointF(550f, 200f);
            PointF innerPoint = new PointF(
                center.X + innerLength * (float)Math.Cos(radian),
                center.Y + innerLength * (float)Math.Sin(radian)
                );
            PointF outerPoint = new PointF(
                center.X + outerLength * (float)Math.Cos(radian),
                center.Y + outerLength * (float)Math.Sin(radian)
                );

            Pen pen = new Pen(color, 1);
            g.DrawLine(pen, innerPoint, outerPoint);
        }
        private void DrawHandForArrows(Graphics g, double angle, int length, Color color)
        {
            double radian = angle * Math.PI / 180;
            PointF center = new PointF(550f, 200f);
            PointF end = new PointF(
                center.X + length * (float)Math.Cos(radian),
                center.Y + length * (float)Math.Sin(radian)
                );

            Pen pen = new Pen(color, 3);
            g.DrawLine(pen, center, end);
        }

    }
}