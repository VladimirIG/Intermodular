using Controller.Logica;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Lógica de interacción para ConnectionView.xaml
    /// </summary>
    public partial class ConnectionView : Window
    {
        //--------------------------Campos de la clase

        string[] campos;

        //--------------------------Constructor
        public ConnectionView()
        {
            InitializeComponent();
            lbl_DirDefault.Content = "Dirección por defecto: " + Connection.LINK;
        }
        
        //--------------------------Botonera
        private void Bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            campos = new string[] { txt_Bloq1.Text, txt_Bloq2.Text, txt_Bloq3.Text, txt_Bloq4.Text};

            comprobarCamposIp();
            comprobarCampoPort();


            if (comprobarCamposIp())
            {
                if (comprobarCampoPort())
                {
                    Connection.IP = $"{campos[0]}.{campos[1]}.{campos[2]}.{campos[3]}";
                    Connection.Port = txt_Port.Text;
                    Connection.LINK = $"http://{Connection.IP}:{Connection.Port}";
                    this.Close();
                    return;
                }
            }

            Fallos.falloFormato("dirección http");
        }

        private void Bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Metodos auxiliares
        private bool comprobarCampoPort()
        {
            try
            {
                if ((Int32.Parse(txt_Port.Text) >= 0) && (Int32.Parse(txt_Port.Text) <= 65535))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool comprobarCamposIp()
        {
            foreach (string item in campos)
            {
                try
                {
                    if ((Int32.Parse(item) >= 0) && (Int32.Parse(item) <= 255))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
