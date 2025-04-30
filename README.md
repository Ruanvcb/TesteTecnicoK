📈 Simulador de Bolsa de Valores em C#

Este é um simulador de bolsa de valores desenvolvido em C# como um desafio técnico. O programa permite que os usuários comprem e vendam ações de empresas fictícias, apliquem eventos de mercado que afetam os preços das ações e salvem/carreguem seu progresso.

🚀 Funcionalidades:

    Compra e venda de ações de empresas fictícias

    Eventos de mercado aleatórios que impactam os preços

    Salvar e carregar progresso em arquivo JSON

    Portfólio com acompanhamento de investimentos

    Sistema de logs para registro de transações

Pré-requisitos:

    .NET 6.0 ou superior


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

    git clone 
    https://github.com/Ruanvcb/TesteTecnicoK.git

    Navegue até a pasta do projeto:
    bash

    cd simulador-bolsa-valores

    Compile e execute o projeto:
    bash

    dotnet run

📝 Como Usar
    Menu Principal:

        Visualize empresas, seus setores e preços atuais

        Acompanhe seu portfólio e saldo

    Opções:

        1: Comprar ações

        2: Vender ações

        3: Aplicar evento de mercado aleatório

        4: Salvar progresso

        5: Carregar progresso

        6: Sair

    Eventos de Mercado:

        Impactam aleatoriamente os preços das ações

        Podem ser positivos ou negativos

⚙️ Configuração Importante

Se ocorrer erro de arquivo não encontrado:

    Clique com o botão direito em Empresa.json ou Evento.json

    Selecione "Propriedades"

    Defina "Copiar para Diretório de Saída" como "Copiar se for mais novo"

🐛 Solução de Problemas

    "Empresa não encontrada para evento": Verifique se os IDs em Evento.json correspondem aos IDs em Empresa.json

    IDs duplicados: O sistema automaticamente gera novos IDs únicos para eventos

📜 Logs

Todas as transações (compras, vendas) e eventos de mercado são registrados no arquivo Logs/transactions.log. 
Exemplo:

    2023-10-01 14:30:45 - Compra de 10 ações da TechCorp por R$100.00
    2023-10-01 14:35:12 - Evento 'Aumento na demanda por tecnologia' aplicado. TechCorp alterou de R$100.00 para R$110.00

🛑 Considerações Finais

    O usuário começa com um saldo inicial de R$ 10.000,00.

    O arquivo SavedState.json é criado automaticamente ao salvar o progresso.

    O arquivo transactions.log é criado automaticamente para registrar todas as transações.


📊 Exemplo de Saída

    === BOLSA DE VALORES ===
    Empresas e Valores Atuais:
    
    NVIDIA - Setor de: Tecnologia
     Preço: R$ 165,00
     Descrição: Líder em placas de vídeo e IA
    
    Seu Portfólio:
    NVIDIA - Quantidade: 10
    
    Saldo Disponível: R$ 8.350,00
    Valor Total: R$ 9.850,00


👨‍💻 Autor

Ruan Vagner Cardozo Barbosa

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.
