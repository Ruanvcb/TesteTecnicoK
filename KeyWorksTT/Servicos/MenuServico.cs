using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruan_Barbosa_TT.Servicos;

public class MenuServico
{
    public void DisplayHeader(DataServico dataServico) 
    {

        Console.WriteLine("=== BOLSA DE VALORES ===");
        Console.WriteLine("Empresas e Valores Atuais:");
        Console.WriteLine();

        foreach (var empresa in dataServico.Empresas)
        { 
            Console.WriteLine($"{empresa.Nome} - Setor de: {empresa.Setor}");
            Console.WriteLine($" Preço: {empresa.ValorDaAcao:C}");
            Console.WriteLine($" Decricao: {empresa.Descricao}");
            Console.WriteLine();
            Console.WriteLine();
        }
        
        Console.WriteLine("\nSeu Portfólio de Ações:");
        foreach (var item in dataServico.UserData.Portfolio)
        {
            var empresa = dataServico.Empresas.Find(c => c.Id == item.EmpresaId);
            Console.WriteLine($"{empresa.Nome} - Quantidade de ações: {item.Quantidade} ");
        }

        decimal valorTotal = dataServico.UserData.Carteira;
        foreach (var item in dataServico.UserData.Portfolio)
        {
            var empresa = dataServico.Empresas.Find(c => c.Id == item.EmpresaId);
            valorTotal += item.Quantidade * empresa.ValorDaAcao;
        }
        Console.WriteLine($"\nSaldo Disponível de: {dataServico.UserData.Carteira:C}");
        Console.WriteLine($"Valor Inicial total da sua Carteira de Ações: {valorTotal:C}\n");
    }

    public void DisplayMenu()
    {
        Console.WriteLine("Opções:");
        Console.WriteLine("1. Comprar Ações");
        Console.WriteLine("2. Vender Ações");
        Console.WriteLine("3. Aplicar Mudança de Mercado");
        Console.WriteLine("4. Salvar Progresso");
        Console.WriteLine("5. Carregar Progresso");
        Console.WriteLine("6. Sair");
        Console.Write("Escolha uma opção: ");
        Console.WriteLine();
    }

    public void ComprarAcao(DataServico dataServico)
    {
        Console.WriteLine("\nEscolha uma empresa para comprar Ações:");
        for (int i = 0; i < dataServico.Empresas.Count; i++)
            Console.WriteLine($"{i + 1}. {dataServico.Empresas[i].Nome} - Preço da Ação: {dataServico.Empresas[i].ValorDaAcao:C}");

        if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 1 && escolha <= dataServico.Empresas.Count)
        {
            var empresa = dataServico.Empresas[escolha - 1];
            Console.Write("Quantidade: ");
            if (int.TryParse(Console.ReadLine(), out int quantidade) && quantidade > 0)
            {
                decimal custo = empresa.ValorDaAcao * quantidade;
                if (dataServico.UserData.Carteira >= custo)
                {
                    var portfolioItem = dataServico.UserData.Portfolio.Find(p => p.EmpresaId == empresa.Id);
                    if (portfolioItem != null)
                        portfolioItem.Quantidade += quantidade;
                    else
                        dataServico.UserData.Portfolio.Add(new PortfolioItem { EmpresaId = empresa.Id, Quantidade = quantidade });
                    dataServico.UserData.Carteira -= custo; //isso atualiza o saldo depois de comprar as açõess
                    Console.WriteLine("Compra de Ações realizada com sucesso!");
                }
                else
                    Console.WriteLine("Saldo insuficiente para a compra Ações!");
            }
            else
                Console.WriteLine("Quantidade de Ações Inválidad!");
        }
        else
            Console.WriteLine("Opção selecionada é Inválida!");
    }

    public void VenderAcao(DataServico dataServico)
    {
        if (dataServico.UserData.Portfolio.Count == 0) 
        {
            Console.WriteLine("Você não possue Ações para vender, compre algumas!");
            return;
        }

        Console.WriteLine("\nEscolha a Empresa que você possue Açõess para vender: ");
        var acoesVender = dataServico.UserData.Portfolio.Select(p => dataServico.Empresas.Find(c => c.Id == p.EmpresaId)).ToList();
        for (int i = 0; i < acoesVender.Count; i++)
        {
            var empresa = acoesVender[i];
            var item = dataServico.UserData.Portfolio.Find(p => p.EmpresaId == empresa.Id);
            Console.WriteLine($"{i + 1}. {empresa.Nome} - Quantidade: {item.Quantidade} - Preço das Ações: {empresa.ValorDaAcao:C}");
        }

        if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 1 && escolha <= acoesVender.Count)
        {
            var empresa = acoesVender[escolha - 1];
            var item = dataServico.UserData.Portfolio.Find(p => p.EmpresaId == empresa.Id);
            Console.WriteLine($"Quantidade de Ações que serão vendidas: (max {item.Quantidade}):");
            if (int.TryParse(Console.ReadLine(), out int quantidade) && quantidade > 0 && quantidade <= item.Quantidade)
            {
                decimal proceder = empresa.ValorDaAcao * quantidade;
                item.Quantidade -= quantidade;
                if (item.Quantidade == 0)
                    dataServico.UserData.Portfolio.Remove(item);
                dataServico.UserData.Carteira += proceder;
                Console.WriteLine("A venda das Ações foi realizada com Sucesso!");
            }
            else
                Console.WriteLine("Quantidade de Ações Inválida!");
        }
        else
            Console.WriteLine("A opção selecionada é inválida!");
    }

    public void AplicarEvento(DataServico dataServico)
    {
        if (dataServico.Eventos.Count == 0)
        {
            Console.WriteLine("Não há eventos que afetem o Mercado no momento.");
            return;
        }

        var random = new Random();
        var eventosValidos = dataServico.Eventos.Where(e => dataServico.Empresas.Any(emp => emp.Id == e.EmpresaAfetadaId)).ToList();

        //Antes dava a mensagem que não foi encotrada a empresa pra aplicar o evento
        //pq havia a duplicidade dos Id'ss

        if (eventosValidos.Count == 0)
        {
            Console.WriteLine("Nenhum evento foi aplicado, pois nenhuma empresa correspondente foi encontrada.");
            return;
        }

        var evento = eventosValidos[random.Next(eventosValidos.Count)];
        var empresa = dataServico.Empresas.Find(c => c.Id == evento.EmpresaAfetadaId);

        decimal precoAntigo = empresa.ValorDaAcao;
        empresa.ValorDaAcao *= (1 + evento.Porcentagem / 100);

        Console.WriteLine($"O Evento Aplicado foi: {evento.Titulo}");
        Console.WriteLine();
        Console.WriteLine($"Descrição: {evento.Descricao}");
        Console.WriteLine();
        Console.WriteLine($"{empresa.Nome} alterou o valor de {precoAntigo:C} para {empresa.ValorDaAcao:C}");
        Console.WriteLine();
    }
    
}