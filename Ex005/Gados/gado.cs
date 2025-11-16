namespace FazendaGado
{
    public class Gado
    {
        public int codigo;
        public int leite;
        public int alim;
        public DataNasc nasc;
        public char abate;

        public Gado()
        {
            nasc = new DataNasc();
            abate = 'N';
        }
    }
}