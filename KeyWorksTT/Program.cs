using KeyWorksTT.Servicos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

//Anotações referente ao Teste esstão no Notion - Estudo.

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