using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using KeyWorksTT.Servicos;

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
            if (!File.Exists(ArquivoEmpresa))
                throw new FileNotFoundException("Arquivo Empresa.json não foi encontrado!");
            string empresaJson = File.ReadAllText(ArquivoEmpresa);
            Empresas = JsonSerializer.Deserialize<List<Empresa>>(empresaJson);

            if (!File.Exists(ArquivoEvento))
                throw new FileNotFoundException("Arquivo Evento.json não foi encontrado!");
            string eventoJson = File.ReadAllText(ArquivoEvento);
            Eventos = JsonSerializer.Deserialize<List<Evento>>(eventoJson);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
    }

    private void LoadUserData()
    {
        if (File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            var savedState = JsonSerializer.Deserialize<SavedState>(json);
            UserData = savedState.User;

            foreach (var valorSalvo in savedState.Empresas)
            {
                var empresas = Empresas.Find(c => c.Id == valorSalvo.Id);
                if (empresas != null)
                    empresas.ValorDaAcao = valorSalvo.ValorDaAcao;
            }
        }
        else {
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