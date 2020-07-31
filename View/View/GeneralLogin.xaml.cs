using Controller.Controles;
using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace View
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //--------------------------Campos de la clase
        private int intentos;

        //--------------------------Constructor
        public MainWindow()
        {
            //iniciarNode();  
            conexionPorDefecto();
            InitializeComponent();

            intentos = 3;
            txt_Usuario.Focus();
            
        }

        //--------------------------Botonera
        private void btn_aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (InputChecker.comprobarFormatoLogin(txt_Usuario.Text, pass_Usuario.Password))
            {
                comprobarCredenciales();
            }
            else
            {
                falloLogin();
            }
        }
        private void btn_Config_Click(object sender, RoutedEventArgs e)
        {
            ConnectionView mostrar = new ConnectionView();
            mostrar.ShowDialog();
        }
        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            salir();
        }

        //--------------------------Métodos principales
        private void comprobarCredenciales()
        {
            try
            {
                Empleado emp = EmpleadoController.getEmpleado(txt_Usuario.Text)[0];

                if (emp.passw == pass_Usuario.Password)
                {
                    if (emp.isadmin)
                    {
                        AdminView adminView = new AdminView(emp, null);//Null corresponde a una nueva sesion, con esta variable controlamos que los pedidos de las mesas se conserven al saltar entre ventanas de Admin a Empleado.
                        adminView.Show();
                        this.Close();
                    }
                    else
                    {
                        EmpleadoView empleadoView = new EmpleadoView(emp);
                        empleadoView.Show();
                        this.Close();
                    }
                }
                else
                {
                    falloLogin();
                }
            }
            catch (Exception)
            {
                Fallos.falloConexionDB();
            }
        }

        private void falloLogin()
        {
            actualizarLabelIntentos();
            comprobarIntentosRestantes();
            MessageBox.Show("Usuario o contraseña incorrecto.", "Fallo en el login", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void iniciarNode() //EN ESTADO DE PRUEBA PARA EL INSTALADOR
        {
            string pathAppJs = System.IO.Path.GetFullPath("../../../../") + "\\Model";
            Process p = new Process();
            p.StartInfo.WorkingDirectory = pathAppJs;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c node app.js";
            p.Start();
        }

        //--------------------------Métodos auxiliares
        private void comprobarIntentosRestantes()
        {
            if (intentos == 0)
            {
                MessageBox.Show("Ha agotado el número de intentos.", "Intentos", MessageBoxButton.OK, MessageBoxImage.Error);
                salir();
            }
        }

        private void actualizarLabelIntentos()
        {
            intentos--;
            lbl_intentos.Content = $"Tiene {intentos} intentos.";
        }
        private void conexionPorDefecto()
        {
            Connection.IP = "localhost";
            Connection.Port = "3000";
            Connection.LINK = $"http://{Connection.IP}:{Connection.Port}";
        }

        private void salir()
        {
            System.Environment.Exit(0);
        }

        //--------------------------Eventos
        private void pass_Usuario_KeyDown(object sender, KeyEventArgs e) //Al estar en el campo contraseña y presionar ENTER se activa el botón Aceptar
        {
            if (e.Key == Key.Enter)
            {
                btn_aceptar_Click(this, e);
            }
        }

  
    }
}