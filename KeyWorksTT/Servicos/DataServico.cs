using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Ruan_Barbosa_TT.Servicos;

//Se ocorrer o erro de não achou o arquivo "Empresa.json", siga o passos abaixo:
//Botao direito em "Empresa.json" -> Propriedades -> Não Copiar -> Copiar se for mais novo
public class DataServico
{
    private const string ArquivoEmpresa = "Data/Empresa.json";
    private const string ArquivoEvento = "Data/Evento.json";
    private const string SaveFilePath = "Data/SavedState.json";

    public List<Empresa> Empresas { get; private set; }

    public List<Evento> Eventos { get; private set; }

    public UserData UserData { get; private set; }


    public DataServico()
    {
        LoadInitialData();
        LoadUserData();
    }

    private void LoadInitialData()
    {
        try
        {
            // Carrega as empresas
            if (!File.Exists(ArquivoEmpresa))
                throw new FileNotFoundException($"Arquivo {ArquivoEmpresa} não foi encontrado!");
            string empresaJson = File.ReadAllText(ArquivoEmpresa);
            Empresas = JsonSerializer.Deserialize<List<Empresa>>(empresaJson) ?? new List<Empresa>();

            // Carrega os eventos de mercado, corrige o erro de ID duplicado "empresa não encontrada"
            if (!File.Exists(ArquivoEvento))
                throw new FileNotFoundException($"Arquivo {ArquivoEvento} não foi encontrado!");
            string eventoJson = File.ReadAllText(ArquivoEvento);
            var eventosCarregados = JsonSerializer.Deserialize<List<Evento>>(eventoJson) ?? new List<Evento>();

            Eventos = CorrigirIdsDuplicados(eventosCarregados);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
    }

    private List<Evento> CorrigirIdsDuplicados(List<Evento> eventos)
    {
        //peguei essa lógica de um código da fercien
        //assim eu verifico primeiro se o id já existe na lista deeventos
        HashSet<int> idsExistentes = new HashSet<int>(eventos.Select(e => e.Id));

        foreach (var evento in eventos)
        {
            while (idsExistentes.Contains(evento.Id))
            {
                evento.Id = GerarNovoIdUnico(idsExistentes);
            }
            idsExistentes.Add(evento.Id);
        }
        return eventos;
    }

    private int GerarNovoIdUnico(HashSet<int> idsExistentes)
    {
        Random random = new Random();
        int novoId;
        do
        {
            novoId = random.Next(1000, 9999); // Gera um ID dentro desse intervalo de ID's 
        } while (idsExistentes.Contains(novoId));
        return novoId;
    }

    private void LoadUserData()
    {
        if (File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            var savedState = JsonSerializer.Deserialize<SavedState>(json);
            UserData = savedState?.User ?? new UserData { Carteira = 10000.00m, 
                                                          Portfolio = new List<PortfolioItem>() };

            if (savedState?.Empresas != null)
            {
                foreach (var valorSalvo in savedState.Empresas)
                {
                    var empresa = Empresas.Find(c => c.Id == valorSalvo.Id);
                    if (empresa != null)
                        empresa.ValorDaAcao = valorSalvo.ValorDaAcao;
                }
            }
        }
        else
        {
            UserData = new UserData { Carteira = 10000.00m, Portfolio = new List<PortfolioItem>() };
        }
    }

    public void SaveUserData()
    {
        var savedState = new SavedState
        {
            Empresas = Empresas,
            User = UserData
        };
        string json = JsonSerializer.Serialize(savedState);
        File.WriteAllText(SaveFilePath, json);
    }

    public void ReloadUserData()
    {
        LoadUserData();
    }
}