using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyWorksTT.Servicos;

public class MenuServico
{
    public void DisplayHeader(DataServico dataServico) 
    {
        Console.WriteLine("=== BOLSA DE VALORES ===");
        Console.WriteLine("Empresas e Valores Atuais:");
        foreach (var empresa in dataServico.Empresas)
            Console.WriteLine($"{empresa.Nome} - Preço: {empresa.ValorDaAcao:C}");

        Console.WriteLine("\nSeu Portifólio de Ações:");
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
        Console.WriteLine($"Valor total da sua Carteira de Ações: {valorTotal:C}\n");
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
                    Console.WriteLine("Compra de Açõees realizada com sucesso!");
                }
                else
                    Console.WriteLine("Saldo insuficiente para comprar Ações! Perdeu tudo no tigrinho.");
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
        if(dataServico.Eventos.Count == 0)
        {
            Console.WriteLine("Não há eventos que afetem o Mercado no momento.");
            return;
        }

        var random = new Random(); //travei 1 hora só nisso
        var eventos = dataServico.Eventos[random.Next(dataServico.Eventos.Count)];
        var empresa = dataServico.Empresas.Find(c => c.Id == eventos.EmpresaAfetadaId);
        if (empresa == null)
        {
            Console.WriteLine("Empresa afetada pelo evento não encontrada.");
            return;
        }

        decimal precoAntigo = empresa.ValorDaAcao;
        empresa.ValorDaAcao *= (1 + eventos.Porcentagem / 100);
        Console.WriteLine($"O Evento Aplicado foi: {eventos.Descricao}");
        Console.WriteLine($"Novo preço da Ação da empresa: {empresa.Nome}: {empresa.ValorDaAcao:C}");//Se não tiver o C ele adiciona casas decimais
    }

}