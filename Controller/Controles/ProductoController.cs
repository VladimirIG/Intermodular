using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Windows;

namespace Controller.Controles
{
    public class ProductoController
    {
        public static List<Producto> listarProducto()
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/producto", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Producto>>(response.Content);
        }

        public static List<Producto> getProducto(int codigo)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/producto/{codigo}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Producto>>(response.Content);
        }

        public static bool insertarProducto(int codigo, string nombre, string categoria
        , string especificaciones, decimal precio, bool activo, string imagen)
        {
            Producto producto = new Producto(codigo, nombre, categoria
        , especificaciones, precio, activo, imagen);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/producto", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(producto);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Producto Insertado", "Producto Insertado", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool insertarProducto(string nombre, string categoria
        , string especificaciones, decimal precio, bool activo, string imagen)//PK autoincrementable
        {
            Producto producto = new Producto(nombre, categoria
        , especificaciones, precio, activo, imagen);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/producto", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(producto);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Producto Insertado", "Producto Insertado", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool actualizarProducto(int codigo, string nombre, string categoria
        , string especificaciones, decimal precio, bool activo, string imagen)
        {
            Producto producto = new Producto(codigo, nombre, categoria
        , especificaciones, precio, activo, imagen);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/producto", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(producto);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Producto Actualizado", "Producto Actualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool deleteProducto(int codigo)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/producto/{codigo}", Method.DELETE);
            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                if (ProductoController.getProducto(codigo).Count == 0)
                {
                    MessageBox.Show("Producto eliminado", "Producto eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("El Producto no ha podido ser eliminado ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }
    }
}