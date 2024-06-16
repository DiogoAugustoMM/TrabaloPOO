﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO
{
    internal class FestaECia
    {
        public Espaco[] Espacos = new Espaco[8];
        public Evento[] Cerimonias = new Evento[31];

        public FestaECia()
        {
            Espaco A = new Espaco("A", 100,10000);
            Espacos[0] = A;
            Espaco B = new Espaco("B", 100, 10000);
            Espacos[1] = B;
            Espaco C = new Espaco("C", 100, 10000);
            Espacos[2] = C;
            Espaco D = new Espaco("D", 100, 10000);
            Espacos[3] = D;
            Espaco E = new Espaco("E", 200, 17000);
            Espacos[4] = E;
            Espaco F = new Espaco("F", 200, 17000);
            Espacos[5] = F;
            Espaco G = new Espaco("G", 50, 8000);
            Espacos[6] = G;
            Espaco H = new Espaco("H", 500, 35000);
            Espacos[7] = H;
        }

        private Espaco VerificaProximaData(DateTime data, int convidados)
        {
            //verifica a quantidade de convidados e retorna o espaço adequado
            if (convidados <= 50)
            {
                Console.WriteLine("a proxima data disponivel é em " + Espacos[6].ProximaDataDisponivel(data) + " No espaço" + Espacos[6].Tipo
                + "\nDeseja essa Data?(disgite sim ou não)");
                string opcao = Console.ReadLine();
                if (opcao == "sim")
                {
                    Espacos[6].AdicionaData(Espacos[6].ProximaDataDisponivel(data));
                    return Espacos[6];
                }
                else return new Espaco("Z", -2,-2);


            }
            else if (convidados <= 100)
            {
                DateTime menor = DateTime.MaxValue;
                Espaco disponivel = new Espaco("z", 0,-2);
                for (int i = 0; i < 4; i++)
                {
                    DateTime atual = Espacos[i].ProximaDataDisponivel(data);
                    if (atual <= menor)
                    {
                        menor = atual;
                        disponivel = Espacos[i];
                    }
                }
                Console.WriteLine("a proxima data disponivel é em " + menor + " No espaço" + disponivel.Tipo + "\nDeseja essa Data?(disgite sim ou não)");
                string opcao = Console.ReadLine();
                if (opcao == "sim")
                {
                    disponivel.AdicionaData(disponivel.ProximaDataDisponivel(data));
                    return disponivel;
                }
                else return new Espaco("z", -2, -2);


            }
            else if (convidados <= 200)
            {
                DateTime menor = DateTime.MaxValue;
                Espaco disponivel = new Espaco("z", -2, -2);
                for (int i = 4; i < 6; i++)
                {
                    DateTime atual = Espacos[i].ProximaDataDisponivel(data);
                    if (atual <= menor)
                    {
                        menor = atual;
                        disponivel = Espacos[i];
                    }
                }
                Console.WriteLine("a proxima data disponivel é em " + menor + " No espaço" + disponivel.Tipo + "\nDeseja essa Data?(disgite sim ou não)");
                string opcao = Console.ReadLine();
                if (opcao == "sim")
                {
                    disponivel.AdicionaData(disponivel.ProximaDataDisponivel(data));
                    return disponivel;
                }
                else return new Espaco("z", -2, -2);

            }
            else if (convidados <= 500)
            {
                Console.WriteLine("a proxima data disponivel é em " + Espacos[7].ProximaDataDisponivel(data) + " No espaço " + Espacos[7].Tipo + "\nDeseja essa Data?(disgite sim ou não)");
                string opcao = Console.ReadLine();
                if (opcao == "sim")
                {
                    Espacos[7].AdicionaData(Espacos[7].ProximaDataDisponivel(data));
                    return Espacos[7];
                }
                else return new Espaco("z", -2, -2);



            }
            else
            {
                return new Espaco("z", -1, -2);
            }
        }

        public bool CriarEvento(DateTime data, int convidados, int tipoEvento)
        {
            Console.WriteLine("digite seu cpf");
            string cpf = Console.ReadLine();
            //cria a cerimonia com o espaço ideal
            Espaco disponivel = VerificaProximaData(data, convidados);
            if (disponivel.Capacidade == -1)
            {
                Console.WriteLine("o numero de convidados excede nossa capacidade maxima de pessoas");
                return false;
            }
            else if (disponivel.Capacidade == -2)
            {
                Console.WriteLine("Agendamento cancelado");
                return false;
            }
            else
            {
                Evento nova;
                //Cria uma evento livre
                if (tipoEvento == 1)
                {
                    nova = new Casamento(convidados, data, disponivel.Valor, disponivel, cpf);
                }
                else if (tipoEvento == 2)
                {
                    nova = new Aniversario(convidados, data, disponivel.Valor, disponivel, cpf);
                }
                else if (tipoEvento == 3)
                {
                    nova = new Aniversario(convidados, data, disponivel.Valor, disponivel, cpf);
                }
                else if (tipoEvento == 4)
                {
                    nova = new Aniversario(convidados, data, disponivel.Valor, disponivel, cpf);
                }
                else
                {
                    nova = new Evento(data,convidados,disponivel,cpf,disponivel.Valor);
                }

                for (int i = 0; i < Cerimonias.Length; i++)
                {
                    if (Cerimonias[i] == null)
                    {
                        Cerimonias[i] = nova;
                        Console.WriteLine("Cerimonia no espaço {0} confirmada!", disponivel.Tipo);
                        return true;
                    }
                }
                Console.WriteLine("Agendamento cancelado");
                return false;
            }
        }

        public bool Informacoes(string cpfnoiva)
        {
            for (int i = 0; i < Cerimonias.Length; i++)
            {
                if (Cerimonias[i] != null && Cerimonias[i].CPFNoiva == cpfnoiva)
                {
                    Console.WriteLine("Casamento da noiva de cpf {0} ocorrera no dia {1} para {2} convidados", Cerimonias[i].CPFNoiva, Cerimonias[i].Data, Cerimonias[i].Convidados);
                    return true;
                }
            }
            Console.WriteLine("CPF de noiva invalido ou casamento não cadastrado ainda");
            return false;
        }

        public int TiposEventos()
        {
            int opcao = -1; // Inicializa a variável opção

            do
            {
                // Exibe as opções de eventos
                Console.WriteLine("Os eventos ofertados são:" +
                "\n 1. Casamento" +
                "\n 2. Aniversario" +
                "\n 3. Festa de Empresa" +
                "\n 4. Formatura" +
                "\n 5. Apenas aluguel de espaco ");
                Console.Write("Escolha o numero correspondente ao evento desejado: ");

                try
                {
                    // Tenta converter a entrada do usuário para um número inteiro
                    opcao = int.Parse(Console.ReadLine());

                    // Verifica se a opção é válida
                    if (opcao < 1 || opcao > 5)
                    {
                        Console.WriteLine("Por favor, selecione uma opção válida (1, 2, 3, 4 ou 5).");
                        opcao = -1; // Reseta a opção para continuar o loop
                    }
                }
                catch (FormatException)
                {
                    // Se a entrada não for um número, exibe uma mensagem de erro
                    Console.WriteLine("Selecione o numero correspondente a opção desejada!!!");
                }
            } while (opcao == -1); // Continua o loop até que uma opção válida seja selecionada

            return opcao;           
        }
    }
}
