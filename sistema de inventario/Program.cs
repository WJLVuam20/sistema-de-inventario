using System;
using System.Collections.Generic;

class Producto
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    public override string ToString()
    {
        return $"Codigo: {Codigo}, Nombre: {Nombre}, Cantidad: {Cantidad}, Precio: {Precio:C}";
    }
}

class Inventario
{
    private Dictionary<string, Producto> productos = new Dictionary<string, Producto>();

    public void AgregarProducto(string codigo, string nombre, int cantidad, decimal precio)
    {
        if (productos.ContainsKey(codigo))
        {
            Console.WriteLine("El producto ya existe.");
        }
        else
        {
            productos[codigo] = new Producto { Codigo = codigo, Nombre = nombre, Cantidad = cantidad, Precio = precio };
            Console.WriteLine("Producto agregado con éxito.");
        }
    }

    public void EliminarProducto(string codigo)
    {
        if (productos.Remove(codigo))
        {
            Console.WriteLine("Producto eliminado con exito.");
        }
        else
        {
            Console.WriteLine("El producto no existe.");
        }
    }

    public void ModificarProducto(string codigo, string nombre = null, int? cantidad = null, decimal? precio = null)
    {
        if (productos.TryGetValue(codigo, out var producto))
        {
            if (nombre != null)
                producto.Nombre = nombre;
            if (cantidad.HasValue)
                producto.Cantidad = cantidad.Value;
            if (precio.HasValue)
                producto.Precio = precio.Value;

            Console.WriteLine("Producto modificado con éxito.");
        }
        else
        {
            Console.WriteLine("El producto no existe.");
        }
    }

    public void ConsultarProducto(string codigo)
    {
        if (productos.TryGetValue(codigo, out var producto))
        {
            Console.WriteLine(producto);
        }
        else
        {
            Console.WriteLine("El producto no existe.");
        }
    }

    public void MostrarProductos()
    {
        if (productos.Count > 0)
        {
            foreach (var producto in productos.Values)
            {
                Console.WriteLine(producto);
            }
        }
        else
        {
            Console.WriteLine("No hay productos en el inventario.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var inventario = new Inventario();

        while (true)
        {
            Console.WriteLine("\nMenu de opciones:");
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Eliminar producto");
            Console.WriteLine("3. Modificar producto");
            Console.WriteLine("4. Consultar producto");
            Console.WriteLine("5. Mostrar todos los productos");
            Console.WriteLine("6. Salir");

            Console.Write("Seleccione una opcion: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el codigo del producto: ");
                    string codigo = Console.ReadLine();
                    Console.Write("Ingrese el nombre del producto: ");
                    string nombre = Console.ReadLine();
                    Console.Write("Ingrese la cantidad del producto: ");
                    int cantidad = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el precio del producto: ");
                    decimal precio = decimal.Parse(Console.ReadLine());
                    inventario.AgregarProducto(codigo, nombre, cantidad, precio);
                    break;

                case "2":
                    Console.Write("Ingrese el código del producto a eliminar: ");
                    codigo = Console.ReadLine();
                    inventario.EliminarProducto(codigo);
                    break;

                case "3":
                    Console.Write("Ingrese el codigo del producto a modificar: ");
                    codigo = Console.ReadLine();
                    Console.Write("Ingrese el nuevo nombre (deje vacio si no desea cambiar): ");
                    string nuevoNombre = Console.ReadLine();
                    Console.Write("Ingrese la nueva cantidad (deje vacio si no desea cambiar): ");
                    string nuevaCantidad = Console.ReadLine();
                    Console.Write("Ingrese el nuevo precio (deje vacio si no desea cambiar): ");
                    string nuevoPrecio = Console.ReadLine();

                    inventario.ModificarProducto(codigo,
                        string.IsNullOrWhiteSpace(nuevoNombre) ? null : nuevoNombre,
                        string.IsNullOrWhiteSpace(nuevaCantidad) ? (int?)null : int.Parse(nuevaCantidad),
                        string.IsNullOrWhiteSpace(nuevoPrecio) ? (decimal?)null : decimal.Parse(nuevoPrecio));
                    break;

                case "4":
                    Console.Write("Ingrese el codigo del producto a consultar: ");
                    codigo = Console.ReadLine();
                    inventario.ConsultarProducto(codigo);
                    break;

                case "5":
                    inventario.MostrarProductos();
                    break;

                case "6":
                    Console.WriteLine("Saliendo del programa.");
                    return;

                default:
                    Console.WriteLine("Opcion no válida, por favor intente de nuevo.");
                    break;
            }
        }
    }
}
