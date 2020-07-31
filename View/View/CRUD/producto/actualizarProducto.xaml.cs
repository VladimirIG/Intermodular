using Controller.Controles;
using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace View.CRUD.producto
{
    /// <summary>
    /// Lógica de interacción para actualizarProducto.xaml
    /// </summary>
    public partial class actualizarProducto : Window
    {
        //--------------------------Campos de la
        private List<Producto> productos;

        private Producto producto;

        private int codigo;
        private string nombre;
        private string categoria;
        private string especificaciones;
        private decimal precio;
        private bool activo;
        private string imagen;

        string rutaImagenVieja;  //Esta ruta nos vale en el caso de que al actualizar el producto lo cambiamos de categoría
        string categoriaVieja;    //teniendo esta ruta, podemos borrar la imagen de la carpetade la categoría anterior

        //--------------------------Constructor
        public actualizarProducto()
        {
            InitializeComponent();

            productos = ProductoController.listarProducto();
            rellenarComboBox();
        }

        //--------------------------Botonera
        private void bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                if (ProductoController.actualizarProducto(codigo, nombre, categoria, especificaciones
                , precio, activo, imagen))
                {
                    this.Close();
                }
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
                    File.Delete(pathDestino);
                    File.Copy(pathOrigen, pathDestino);
                    return true; //la imagen ya está copiada en el destino, como estamos actualizando, queremos también poner una imagen nueva
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
                errores += "\n-Precio: Solo se admiten números y comas.";
                resultado = false;
            }
            if (btn_Examinar.IsEnabled)
            {
                try
                {
                    if (anyadirImagenAlProyecto())
                    {
                        this.imagen = rutaLinkRelativo();
                    }
                    else
                    {
                        errores += "\n-URI Imagen: La imagen debe existir y debe ser jpg.";
                        resultado = false;
                    }
                }
                catch (Exception)
                {
                    errores += "\n-Imagen: La imagen no puede ser alterada si se ha cargado ya la vista de empleado.";
                    resultado = false;
                }
            }
            else
            {
                cambioDeCategoria();
                this.imagen = $"IMG/{comb_Categoria.Text}/{txt_Nombre.Text}.jpg";
            }

            this.codigo = Int32.Parse(comb_Codigo.Text);
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
        private void rellenarComboBox()
        {
            foreach (Producto producto in productos)
            {
                comb_Codigo.Items.Add(producto.codigo);
            }
        }

        private void cambioDeCategoria() //Si la actualización es porqué se equivocó el admin en la categoría, con esta función copiamos la imagen del producto a la nueva carpeta
        {
            if (!categoriaVieja.Equals(comb_Categoria.Text))
            {

                string rutaVieja = System.IO.Path.GetFullPath("../../" + rutaImagenVieja);
                string rutaNueva = System.IO.Path.GetFullPath("../../" + rutaLinkRelativo());
                if (File.Exists(rutaNueva))
                {
                    File.Delete(rutaNueva);
                }

                File.Copy(rutaVieja, rutaNueva);
                File.Delete(rutaVieja);
            }
        }

        private string rutaLinkRelativo() 
        {
            return $"IMG\\{comb_Categoria.Text}\\{txt_Nombre.Text}.jpg";
        }

        private void refrescarCampos()
        {
            comb_Codigo.Text = producto.codigo.ToString();
            txt_Nombre.Text = producto.nombre;
            comb_Categoria.Text = producto.categoria;
            txt_Especificaciones.Text = producto.especificaciones;
            txt_Precio.Text = producto.precio.ToString();
            chk_Activo.IsChecked = producto.activo;
            txt_Imagen.Text = producto.imagen;
        }

        //--------------------------Eventos
        private void comb_Codigo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int codigo = Int32.Parse(comb_Codigo.SelectedItem.ToString());
            producto = ProductoController.getProducto(codigo)[0];

            refrescarCampos();

            this.rutaImagenVieja = txt_Imagen.Text;
            this.categoriaVieja = comb_Categoria.Text;
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

        private void chk_NuevaImg_Checked(object sender, RoutedEventArgs e)
        {
            btn_Examinar.IsEnabled = true;
            txt_Imagen.Text = "";
        }
    }
}