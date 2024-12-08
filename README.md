
# BlipChat - API RESTful para Integração com GitHub
Descrição

Este repositório contém a implementação de uma API RESTful construída em C# que integra com a API pública do GitHub para obter informações sobre repositórios da organização takenet. A API fornece os 5 repositórios mais antigos de C#, ordenados do mais antigo para o mais novo, para ser consumida por um chatbot na plataforma Blip.ai.

A API segue o padrão RESTful, utilizando práticas de Clean Code e SOLID para garantir uma estrutura organizada, legível e extensível.

A plataforma utilizada para testar a API publicamente é a Railway:

https://railway.app/

## Instalação e Execução
```bash
# Download do projeto
link: https://github.com/FMRavelli/BlipChat
# Build e Execução
cd BlipChat
dotnet run
./BlipChat
```

## Estrutura
```
BlipChat/
│
├── api/                    # Diretório principal da API
│   ├── Controllers/        # Controladores da API
│   │   └── GitHubController.cs  # Controlador para manipular requisições sobre repositórios
│   ├── Services/           # Serviços que contêm a lógica de negócios
│   │   └── GitHubService.cs  # Serviço que faz a integração com a API do GitHub
│   ├── Repositories/       # Repositórios que interagem com a API externa (GitHub)
│   │   └── GitHubRepository.cs  # Repositório que faz chamadas à API pública do GitHub
│   ├── Models/             # Modelos de dados utilizados na aplicação
│   │   └── GitHubUser.cs   # Modelo representando o usuário do GitHub
│   └── Startup.cs          # Configuração e inicialização da aplicação
│
├── .gitignore              # Arquivo para ignorar arquivos temporários
├── README.md               # Este arquivo de documentação
└── appsettings.json        # Configurações da aplicação
```

## API

### `/api/github/user`

* **Método**: `GET`  
  **Descrição**: Consulta a lista de repositórios do usuário GitHub e a URL do avatar associado.

#### Retorno

```json
[
  {
    "avatarUrl": "https://avatars.githubusercontent.com/u/4369522?v=4",
    "repositories": [
      {
        "title": "library.data",
        "description": "Provides a simple abstraction for implementing the repository and unit of work patterns for data-enabled applications"
      },
      {
        "title": "library.logging",
        "description": "Provides a simple logging interface for applications and some basic implementations of this interface"
      },
      {
        "title": "libphonenumber-csharp",
        "description": "Forking original c# port"
      },
      {
        "title": "Takenet.ScoreSystem",
        "description": "Takenet score system"
      },
      {
        "title": "ServiceStack.Text",
        "description": ".NET's fastest JSON, JSV and CSV Text Serializers"
      }
    ]
  }
]
