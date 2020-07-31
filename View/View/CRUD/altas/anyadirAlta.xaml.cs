using Controller.Controles;
using Controller.Logica;
using System;
using System.Windows;
using System.Windows.Controls;

namespace View.CRUD.altas
{
    /// <summary>
    /// Lógica de interacción para anyadirAlta.xaml
    /// </summary>
    public partial class anyadirAlta : Window
    {
        //--------------------------Campos de la clase
        private ListView listaVista;

        private string dni;
        private string passw;
        private string nombre;
        private string apellidos;
        private string rango;
        private string tlf;
        private string disponibilidad;
        private bool isadmin;
        private bool activo;

        private string numero_ss;
        private string comienza;
        private string finaliza;
        private int horas_semana;
        private string tipo;

        //--------------------------Constructor
        public anyadirAlta(ListView ListaVentanaPadre)
        {
            InitializeComponent();
            listaVista = ListaVentanaPadre;
            date_Comienza.SelectedDate = DateTime.Today;
            date_Finaliza.SelectedDate = DateTime.Today;
        }

        //--------------------------Botonera
        private void Bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            asignarCampos();

            if (comprobarCampos())
            {
                if (AltaTrabajadorController.insertarAlta(this.dni, this.passw, this.nombre, this.apellidos, this.rango, this.tlf, this.disponibilidad
                    , this.isadmin, this.activo, this.numero_ss, this.comienza, this.finaliza, this.horas_semana, this.tipo)
                    )
                {
                    this.Close();
                }
            }
        }

        private void Bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos principales
        private bool comprobarCampos()
        {
            string errores = "";
            bool resultado = true;

            if (!InputChecker.comprobarFormatoDni(this.dni))
            {
                errores += "\n -DNI: Formato incorrecto. ";
                resultado = false;
            }

            if (this.passw.Length < 3)
            {
                errores += "\n -Contraseña: Debe tener almenos 4 caracteres.";
                resultado = false;
            }

            if (!InputChecker.isOnlyLetters(this.nombre) || this.nombre.Length < 3)
            {
                errores += "\n -Nombre: Solo se admiten letras. Debe tener almenos 4 caracteres";
                resultado = false;
            }

            if (!InputChecker.isOnlyLetters(this.apellidos) || this.apellidos.Length < 3)
            {
                errores += "\n -Apellidos: Solo se admiten letras. Debe tener almenos 4 caracteres";
                resultado = false;
            }

            if (!InputChecker.isOnlyNumbers(this.tlf) || (this.tlf.Length < 9))
            {
                errores += "\n -Teléfono: Solo se admiten numeros. Debe tener almenos 9 caracteres";
                resultado = false;
            }

            if (!InputChecker.isOnlyNumbers(this.numero_ss) || (this.numero_ss.Length < 4))
            {
                errores += "\n -Número SS: Solo se admiten numeros. Debe tener almenos 6 caracteres";
                resultado = false;
            }

            if (String.IsNullOrEmpty(this.comienza))
            {
                errores += "\n -Empieza: No puede estar vacío";
                resultado = false;
            }

            if (String.IsNullOrEmpty(this.finaliza))
            {
                errores += "\n -Finaliza: No puede estar vacío";
                resultado = false;
            }

            if (!resultado)
            {
                Fallos.multiFalloFormato(errores);
            }

            return resultado;
        }

        //--------------------------Métodos auxiliares
        private void asignarCampos()
        {
            dni = txt_Dni.Text;
            passw = txt_Passw.Text;
            nombre = txt_Nombre.Text;
            apellidos = txt_Apellidos.Text;
            rango = comb_Rango.Text;
            tlf = txt_Telefono.Text;
            disponibilidad = comb_Disponibilidad.Text;
            isadmin = (bool)chk_isAdmin.IsChecked;
            activo = (bool)chk_Activo.IsChecked;

            /**/

            numero_ss = txt_Numero_ss.Text;
            DateTime com = (DateTime)date_Comienza.SelectedDate;//  '2019-05-13'
            comienza = com.ToString("yyyy-MM-dd");
            DateTime fin = (DateTime)date_Finaliza.SelectedDate;
            finaliza = fin.ToString("yyyy-MM-dd");
            horas_semana = (Int32)updown_HorasSemana.Value;
            tipo = comb_Tipo.Text;
        }
    }
}