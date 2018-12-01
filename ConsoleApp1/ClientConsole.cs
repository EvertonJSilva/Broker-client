using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourHealth.Domain.Entities;

namespace ConsoleApp1
{
    class ClientConsole
    {
        static void Main(string[] args)
        {
            bool sair = false;
            while (!sair)
            {
                Console.WriteLine("\nPara consultar digite 1, para incluir digite 2:");

                if (int.Parse(Console.ReadLine()) == 1)
                {
                    consultar();
                }
                else
                {
                    inserir();
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void consultar()
        {
            Console.WriteLine("\nDigite o id para consulta:");

            int id = int.Parse(Console.ReadLine());
            Beneficiario beneficiario = null;

            try
            {
                beneficiario = Proxy.ConsultarBeneficiarioPorId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (beneficiario == null)
                Console.WriteLine("\nId não encontrado");
            else
                Console.WriteLine("\nCpf do beneficiario: " + beneficiario.Cpf);

        }

        public static void inserir()
        {
            Console.WriteLine("\nDigite o nome do beneficiario:");

            string nome = Console.ReadLine();

            Console.WriteLine("\nDigite o CPF do beneficiario:");

            string cpf = Console.ReadLine();

            Beneficiario beneficiario = new Beneficiario();
            beneficiario.Nome = nome;
            beneficiario.Cpf = cpf;
            beneficiario.Id = 0;

            string retorno = "";
            try
            {
               retorno = Proxy.IncluirBeneficiario(beneficiario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(retorno);
          
        }
    }
}
