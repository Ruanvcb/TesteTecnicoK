📈 Simulador de Bolsa de Valores em C#

Este é um simulador de bolsa de valores desenvolvido em C# como um desafio técnico. O programa permite que os usuários comprem e vendam ações de empresas fictícias, apliquem eventos de mercado que afetam os preços das ações e salvem/carreguem seu progresso.
🚀 Funcionalidades:

    Compra e venda de ações: Compre e venda ações de empresas fictícias.

    Eventos de mercado: Eventos aleatórios que afetam os preços das ações.

    Salvar e carregar progresso: Salve seu progresso em um arquivo JSON e continue de onde parou.

    Interface de console: Menu interativo para facilitar a navegação.

    Logs de transações: Todas as compras, vendas e eventos são registrados em um arquivo de log.

    BolsaDeValores/
    ├── Data/
    │   ├── Empresas.json          # Dados das empresas e preços iniciais das ações
    │   ├── Eventos.json           # Eventos de mercado que afetam os preços das ações
    │   └── SavedState.json        # Progresso salvo do usuário (criado automaticamente)
    ├── Logs/
    │   └── transactions.log       # Logs de todas as transações e eventos (criado automaticamente)
    ├── Models/                    # Classes de modelo
    │   ├── Company.cs             # Representa uma empresa
    │   ├── MarketEvent.cs         # Representa um evento de mercado
    │   ├── PortfolioItem.cs       # Representa um item no portfólio do usuário
    │   └── UserData.cs            # Armazena o saldo e o portfólio do usuário
    ├── Services/                  # Serviços de lógica de negócio
    │   ├── DataService.cs         # Gerencia o carregamento e salvamento de dados
    │   └── MenuService.cs         # Gerencia a exibição do menu e as operações do usuário
    └── Program.cs                 # Ponto de entrada da aplicação

    🛠️ Como Executar o Projeto
Pré-requisitos

    .NET SDK instalado.

    Um editor de código como Visual Studio ou VS Code.

Passos para Executar

    Clone o repositório:
    bash
    Copy

    git clone https://github.com/seu-usuario/simulador-bolsa-valores.git

    Navegue até a pasta do projeto:
    bash
    Copy

    cd simulador-bolsa-valores

    Compile e execute o projeto:
    bash
    Copy

    dotnet run

📝 Como Usar

    Menu Principal:

        O menu exibe o saldo do usuário, o portfólio de ações e o valor total do patrimônio.

        Escolha uma opção digitando o número correspondente.

    Comprar Ações:

        Selecione uma empresa da lista.

        Informe a quantidade de ações que deseja comprar.

        O valor será debitado do seu saldo.

    Vender Ações:

        Selecione uma empresa da lista de ações que você possui.

        Informe a quantidade de ações que deseja vender.

        O valor será creditado no seu saldo.

    Aplicar Evento de Mercado:

        Um evento aleatório será aplicado, alterando o preço das ações de uma empresa.

    Salvar Progresso:

        Salva o estado atual (preços das ações, saldo e portfólio) em um arquivo JSON.

    Carregar Progresso:

        Carrega o estado salvo anteriormente.

    Sair:

        Encerra a aplicação.

📄 Arquivos de Configuração
Empresas.json

Contém a lista de empresas e os preços iniciais das ações. Exemplo:
json
Copy

[
  {
    "Id": 1,
    "Nome": "NVIDIA",
    "ValorDaAcao": 165.00
  },
]

Eventos.json

Contém os eventos de mercado que afetam os preços das ações. Exemplo:
json
Copy

[
  {
    "Id": 1,
    "Descricao": "Aumento no Mercado de IA",
    "EmpresaAfetadaId": 1,
    "Porcentagem": 17.0
  },
]

📜 Logs

Todas as transações (compras, vendas) e eventos de mercado são registrados no arquivo Logs/transactions.log. Exemplo:
Copy

2023-10-01 14:30:45 - Compra de 10 ações da TechCorp por R$1,000.00
2023-10-01 14:35:12 - Evento 'Aumento na demanda por tecnologia' aplicado. TechCorp alterou de R$100.00 para R$110.00

🛑 Considerações Finais

    O usuário começa com um saldo inicial de R$ 10.000,00.

    O arquivo SavedState.json é criado automaticamente ao salvar o progresso.

    O arquivo transactions.log é criado automaticamente para registrar todas as transações.

👨‍💻 Autor

Ruan Vagner Cardozo Barbosa
📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.
