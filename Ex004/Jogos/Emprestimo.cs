using System;

namespace ControleJogos
{
    public class Emprestimo
    {
        public DateTime data;
        public string nomePessoa;
        public char emprestado; 
        public Emprestimo()
        {
            data = DateTime.MinValue;
            nomePessoa = "";
            emprestado = 'N';
        }
    }
}