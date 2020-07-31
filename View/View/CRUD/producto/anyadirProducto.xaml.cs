using Controller.Controles;
using Controller.Logica;
using System;
using System.IO;
using System.Windows;

namespace View.CRUD.producto
{
    /// <summary>
    /// Lógica de interacción para anyadirProducto.xaml
    /// </summary>
    public partial class anyadirProducto : Window
    {
        //--------------------------Campos de la clase

        private string nombre;
        private string categoria;
        private string especificaciones;
        private decimal precio;
        private bool activo;
        private string imagen;

        //--------------------------Constructor
        public anyadirProducto()
        {
            InitializeComponent();
        }

        //--------------------------Botonera
        private void bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                if (ProductoController.insertarProducto(nombre, categoria, especificaciones
                , precio, activo, imagen))
                {
                    this.Close();
                }
            }
        }

        private void btn_Examinar_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Image";
            dlg.DefaultExt = ".jpg";

            dlg.Filter = "Image files (JPG,PNG)|*.JPG;*.PNG";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                txt_Imagen.Text = dlg.FileName;
            }
            else
            {
                txt_Imagen.Text = "";
            }
        }

        private void bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos principales
        private bool anyadirImagenAlProyecto()
        {
            string pathOrigen = txt_Imagen.Text;
            string pathDestino = System.IO.Path.GetFullPath("../../") + rutaLinkRelativo();

            if (File.Exists(pathOrigen))
            {
                if (!File.Exists(pathDestino))
                {
                    string ext = Path.GetExtension(pathOrigen);
                    if (ext.Equals(".jpg"))
                    {
                        File.Copy(pathOrigen, pathDestino);
                        return true; //la imagen se copia si no existe en el destino
                    }
                }
                else
                {
                    return true; //la imagen ya está copiada en el destino
                }
            }
            return false; //la imagen no existe en el origen
        }

        private bool comprobarAsignarCampos()
        {
            string errores = "";
            bool resultado = true;

            if (!InputChecker.isOnlyLetters(txt_Nombre.Text) || (txt_Nombre.Text.Length < 3))
            {
                errores += "\n-Nombre: Solo se admiten letras. Debe tener almenos 4 caracteres";
                resultado = false;
            }
            else
            {
                this.nombre = txt_Nombre.Text.ToUpper();
            }

            try
            {
                this.precio = decimal.Parse(txt_Precio.Text.Replace('.', ','));
            }
            catch (Exception)
            {
                errores += "\n-Precio: Solo se admiten numeros y comas.";
                resultado = false;
            }

            if (anyadirImagenAlProyecto())
            {
                this.imagen = $"IMG/{comb_Categoria.Text}/{txt_Nombre.Text}.jpg";
            }
            else
            {
                errores += "\n-URI Imagen: La imagen debe existir y debe ser jpg.";
                resultado = false;
            }

            this.categoria = comb_Categoria.Text;
            this.especificaciones = txt_Especificaciones.Text;
            this.activo = (bool)chk_Activo.IsChecked;

            if (!resultado)
            {
                Fallos.multiFalloFormato(errores);
                return false;
            }

            return true;
        }

        //--------------------------Métodos auxiliares
        private string rutaLinkRelativo()
        {
            return $"IMG\\{comb_Categoria.Text}\\{txt_Nombre.Text}.jpg";
        }
    }
}