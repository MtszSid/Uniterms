using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.ComponentModel;
using Uniterms.Models;
using Windows.Foundation;
using Windows.UI;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Uniterms.Views
{
    public sealed partial class DrawOperation : UserControl
    {
        public Operation AlgebraicOperation
        {
            get => GetValue(UnitermsProperty) as Operation;
            set => SetValue(UnitermsProperty, value);
        }

        private static readonly double horizontalOffset = 8;
        private static readonly double verticalOffset = 10;
        private static readonly double margin = 1;

        private int _strokeThickness = 2;
        private int _fontSize = 14;

        public static readonly DependencyProperty UnitermsProperty =
            DependencyProperty.Register(
                nameof(AlgebraicOperation),
                typeof(Operation),
                typeof(DrawOperation),
                new PropertyMetadata(null, OnOperationChanged));

        public DrawOperation()
        {            
            this.InitializeComponent();
        }

        private void Person_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            Container.Children.Clear();
            if (AlgebraicOperation is not null)
            {
                DrawGraph(AlgebraicOperation, 20, 20);
            }
        }

        private static void OnOperationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DrawOperation control)
            {
                if (e.NewValue is Operation operation)
                {
                    operation.PropertyChanged += control.Person_PropertyChanged;
                }

                else if (e.NewValue is null && control.AlgebraicOperation is not null)
                    control.AlgebraicOperation.PropertyChanged -= control.Person_PropertyChanged;

                control.UpdateUI();
            }

        }

        private (double, double) DrawGraph(Operation operation, double x, double y)
        {
            (double, double) left;
            (double, double) separator;
            (double, double) right;
            if (operation.Left is Uniterm)
                left = DrawUniterm((operation.Left as Uniterm).Value, x, y + verticalOffset);
            else
            {
                left = DrawGraph(operation.Left as Operation, x, y + verticalOffset);
            }

            separator = DrawUniterm(operation.Separator, left.Item1 + x + horizontalOffset, y + verticalOffset);

            if (operation.Right is Uniterm)
                right = DrawUniterm((operation.Right as Uniterm).Value, left.Item1 + separator.Item1 + x + 2 * horizontalOffset, y + verticalOffset);
            else
                right = DrawGraph(operation.Right as Operation, left.Item1 + separator.Item1 + x + 2 * horizontalOffset, y + verticalOffset);

            if (operation is ParallelOperation)
            {
                DrawParallelLine(
                    x,
                    left.Item1 + separator.Item1 + right.Item1 + x + 2 * horizontalOffset,
                    y);
            }

            if (operation is SequenceOperation)
            {
                DrawSequenceLine(
                    x,
                    left.Item1 + separator.Item1 + right.Item1 + x + 2 * horizontalOffset,
                    y);
            }

            return (left.Item1 + separator.Item1 + right.Item1 + 2 * verticalOffset, Math.Max(left.Item2, right.Item2) + verticalOffset);

        }

        private (double, double) DrawUniterm(string uniterm, double x, double y)
        {

            var textBlock = new TextBlock
            {
                Text = uniterm,
                FontSize = _fontSize,
                Foreground = Foreground,
                Margin = new Thickness(2, 0, 2, 0)
            };

            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);

            Container.Children.Add(textBlock);

            var size = new Size(double.MaxValue, double.MaxValue);

            textBlock.Measure(size);
            var sizeComputed = textBlock.DesiredSize;

            return (sizeComputed.Width, sizeComputed.Height);
        }

        private void DrawParallelLine(double x1, double x2, double y)
        {

            var line = new Line
            {
                X1 = x1,
                Y1 = y,
                X2 = x2,
                Y2 = y,
                Stroke = Foreground,    
                StrokeThickness = _strokeThickness
            };

            var line1 = new Line
            {
                X1 = x1,
                Y1 = y,
                X2 = x1,
                Y2 = y + verticalOffset,
                Stroke = Foreground,
                StrokeThickness = _strokeThickness
            };

            var line2 = new Line
            {
                X1 = x2,
                Y1 = y,
                X2 = x2,
                Y2 = y + verticalOffset,
                Stroke = Foreground,
                StrokeThickness = _strokeThickness
            };

            Container.Children.Add(line);
            Container.Children.Add(line1);
            Container.Children.Add(line2);
        }

        private void DrawSequenceLine(double x1, double x2, double y)
        {
            var line = new Path
            {
                Stroke = Foreground,
                StrokeThickness = _strokeThickness
            };
            var pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure
            {
                StartPoint = new Windows.Foundation.Point(x1, y + verticalOffset)
            };

            var delta = (x2 - x1) / 4;
            var delta2 = (double)Math.Sqrt(x2 - x1) + 1;

            BezierSegment bezierSegment = new BezierSegment
            {
                Point1 = new Windows.Foundation.Point(x1 + delta, y - delta2),
                Point2 = new Windows.Foundation.Point(x2 - delta, y - delta2),
                Point3 = new Windows.Foundation.Point(x2, y + verticalOffset)
            };

            pathFigure.Segments.Add(bezierSegment);
            pathGeometry.Figures.Add(pathFigure);

            line.Data = pathGeometry;

            Container.Children.Add(line);
        }
    }
}