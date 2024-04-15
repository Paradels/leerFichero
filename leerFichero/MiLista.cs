
namespace leerFichero
{
    internal class MiLista : List<String> 
    {
        public MiLista(String nf) {
            String linea;
            using (StreamReader sr = new StreamReader(nf))
            {
                linea = sr.ReadLine();
                while (linea != null)
                {
                    Add(linea);
                    linea = sr.ReadLine();
                }
            }
        }
    }
}
