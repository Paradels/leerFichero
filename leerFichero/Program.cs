using System.Dynamic;
using TransporteContainers;

namespace Principal;
public class Program
{
    public static void Main(string[] args)
    {
        FaseDos(FaseUno());
    }

    private static List<Mercancia> FaseUno()
    {
        String? pais;
        Decimal peso;
        List<Mercancia> mercancias = new List<Mercancia>();
        StreamReader sr = new StreamReader("entradadatos.txt");

        
        pais = sr.ReadLine();
        while (pais!= null && pais != "0")
        {
            
            try
            {
                peso = Convert.ToDecimal(sr.ReadLine());
                mercancias.Add(new Mercancia(pais, peso));
            }
            catch (MercanciasException ex)
            {
                Console.WriteLine($"Destino o peso de mercancia es inválido\n{ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Peso debe ser un número\n{ex.Message}");
            }
           
            pais = sr.ReadLine();
        }
        foreach (Mercancia m in mercancias)
        {
            Console.WriteLine(m);
        }
        return mercancias;
    }

    private static void FaseDos(List<Mercancia> mercancias)
    {
        Decimal precioKilo, precioTotal;
        Dictionary<String, Decimal> enviosAgrupados = new Dictionary<String, Decimal>();
        int nConten;
        const int PRECIO_CONTAINER = 100;
        String lineaDeDetalle = "{0,-20}{1,-20}{2,-15}{3,-20}";

        Console.Write("Precio/kilo: ");
        precioKilo = Convert.ToDecimal(Console.ReadLine());

        foreach (Mercancia m in mercancias)
        {
            if (enviosAgrupados.ContainsKey(m.Destino))
                enviosAgrupados[m.Destino] = enviosAgrupados[m.Destino] + m.Peso;
            else
                //enviosAgrupados[m.Destino] = m.Peso;
                enviosAgrupados.Add(m.Destino, m.Peso);
        }

        Console.WriteLine("\nLISTADO DE DESTINOS Y CONTENEDORES");
        Console.WriteLine("----------------------------------");
        Console.WriteLine(lineaDeDetalle, "Destino", "Peso Acumulado", "Nº contenedores", "Precio total");

        foreach (String dest in enviosAgrupados.Keys)
        {
            nConten = (int)Math.Ceiling(enviosAgrupados[dest] / Mercancia.PESO_MAX_CONTAINER);
            precioTotal = nConten * PRECIO_CONTAINER + precioKilo * enviosAgrupados[dest];
            Console.WriteLine(lineaDeDetalle, dest, enviosAgrupados[dest], nConten, precioTotal);
        }
        Console.WriteLine($"\nPrecio por kilo en el contenedor {precioKilo}");
        Console.WriteLine($"Precio por contenedor {PRECIO_CONTAINER}");
        Console.WriteLine($"Peso por contenedor {Mercancia.PESO_MAX_CONTAINER}");
    }

  



    //private static void LeerLista_y_Mostrar(String nf)
    //{
    //    String linea;
    //    StreamReader sr = new StreamReader(nf); ;
    //    List<String> l = new List<String>();
    //    linea = sr.ReadLine();
    //    while (linea != null) 
    //    { 
    //        l.Add(linea);
    //        linea = sr.ReadLine(); 
    //    }   
    //    foreach (String s in l)
    //    {
    //        Console.WriteLine("---> \"" + s + " \"");
    //    }
    //}
    //private static void CrearMiLista_y_Mostrar(String nf)
    //{
    //    MiLista l = new MiLista(nf);
    //    foreach ( String s in l )
    //    {
    //        Console.WriteLine("---> \"" + s + "\"");
    //    }
    //}
}