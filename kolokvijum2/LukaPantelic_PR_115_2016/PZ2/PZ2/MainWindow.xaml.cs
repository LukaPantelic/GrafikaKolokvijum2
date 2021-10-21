using PZ2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace PZ2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region polja
        private int pointOffset = 1;
        private int nodeSize = 1;
        private int gridSize = 300;

        private Dictionary<double, int> mapX;
        private Dictionary<double, int> mapY;

        private SolidColorBrush substationColor = Brushes.Yellow; 
        private SolidColorBrush nodeColor = Brushes.Navy;
        private SolidColorBrush switchColor = Brushes.Blue;
        private SolidColorBrush mixedColor = Brushes.OrangeRed;

        private List<Grid> gridPoints = new List<Grid>();
        private List<Line> lines = new List<Line>();
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            LoadXML();
            Draw();
            DrawLine();
        }
        #region MAIN
        private void LoadXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Geographic.xml");

            XmlNodeList subList;
            subList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Substations/SubstationEntity");

            XmlNodeList nodeList;
            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Nodes/NodeEntity");

            XmlNodeList switchList;
            switchList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Switches/SwitchEntity");

            double minX, maxX, minY, maxY;
            GetLimit(subList, nodeList, switchList, out minX, out maxX, out minY, out maxY);

            mapX = GetCordinates(minX, maxX, gridSize);
            mapY = GetCordinates(minY, maxY, gridSize);

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    var point = new Grid
                    {
                        X = i,
                        Y = j
                    };

                    gridPoints.Add(point);
                }
            }

            foreach (XmlNode node in subList)
            {
                SubstationEntity sub = new SubstationEntity();

                sub.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                sub.Name = node.SelectSingleNode("Name").InnerText;
                sub.X = double.Parse(node.SelectSingleNode("X").InnerText);
                sub.Y = double.Parse(node.SelectSingleNode("Y").InnerText);

                double decimalX, decimalY;
                int gridX = 0, gridY = 0;

                ToLatLon(sub.X, sub.Y, 34, out decimalX, out decimalY);

                foreach (KeyValuePair<double, int> kvp in mapX)
                {
                    if (decimalX >= kvp.Key)
                    {
                        gridX = kvp.Value;
                    }
                }

                foreach (KeyValuePair<double, int> kvp in mapY)
                {
                    if (decimalY >= kvp.Key)
                    {
                        gridY = kvp.Value;
                    }
                }

                var point = gridPoints.Where(p => p.X == gridX && p.Y == gridY).FirstOrDefault();
                int pointIndex = gridPoints.IndexOf(point);
                gridPoints[pointIndex].Entities.Add(sub);
            }

            foreach (XmlNode node in nodeList)
            {
                NodeEntity n = new NodeEntity();

                n.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                n.Name = node.SelectSingleNode("Name").InnerText;
                n.X = double.Parse(node.SelectSingleNode("X").InnerText);
                n.Y = double.Parse(node.SelectSingleNode("Y").InnerText);

                double decimalX, decimalY;
                int gridX = 0, gridY = 0;

                ToLatLon(n.X, n.Y, 34, out decimalX, out decimalY);

                foreach (KeyValuePair<double, int> kvp in mapX)
                {
                    if (decimalX >= kvp.Key)
                    {
                        gridX = kvp.Value;
                    }
                }

                foreach (KeyValuePair<double, int> kvp in mapY)
                {
                    if (decimalY >= kvp.Key)
                    {
                        gridY = kvp.Value;
                    }
                }

                var point = gridPoints.Where(p => p.X == gridX && p.Y == gridY).FirstOrDefault();
                int pointIndex = gridPoints.IndexOf(point);
                gridPoints[pointIndex].Entities.Add(n);
            }

            foreach (XmlNode node in switchList)
            {
                SwitchEntity s = new SwitchEntity();

                s.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                s.Name = node.SelectSingleNode("Name").InnerText;
                s.X = double.Parse(node.SelectSingleNode("X").InnerText);
                s.Y = double.Parse(node.SelectSingleNode("Y").InnerText);

                double decimalX, decimalY;
                int gridX = 0, gridY = 0;

                ToLatLon(s.X, s.Y, 34, out decimalX, out decimalY);

                foreach (KeyValuePair<double, int> kvp in mapX)
                {
                    if (decimalX >= kvp.Key)
                    {
                        gridX = kvp.Value;
                    }
                }

                foreach (KeyValuePair<double, int> kvp in mapY)
                {
                    if (decimalY >= kvp.Key)
                    {
                        gridY = kvp.Value;
                    }
                }

                var point = gridPoints.Where(p => p.X == gridX && p.Y == gridY).FirstOrDefault();
                int pointIndex = gridPoints.IndexOf(point);
                gridPoints[pointIndex].Entities.Add(s);
            }


        }
        private void Draw()
        {
            foreach (var point in gridPoints)
            {
                if (point.Entities.Count == 1)
                {
                    Ellipse shape = new Ellipse()
                    {
                        Height = nodeSize,
                        Width = nodeSize,
                        Name = "id" + point.Entities[0].Id.ToString(),
                        ToolTip = "Id: " + point.Entities[0].Id + ", Name: " + point.Entities[0].Name
                    };

                    if (point.Entities[0] is SubstationEntity)
                    {
                        shape.Fill = substationColor;
                    }
                    else if (point.Entities[0] is NodeEntity)
                    {
                        shape.Fill = nodeColor;
                    }
                    else if (point.Entities[0] is SwitchEntity)
                    {
                        shape.Fill = switchColor;
                    }

                    shape.MouseLeftButtonDown += Shape_LeftClick;

                    point.Shape = shape;
                    Canvas.Children.Add(shape);

                    Canvas.SetTop(shape, point.X * pointOffset);
                    Canvas.SetLeft(shape, point.Y * pointOffset);
                }
                else if (point.Entities.Count > 1)
                {
                    Ellipse shape = new Ellipse()
                    {
                        Height = nodeSize,
                        Width = nodeSize,
                        Fill = mixedColor
                    };

                    shape.MouseLeftButtonDown += Shape_LeftClick;

                    shape.ToolTip = "";
                    foreach (var entity in point.Entities)
                    {
                        shape.ToolTip += "Id: " + entity.Id + ", Name: " + entity.Name + System.Environment.NewLine;
                    }

                    point.Shape = shape;
                    Canvas.Children.Add(shape);

                    Canvas.SetTop(shape, point.X * pointOffset);
                    Canvas.SetLeft(shape, point.Y * pointOffset);
                }
            }
        }

        private void DrawLine()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Geographic.xml");
            XmlNodeList lineList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Lines/LineEntity");

            foreach (XmlNode node in lineList)
            {
                LineEntity l = new LineEntity();

                l.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                l.Name = node.SelectSingleNode("Name").InnerText;
                l.FirstEnd = long.Parse(node.SelectSingleNode("FirstEnd").InnerText);
                l.SecondEnd = long.Parse(node.SelectSingleNode("SecondEnd").InnerText);

                string toolTipText = "Id: " + l.Id + ", Name: " + l.Name;

                Connect(l.FirstEnd, l.SecondEnd, l.Name, toolTipText);
            }
        }

        #region pomocne funkcije

        public static void GetLimit(XmlNodeList substationList, XmlNodeList nodeList, XmlNodeList switchList, out double minX, out double maxX, out double minY, out double maxY)
        {
            minX = double.MaxValue;
            minY = double.MaxValue;
            maxX = double.MinValue;
            maxY = double.MinValue;

            foreach (XmlNode node in substationList)
            {
                double x = double.Parse(node.SelectSingleNode("X").InnerText);
                double y = double.Parse(node.SelectSingleNode("Y").InnerText);

                double decimalX, decimalY;

                ToLatLon(x, y, 34, out decimalX, out decimalY);

                if (decimalX < minX)
                {
                    minX = decimalX;
                }
                if (decimalX > maxX)
                {
                    maxX = decimalX;
                }
                if (decimalY < minY)
                {
                    minY = decimalY;
                }
                if (decimalY > maxY)
                {
                    maxY = decimalY;
                }
            }

            foreach (XmlNode node in nodeList)
            {
                double x = double.Parse(node.SelectSingleNode("X").InnerText);
                double y = double.Parse(node.SelectSingleNode("Y").InnerText);

                double decimalX, decimalY;

                ToLatLon(x, y, 34, out decimalX, out decimalY);


                if (decimalX < minX)
                {
                    minX = decimalX;
                }
                if (decimalX > maxX)
                {
                    maxX = decimalX;
                }
                if (decimalY < minY)
                {
                    minY = decimalY;
                }
                if (decimalY > maxY)
                {
                    maxY = decimalY;
                }
            }

            foreach (XmlNode node in switchList)
            {
                double x = double.Parse(node.SelectSingleNode("X").InnerText);
                double y = double.Parse(node.SelectSingleNode("Y").InnerText);

                double decimalX, decimalY;

                ToLatLon(x, y, 34, out decimalX, out decimalY);

                if (decimalX < minX)
                {
                    minX = decimalX;
                }
                if (decimalX > maxX)
                {
                    maxX = decimalX;
                }
                if (decimalY < minY)
                {
                    minY = decimalY;
                }
                if (decimalY > maxY)
                {
                    maxY = decimalY;
                }
            }
        }
        public static Dictionary<double, int> GetCordinates(double min, double max, double size)
        {
            double offset = (max - min) / size;

            Dictionary<double, int> pointsMap = new Dictionary<double, int>();

            for (int i = 0; i < size; i++)
            {
                pointsMap[min + i * offset] = i;
            }

            return pointsMap;
        }

        //From UTM to Latitude and longitude in decimal
        public static void ToLatLon(double utmX, double utmY, int zoneUTM, out double latitude, out double longitude)
        {
            bool isNorthHemisphere = true;

            var diflat = -0.00066286966871111111111111111111111111;
            var diflon = -0.0003868060578;

            var zone = zoneUTM;
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2) / c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone * 6.0) - 183.0);
            var lat = y / (c_sa * 0.9996);
            var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            var a = x / v;
            var a1 = Math.Sin(2 * lat);
            var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1 / 2.0);
            var j4 = ((3 * j2) + a2) / 4.0;
            var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
            var alfa = (3.0 / 4.0) * e2cuadrada;
            var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
            var b = (y - bm) / v;
            var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
            var eps = a * (1 - (epsi / 3.0));
            var nab = (b * (1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            var delt = Math.Atan(senoheps / (Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

            longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
            latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
        }

        #endregion

        #endregion
        #region Dodatne funkcije i klase
        public class Line
        {
            public double X1 { get; set; }
            public double X2 { get; set; }
            public double Y1 { get; set; }
            public double Y2 { get; set; }
        }

        public class Grid
        {
            public double X { get; set; }
            public double Y { get; set; }
            public bool Visible { get; set; } = false;
            public List<PowerEntity> Entities { get; set; } = new List<PowerEntity>();
            public Shape Shape { get; set; }
        }
        public static class ColorFormatter
        {
            public static Brush GetFillColor(string color)
            {
                if (color == "orange")
                {
                    return Brushes.Orange;
                }
                else if (color == "purple")
                {
                    return Brushes.Purple;
                }
                else if (color == "aqua")
                {
                    return Brushes.Aqua;
                }
                else
                {
                    return Brushes.Black;
                }
            }
        }


        public void Connect(double prviNodeId, double drugiNodeId, string lineName, string toolTipText)
        {
            Grid prviPoint = GridPoint(prviNodeId);
            Grid drugiPoint = GridPoint(drugiNodeId);

            if (prviPoint != null && drugiPoint != null)
            {
                // Proveri da li vec postoji linija da ne bi doslo do preklapanja
                var searchLineInfo = lines.Where(l => (l.X1 == prviPoint.X || l.X2 == prviPoint.X) && (l.X1 == drugiPoint.X || l.X2 == drugiPoint.X) && (l.Y1 == prviPoint.Y || l.Y2 == prviPoint.Y) && (l.Y1 == drugiPoint.Y || l.Y2 == drugiPoint.Y)).FirstOrDefault();

                if (searchLineInfo != null)
                {
                    return;
                }

                // Dodaj informaciju o novoj liniji
                Line lineInfo = new Line
                {
                    X1 = prviPoint.X,
                    X2 = drugiPoint.X,
                    Y1 = prviPoint.Y,
                    Y2 = drugiPoint.Y
                };
                if (lineInfo == null)
                    return;
                lines.Add(lineInfo);

                double x1 = Canvas.GetLeft(prviPoint.Shape) + 0.5;
                double y1 = Canvas.GetTop(prviPoint.Shape) + 0.5;
                double x2 = Canvas.GetLeft(drugiPoint.Shape) + 0.5;
                double y2 = Canvas.GetTop(drugiPoint.Shape) + 0.5;

                System.Windows.Shapes.Line line = new System.Windows.Shapes.Line();

                line.Name = lineName;
                line.ToolTip = toolTipText;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 0.2;
                line.X1 = x1 * pointOffset;
                line.X2 = x2 * pointOffset;
                line.Y1 = y1 * pointOffset;
                line.Y2 = y2 * pointOffset;

                line.MouseRightButtonDown += Line_RightClick;
                Canvas.Children.Add(line);
            }
        }

        private void Shape_LeftClick(object sender, MouseEventArgs e)
        {
            Shape target = sender as Shape;

            ScaleTransform trans = new ScaleTransform();
            target.RenderTransform = trans;

            DoubleAnimation anim = new DoubleAnimation(1, 10, TimeSpan.FromMilliseconds(1000));
            anim.AutoReverse = true;

            trans.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
            trans.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
        }

        private void Line_RightClick(object sender, MouseEventArgs e)
        {
            System.Windows.Shapes.Line line = sender as System.Windows.Shapes.Line;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Geographic.xml");
            XmlNodeList lineList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Lines/LineEntity");

            foreach (XmlNode node in lineList)
            {
                LineEntity l = new LineEntity();

                l.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                l.Name = node.SelectSingleNode("Name").InnerText;
                l.FirstEnd = long.Parse(node.SelectSingleNode("FirstEnd").InnerText);
                l.SecondEnd = long.Parse(node.SelectSingleNode("SecondEnd").InnerText);

                if (line.Name == l.Name)
                {
                    Shape prviShape = GridPoint(l.FirstEnd).Shape;
                    Shape drugiShape = GridPoint(l.SecondEnd).Shape;

                    var colorEditor = new PromenaBoje(prviShape, drugiShape);
                    colorEditor.Show();
                }
            }
        }

        private Grid GridPoint(double id)
        {
            foreach (var point in gridPoints)
            {
                foreach (var entity in point.Entities)
                {
                    if (entity.Id == id)
                    {
                        return point;
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
