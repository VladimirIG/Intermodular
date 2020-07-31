using Controller.Controles;
using Controller.Logica;
using Controller.Modelo;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace View.CRUD.altas
{
    /// <summary>
    /// Lógica de interacción para actualizarAlta.xaml
    /// </summary>

    public partial class actualizarAlta : Window
    {
        //--------------------------Campos de la clase
        private List<AltaTrabajador> altaTrabajadores;

        private AltaTrabajador alta;

        private string dni;
        private string passw;
        private string nombre;
        private string apellidos;
        private string rango;
        private string tlf;
        private string disponibilidad;
        private bool isadmin;
        private bool activo;

        private int numero;
        private string numero_ss;
        private string comienza;
        private string finaliza;
        private int horas_semana;
        private string tipo;

        //--------------------------Constructor
        public actualizarAlta()
        {
            InitializeComponent();

            altaTrabajadores = AltaTrabajadorController.listarAlta();
            rellenarComboBox();
        }

        //--------------------------Botonera
        private void Bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            asignarCampos();

            if (comprobarCampos())
            {
                if (AltaTrabajadorController.actualizarAltaTrabajador(this.dni, this.passw, this.nombre, this.apellidos, this.rango, this.tlf, this.disponibilidad
                    , this.isadmin, this.activo, this.numero, this.numero_ss, this.comienza, this.finaliza, this.horas_semana, this.tipo)
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
                errores += "\n -Número SS: Solo se admiten numeros .Debe tener almenos 6 caracteres";
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

        private void asignarCampos()
        {
            dni = comb_DNI.Text;
            passw = txt_Passw.Text;
            nombre = txt_Nombre.Text;
            apellidos = txt_Apellidos.Text;
            rango = comb_Rango.Text;
            tlf = txt_Telefono.Text;
            disponibilidad = comb_Disponibilidad.Text;
            isadmin = (bool)chk_isAdmin.IsChecked;
            activo = (bool)chk_Activo.IsChecked;

            /**/
            numero = Int32.Parse(txt_Numero.Text);
            numero_ss = txt_Numero_ss.Text;
            DateTime com = (DateTime)date_Comienza.SelectedDate;//  '2019-05-13'
            comienza = com.ToString("yyyy-MM-dd");
            DateTime fin = (DateTime)date_Finaliza.SelectedDate;
            finaliza = fin.ToString("yyyy-MM-dd");
            horas_semana = (Int32)updown_HorasSemana.Value;
            tipo = comb_Tipo.Text;
        }

        //--------------------------Métodos auxiliares
        private void rellenarComboBox()
        {
            foreach (AltaTrabajador alta in altaTrabajadores)
            {
                comb_DNI.Items.Add(alta.dni);
            }
        }

        private void refrescarCampos()
        {
            comb_DNI.Text = alta.dni;
            txt_Passw.Text = alta.passw;
            txt_Nombre.Text = alta.nombre;
            txt_Apellidos.Text = alta.apellidos;
            comb_Rango.Text = alta.rango;
            txt_Telefono.Text = alta.tlf;
            comb_Disponibilidad.Text = alta.disponibilidad;
            chk_isAdmin.IsChecked = alta.isadmin;
            chk_Activo.IsChecked = alta.activo;

            txt_Numero.Text = alta.numero.ToString();
            txt_Numero_ss.Text = alta.numero_ss;

            date_Comienza.SelectedDate = Convert.ToDateTime(alta.comienza);

            date_Finaliza.SelectedDate = Convert.ToDateTime(alta.finaliza);

            updown_HorasSemana.Value = alta.horas_semana;
            comb_Tipo.Text = alta.tipo;
        }

        //--------------------------Eventos
        private void Comb_DNI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string dniCombo = comb_DNI.SelectedItem.ToString();
            alta = AltaTrabajadorController.getAlta(dniCombo)[0];

            refrescarCampos();
        }
    }
}