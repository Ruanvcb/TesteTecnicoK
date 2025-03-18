using Ruan_Barbosa_TT.Servicos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

//Se ocorrer o erro de não achou o arquivo "Empresa.json", siga o passos abaixo:
//Botao direito em "Empresa.json" -> Propriedades -> Não Copiar -> Copiar se for mais novo


//Não recebi as infos da Pasta/Data no dia do recebimento do teste,
//Então eu "criei" algumas empresas e eventos para o /Data.
//Quando recebi o /Data fornecido, adicionei os mesmos.
//Tive que mudar a lógica pq agora tem mais Eventos que Empresas

class Program
{
    static void Main(string[] args)
    {
        DataServico dataServico = new DataServico();
        MenuServico menuServico = new MenuServico();
        bool running = true;

        while (running)
        {
            Console.Clear();
            menuServico.DisplayHeader(dataServico);
            menuServico.DisplayMenu();

            string escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    menuServico.ComprarAcao(dataServico);
                    break;
                case "2":
                    menuServico.VenderAcao(dataServico);
                    break;
                case "3":
                    menuServico.AplicarEvento(dataServico);
                    break;
                case "4":
                    dataServico.SaveUserData();
                    Console.WriteLine("Progresso salvo com sucesso.");
                    break;
                case "5":
                    dataServico.ReloadUserData();
                    Console.WriteLine("Progresso carregado com sucesso.");
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}