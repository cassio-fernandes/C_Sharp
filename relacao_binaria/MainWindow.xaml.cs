using System;
using System.Windows;
using System.Windows.Media;

namespace relacao_binaria
{
    public partial class MainWindow : Window
    {
        int intervaloMin, intervaloMax, qtdLinhas;
        ParesBinarios obterPares;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, RoutedEventArgs e)
        {
            listBoxParesGerados.Items.Clear();
            listBoxFSimetrico.Items.Clear();

            labelStatus.Foreground = new SolidColorBrush(Colors.Red);

            if (verificarCampos())
            {
                obterPares = new ParesBinarios(intervaloMin, intervaloMax, qtdLinhas);
                obterPares.gerarPares();

                if (obterPares.verificarSimetria())
                {
                    labelStatus.Foreground = new SolidColorBrush(Colors.Green);
                    labelStatus.Content = "Pares simétricos";
                }
                else
                {
                    labelStatus.Content = "Pares não simétricos";
                }
                // preenche a lista de pares gerados
                foreach (var p in obterPares.pares)
                {
                    listBoxParesGerados.Items.Add(p);
                }
                //preenche a lista simétrica 
                foreach (var p in obterPares.paresSimetricos)
                {
                    listBoxFSimetrico.Items.Add(p);
                }
            }
        }
        
        private bool verificarCampos()
        {
            if (!string.IsNullOrWhiteSpace(textBoxMinimo.Text) && !string.IsNullOrWhiteSpace(textBoxMaximo.Text) && !string.IsNullOrWhiteSpace(textBoxQtd.Text))
            {   // Conversões de tipos 
                intervaloMin = Convert.ToInt32(textBoxMinimo.Text);
                intervaloMax = Convert.ToInt32(textBoxMaximo.Text);
                qtdLinhas = int.Parse(textBoxQtd.Text);

                if(intervaloMin < intervaloMax) 
                {   // Qtd deve obedecer: < 10^4; <= (n-m)^2; > 0
                    if (qtdLinhas <= Math.Pow((intervaloMax - intervaloMin) + 1, 2) && qtdLinhas < Math.Pow(10, 4) && qtdLinhas > 0)
                    {   // m deve ser <= 10^2
                        if (intervaloMax <= Math.Pow(10, 2))
                            return true;
                        else
                            MessageBox.Show("O intervalo maximo dever ser 100!");
                    }
                    else
                    {
                        //labelStatus.Content = "O maximo de linhas são " + Math.Pow((intervaloMax - intervaloMin)+1, 2) + " linhas.\nMinimo 1";
                        MessageBox.Show("O maximo de linhas são " + Math.Pow((intervaloMax - intervaloMin) + 1, 2) + " linhas.\nMinimo 1");
                    }
                }
                else
                {
                    MessageBox.Show("O intervalo minimo não pode ser maior\nque o intervalo máximo!");
                }
            }
            else
            {
                MessageBox.Show("Verifique os campos vazios!");
            }

            return false;
        }
    }
}
