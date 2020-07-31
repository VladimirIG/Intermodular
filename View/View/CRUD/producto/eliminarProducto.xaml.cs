using Controller.Controles;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace View.CRUD.producto
{
    /// <summary>
    /// Lógica de interacción para eliminarProducto.xaml
    /// </summary>
    public partial class eliminarProducto : Window
    {
        //--------------------------Campos de la clase
        private List<Producto> productos;

        //--------------------------Constructor
        public eliminarProducto()
        {
            InitializeComponent();

            productos = ProductoController.listarProducto();
            rellenarComboBox();
        }

        //--------------------------Botonera
        private void bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (ProductoController.deleteProducto(Int32.Parse(comb_Codigo.Text)))
            {
                this.Close();
            }
            borrarArchivoImagenProducto(comb_Codigo.Text);
        }

        private void bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos principales
        private void borrarArchivoImagenProducto(string codigoProducto)
        {
            string uriImagen = conseguirUriImagen(codigoProducto);
            if (String.IsNullOrEmpty(uriImagen))
            {
                return;
            }

            string pathimagen = System.IO.Path.GetFullPath("../../") + uriImagen;

            if (File.Exists(pathimagen))
            {
                try
                {
                    File.Delete(pathimagen);
                    MessageBox.Show("La imagen asociada al producto ha sido eliminada.", "Imagen producto", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("La imagen asociada al producto no se ha podido borrar porque está siendo utilizada en la sesión de empleados. Puede borrarla manualmente.", "Imagen producto", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }

            MessageBox.Show("No se ha podido eliminar la imagen asociada al producto.", "Imagen producto", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        //--------------------------Métodos auxiliares
        private void rellenarComboBox()
        {
            foreach (Producto producto in productos)
            {
                comb_Codigo.Items.Add(producto.codigo);
            }
        }

        private string conseguirUriImagen(string codigoProducto)
        {
            foreach (Producto item in productos)
            {
                if (item.codigo.ToString().Equals(codigoProducto))
                {
                    try
                    {
                        return item.imagen.ToString();
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                }
            }
            return "";
        }
    }
}