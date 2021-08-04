using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CircunferenciaRectes
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Atributs 
        //Storiboard
        Storyboard story = new Storyboard();
        public struct Costats
        {
           /// <summary>
           /// 
           /// </summary>
           /// <param name="primer"></param>
           /// <param name="segon"></param>
           /// <param name="tercer"></param>
           /// <param name="quart"></param>
            public Costats(bool primer,bool segon ,bool tercer,bool quart)
            {
                Primer = primer;
                Segon = segon;
                Tercer = tercer;
                Quart = quart;
            }
            public bool Primer { get; set; }
            public bool Segon { get; set; }
            public bool Tercer { get; set; }
            public bool Quart { get; set; }

        }
        private Brush defaultColor;
        private Brush btnColorSelectionat = Brushes.WhiteSmoke;
        private SolidColorBrush colorLinia ;

        Line line;

        private int strokeThicknesses = 4;
        private int nLines=1;
        private bool? dibuixarTot = null;
        private int tempsPerDixuixar=1;
        private const string strAlMateixTemps= "Al mateix temps"; 


        //variables: de laterales
        public bool primerQuadrat = false;
        public bool segonQUadrant = false;
        public bool tercerQuadrant = false;
        public bool quartQuadrant = false;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            //default brush value
            defaultColor = Brushes.NavajoWhite;

            ActualizarConfiguracio();
        }
        /// <summary>
        /// Actualitza els parametres de configuracion (UI) amb variables locals
        /// </summary>
        private void ActualizarConfiguracio()
        { 
            dibuixarTot = cbxSencerMeitat.IsChecked == true;
            nLines = integerUpdDownNLinies.Value.Value;
            tempsPerDixuixar = integerUpdDownNTemps.Value.Value;
            colorLinia = new SolidColorBrush((Color)clrPickerLine.SelectedColor);
            strokeThicknesses = integerUpdDownNGrosor.Value.Value;
        }

         
        /// <summary>
        /// Generar la grafica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {

            if (!primerQuadrat && !segonQUadrant && !tercerQuadrant && !quartQuadrant)
            {
                tbErrorTrobat.Visibility = Visibility.Visible;
                tbErrorTrobat.Text = " No heu selectionat cap cantonada !!!";
               
            }
            else
            { 

                cnvTauler.Children.Clear();
                ActualizarConfiguracio();//actualizem els parametres 

                //generar metodes
                if (primerQuadrat) { DibuixarPrimerQuadrat(); }
                if (segonQUadrant) { DibuixarSegonQuadrat(); }
                if (tercerQuadrant) { DibuixarTercerQuadrat(); }
                if (quartQuadrant) { DibuixarQuartQuadrat(); }
                 
            }
             
            #region noUtilitzat 
            //simple line
            //line = new Line(); 
            //line.Stroke = colorLinia;
            //line.X1 = 0;
            //line.X2 = cnvTauler.ActualWidth;
            //line.Y1 = 0;
            //line.Y2 = cnvTauler.ActualHeight; 
            //line.StrokeThickness = 4; 
            //cnvTauler.Children.Add(line);
            #endregion
        }

        #region Metodes generar grafica
        private void DibuixarSegonQuadrat()
        {
            for (int i = 0; i < nLines; i++)
            {
               
                line = new Line();
                line.Stroke = colorLinia;
                
                if (dibuixarTot==true)
                    line.X1 = (cnvTauler.ActualWidth / nLines) * (i);
                else
                    line.X1 = cnvTauler.ActualWidth / 2 + (((cnvTauler.ActualWidth / 2) / nLines) * (i));
                line.Y1 = 0;

                line.X2 = 1000;
                if (dibuixarTot == true)
                    line.Y2 = (cnvTauler.ActualWidth / nLines) * (i + 1);
                else
                    line.Y2 = ((cnvTauler.ActualWidth / nLines) * (i + 1)) / 2;


                line.StrokeThickness = strokeThicknesses;//grosor de liniea
                if (cbbxVelocitat_al_dibuixar.Text == strAlMateixTemps)
                {
                    cnvTauler.Children.Add(line);
                    Animar(line);
                }
                else
                {
                    Animar(line);
                    cnvTauler.Children.Add(line);
                    Esperar(Convert.ToInt32(integerUpdDownNTemps.Value.Value));
                }

                DoEvents();
            }
        }
        private void DibuixarPrimerQuadrat()
        {  //Dibuixem totes les lineas
            for (int i = 0; i < nLines; i++)
            { 
                line = new Line();
                line.Stroke = colorLinia;
              
                if (dibuixarTot==true) //valor null, true false
                    line.X1 = (cnvTauler.ActualWidth / nLines) * (i + 1);
                else
                    line.X1 = (((cnvTauler.ActualWidth / 2) / nLines) * (i + 1));
                line.Y1 = 0;

                line.X2 = 0;
                if (dibuixarTot == true)
                    line.Y2 = (cnvTauler.ActualWidth / nLines) * (nLines - i);
                else
                    line.Y2 = ((cnvTauler.ActualWidth / 2) / nLines) * (nLines - i);
                line.StrokeThickness = strokeThicknesses;
                

                if (cbbxVelocitat_al_dibuixar.Text == strAlMateixTemps)
                {
                    cnvTauler.Children.Add(line);
                    Animar(line);
                }
                else
                {
                    Animar(line);
                    cnvTauler.Children.Add(line);
                    Esperar(Convert.ToInt32(integerUpdDownNTemps.Value.Value));
                }

                DoEvents();
             
            }
        }
        private void DibuixarTercerQuadrat() 
        { 
            for (int i = 0; i < nLines; i++)
            { 
                line = new Line();
                line.Stroke = colorLinia;   
                line.X1 = cnvTauler.ActualHeight;
                line.Y2 = cnvTauler.ActualWidth;
                if (dibuixarTot==true)
                    line.Y2 = ((cnvTauler.ActualWidth / nLines) * (i));
                else 
                     line.Y2 = cnvTauler.ActualWidth / 2 + (((cnvTauler.ActualWidth / 2) / nLines) * (i));

                line.X2 = 0;
                line.Y1 = cnvTauler.ActualWidth;
                
                if (dibuixarTot == true)
                    line.X1 = (cnvTauler.ActualWidth / nLines) * (i + 1);
                else
                    line.X1 = ((cnvTauler.ActualWidth / nLines) * (i + 1)) / 2;


                line.StrokeThickness = strokeThicknesses;


                if (cbbxVelocitat_al_dibuixar.Text == strAlMateixTemps)
                {
                    cnvTauler.Children.Add(line);
                     Animar(line);
                }
                else
                {
                    Animar(line);
                    cnvTauler.Children.Add(line);
                    Esperar(Convert.ToInt32(integerUpdDownNTemps.Value.Value));
                }

                DoEvents();
            }
        }
        private void DibuixarQuartQuadrat() 
        {
            for (int i = 0; i < nLines; i++)
            { 
                line = new Line();
                line.Stroke = colorLinia;

                line.X1 = cnvTauler.ActualHeight;
                line.Y2 = cnvTauler.ActualWidth;

                if (dibuixarTot == true)
                    line.X2 = (cnvTauler.ActualWidth / nLines) * (nLines - (i + 1));
                else
                    line.X2 = cnvTauler.ActualWidth / 2 + (((cnvTauler.ActualWidth / 2) / nLines) * (nLines - (i + 1)));

                if (dibuixarTot==true)
                    line.Y1 = (cnvTauler.ActualWidth / nLines) * (i);
                else
                    line.Y1 = cnvTauler.ActualWidth / 2 + ((cnvTauler.ActualWidth / 2) / nLines) * (i);

                line.StrokeThickness = strokeThicknesses;

                if (cbbxVelocitat_al_dibuixar.Text == strAlMateixTemps)
                {
                    cnvTauler.Children.Add(line);
                     Animar(line);
                }
                else
                {
                    Animar(line);
                    cnvTauler.Children.Add(line);
                    Esperar(Convert.ToInt32(integerUpdDownNTemps.Value.Value));
                }

                DoEvents();
            }
        }
        #endregion Animacion

        #region 
        public void Animar(Line linea)
        {
            story.Children.Clear();

            DoubleAnimation animacio_x = new DoubleAnimation();
            DoubleAnimation animacio_y = new DoubleAnimation();

            animacio_x.Duration = TimeSpan.FromSeconds(Convert.ToInt32(integerUpdDownNTemps.Value.Value));
            animacio_y.Duration = TimeSpan.FromSeconds(Convert.ToInt32(integerUpdDownNTemps.Value.Value));
            animacio_x.To = linea.X1;
            animacio_x.From = linea.X2;
            animacio_y.To = linea.Y1;
            animacio_y.From = linea.Y2; 

            Storyboard.SetTargetProperty(animacio_x, new PropertyPath(Line.X1Property));
            Storyboard.SetTargetProperty(animacio_y, new PropertyPath(Line.Y1Property));

            story.Children.Add(animacio_x);
            story.Children.Add(animacio_y);
            linea.BeginStoryboard(story);



        }
        #endregion

        /// <summary>
        /// Cuan canviem el color de dibuix
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clrPickerLine_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e )
        {
            colorLinia = new SolidColorBrush((Color)clrPickerLine.SelectedColor);
            if (line != null)
                foreach (FrameworkElement Framework_Element in cnvTauler.Children)
                { 
                    ((Line)Framework_Element).Stroke = colorLinia;//new SolidColorBrush(Colors.Black);
                } 
        }
        
        #region Grafica adaptativa
        /// <summary>
        /// La grafica es adaptativa al cambio de tamano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cnvTauler_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(line!=null) //comprobem que no es la primera intents
            { 

            cnvTauler.Children.Clear();

            line = new Line();
            //per agafar el color de grafica
            //SolidColorBrush brush = new SolidColorBrush((Color)clrPickerLine.SelectedColor);
            //line.Stroke = brush;
            //line.X1 = 10;
            //line.X2 = cnvTauler.ActualWidth;
            //line.Y1 = 1;
            //line.Y2 = cnvTauler.ActualHeight;
            //line.StrokeThickness = 2;
            //cnvTauler.Children.Add(line);
            }
        }
        #endregion

        #region DoEvents : Metodos asincronicos
        /// <summary>
        /// Metodo que crear una espera , utilitzant el THREAD
        /// </summary>
        /// <param name="segons"></param>
        private void Esperar(double segons)
        {
            var frame = new DispatcherFrame();
            new Thread((ThreadStart)(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(segons));
                frame.Continue = false;
            })).Start();
            Dispatcher.PushFrame(frame);
        }

        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(
               System.Windows.Threading.DispatcherPriority.Background,
               new Action(delegate { }));
        }
        #endregion

        #region Controls de button : colors de boton

        //btnCantonadaUpLeft
        private void btnEsquerra_Click(object sender, RoutedEventArgs e)
        {
            tbErrorTrobat.Visibility = Visibility.Hidden;
            primerQuadrat = !primerQuadrat;

            if(btnCantonadaUpLeft.Background == defaultColor)
            {
                btnCantonadaUpLeft.Background = btnColorSelectionat;
            }
            else
            {
                btnCantonadaUpLeft.Background = defaultColor;
            }
        }
        //btnCantonadaUpRight
        private void btnDreta_Click(object sender, RoutedEventArgs e)
        {
            tbErrorTrobat.Visibility = Visibility.Hidden;
            segonQUadrant = !segonQUadrant;
            if (btnCantonadaUpRight.Background == defaultColor)
            {
                btnCantonadaUpRight.Background = btnColorSelectionat;
            }
            else
            {
                btnCantonadaUpRight.Background = defaultColor;
            }
        }
        //btnContonadaDownLeft
        private void btnAdalt_Click(object sender, RoutedEventArgs e)
        {
            tbErrorTrobat.Visibility = Visibility.Hidden;
            tercerQuadrant = !tercerQuadrant;
            if (btnContonadaDownLeft.Background == defaultColor)
            {
                btnContonadaDownLeft.Background = btnColorSelectionat;
            }
            else
            {
                btnContonadaDownLeft.Background = defaultColor;
            }
        }
        //btnCantonadaDownRight
        private void btnAbaix_Click(object sender, RoutedEventArgs e)
        {
            tbErrorTrobat.Visibility = Visibility.Hidden;
            quartQuadrant = !quartQuadrant;
            if (btnCantonadaDownRight.Background == defaultColor)
            {
                btnCantonadaDownRight.Background = btnColorSelectionat;
            }
            else
            {
                btnCantonadaDownRight.Background = defaultColor;
            }
        }
        /// <summary>
        /// Posem  el color per defecte  els bottons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Expander_Loaded(object sender, RoutedEventArgs e)
        {
            btnCantonadaUpLeft.Background = defaultColor;
            btnCantonadaUpRight.Background = defaultColor;
            btnContonadaDownLeft.Background = defaultColor;
            btnCantonadaDownRight.Background = defaultColor;
        }
       
        //actualitzem el variable de  grosor en la grafica, i el variable local
        private void integerUpdDownNGrosor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            strokeThicknesses = integerUpdDownNGrosor.Value.Value;//actualitzem el variable
            if (line != null)
                foreach (FrameworkElement Framework_Element in cnvTauler.Children)
                {
                    ((Line)Framework_Element).StrokeThickness = strokeThicknesses;//new SolidColorBrush(Colors.Black);
                }
        }
        #endregion
        MediaPlayer mediaPlayer = new MediaPlayer();
        private bool activat = false;
        private void cnvTauler_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            activat = !activat;
            if (activat) {
                mediaPlayer.Volume = 30;
            mediaPlayer.Open(new Uri(@"C:\Users\estud\Music\vivaldi.mp3"));
            mediaPlayer.Play();
            }
            else
            {
                mediaPlayer.Stop();
            }
      
        }
    }
} 
