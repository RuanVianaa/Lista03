namespace ControleJogos
{
    public class Jogo
    {
        public string Titulo;
        public string Console;
        public int Ano;
        public int Ranking;
        
        public Emprestimo InfoEmprestimo;

        public Jogo()
        {
            InfoEmprestimo = new Emprestimo();
        }
    }
}