using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace relacao_binaria
{
    public class ParesBinarios
    {
        private int intMin, intMax, quant;
        private int[,] xy;
        Random num;
        public List<string> pares = null;
        public List<string> paresSimetricos = null;

        //Construtor recebe os valores dos intervalos maximos e míniimo e a quantidade de linhas
        public ParesBinarios(int n, int m, int q)
        {
            intMin = n;
            intMax = m+1; // O +1 é para que o intervalo de numeros aleatórios vá de min à max, em vez de min à max-1;
            quant = q;
        }

        public void gerarPares()
        {
            num = new Random();
            xy = new int[1, 2];
            int q = quant;
            do
            {
                xy[0, 0] = num.Next(intMin, intMax);
                xy[0, 1] = num.Next(intMin, intMax);
                string aux = xy[0, 0].ToString() + "-" + xy[0, 1].ToString();

                if (pares != null)
                {
                    foreach (var num in pares)
                    {   
                        if(pares.Contains(aux))
                        {
                            break;
                        }
                        else
                        {
                            pares.Add(aux);
                            if (!paresSimetricos.Contains(aux))
                            {
                                gerarSimetria(xy[0, 1], xy[0, 0]);

                                if (!(xy[0, 0] == xy[0, 1]))
                                {
                                    gerarSimetria(xy[0, 0], xy[0, 1]);
                                }
                            }
                            //  Caso os valores de X e Y sejam iguais, significa que já são simétricos. Não precisa inserir

                            q--;
                            break;
                        }
                    }
                }
                else 
                {
                    pares = new List<string>();
                    paresSimetricos = new List<string>();
                    pares.Add(aux);
                    gerarSimetria(xy[0, 1], xy[0, 0]);

                    //  Caso os valores de X e Y sejam iguais, significa que já são simétricos. Não precisa inserir

                    if (!(xy[0, 0] == xy[0, 1]))
                    {
                        gerarSimetria(xy[0, 0], xy[0, 1]);
                    }
                    q--;
                }
            } while (q > 0);
            pares.Sort();
        }

        private void gerarSimetria(int x, int y)
        {
            paresSimetricos.Add(x.ToString() + "-" + y.ToString());
            paresSimetricos.Sort();
        }

        public bool verificarSimetria()
        {
            int i=0;
            
            if (!(quant == Math.Pow((intMax-intMin), 2)))
            {
                foreach (var elemento in pares)
                {
                    if (paresSimetricos.Contains(elemento))
                    {
                        i++;
                    }
                }

                //paresSimetricos.Sort();

                if (i != paresSimetricos.Count)
                {
                    return false;
                }
            }

            return true;
        }
    }
}