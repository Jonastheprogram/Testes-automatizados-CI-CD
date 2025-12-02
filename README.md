# Testes automatizados CI/CD para API

Este repositório contém o código-fonte e a infraestrutura de CI/CD para a API do projeto Api ESG FIAP. A aplicação foi desenvolvida em .NET, containerizada com Docker e configurada para deploy automatizado na nuvem Microsoft Azure através do GitHub Actions.

## Como executar localmente com Docker

Para executar a aplicação em seu ambiente de desenvolvimento local, garantindo consistência com os ambientes de nuvem, siga os passos abaixo.

**Pré-requisitos:**
* Docker e Docker Compose instalados.

**Passos:**
1.  Clone este repositório para a sua máquina.
2.  Na raiz do projeto, crie um arquivo chamado `.env` a partir do modelo `.env.example` e preencha as variáveis de ambiente necessárias para o desenvolvimento.
3.  Abra um terminal na raiz do projeto e execute o seguinte comando:
    
    `docker-compose up --build`
    
4.  A aplicação estará disponível no seu navegador ou ferramenta de API no endereço `http://localhost:8080`.

## ⚙️ Pipeline CI/CD com GitHub Actions

O pipeline de Integração Contínua e Deployment Contínuo (CI/CD) foi implementado utilizando **GitHub Actions**, a ferramenta de automação nativa do GitHub, para construir, testar e implantar a aplicação na nuvem Azure.

### Ferramentas Utilizadas
* **GitHub Actions:** Orquestração do workflow de CI/CD, execução de jobs e gerenciamento de segredos.
* **Azure Container Registry (ACR):** Repositório privado para armazenar as imagens Docker geradas pelo pipeline.
* **Azure App Service for Containers:** Serviço de hospedagem para executar a aplicação em contêineres nos ambientes de nuvem.

### Lógica e Etapas do Workflow
O workflow é definido no arquivo `.github/workflows/main.yml` e acionado a cada `push` na branch `main`. Consiste em três passos sequenciais:

1.  **`build` (Build, Test & Push):**
    * O código-fonte é clonado para um executor (runner) virtual.
    * As dependências do projeto .NET são restauradas e os testes automatizados são executados.
    * Uma imagem Docker é construída a partir do `Dockerfile`. 
    * A imagem é marcada com o hash do commit (garantindo rastreabilidade) e enviada (push) para o Azure Container Registry. 

2.  **`deploy-staging` (Deploy para Staging):**
    * Executado automaticamente após o sucesso do job `build`.
    * O Azure App Service do ambiente de **Staging** é atualizado com a nova imagem Docker. 
    * Este ambiente serve para validação final em um ambiente idêntico ao de produção. 

3.  **`deploy-production` (Deploy para Produção):**
    * Este depende do sucesso do deploy em Staging. 
    * **Possui uma trava de aprovação manual:** Utilizando a funcionalidade "Environments" do GitHub, o workflow pausa e exige que um revisor aprovado clique em "Approve" para continuar. Isso garante controle total sobre as liberações em produção. 
    * Após a aprovação, o Azure App Service de **Produção** é atualizado com a mesma imagem validada em Staging. 
      

## 🐳 Containerização

A aplicação é totalmente containerizada com Docker para garantir portabilidade, consistência e escalabilidade.

### Estratégia do Dockerfile
Foi utilizada uma abordagem de **múltiplos estágios (multi-stage builds)** no `Dockerfile` para criar uma imagem final otimizada, leve e segura.

1.  **Estágio `build`:** Usa a imagem completa do SDK do .NET para compilar a aplicação e executar os testes. Este estágio contém todas as ferramentas necessárias para o build, mas é descartado no final.
2.  **Estágio `final`:** Usa a imagem de runtime do ASP.NET, que é muito menor. Apenas os arquivos compilados da aplicação, gerados no estágio anterior, são copiados para a imagem final.


### Conteúdo do Dockerfile
# Estágio 1: Build 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Restaura as dependências da solução inteira 
COPY *.sln .
COPY Api.Esg.Fiap/*.csproj ./Api.Esg.Fiap/
RUN dotnet restore

# Copia todo o código
COPY . .



# Publica a aplicação 
WORKDIR /source/Api.Esg.Fiap
RUN dotnet publish -c Release -o /app/out --no-restore

# Estágio 2: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app


EXPOSE 8080

# Copia o resultado final da publicação
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Api.Esg.Fiap.dll"]


##  Prints do Funcionamento

**(Instrução: Insira aqui os seus prints. Substitua os `placeholders` abaixo)**

### Pipeline em Execução no GitHub Actions
![1](https://github.com/user-attachments/assets/d2b8b2ac-0769-49d0-85de-e396117ae73a)

### Ambiente de Staging Funcionando
![5](https://github.com/user-attachments/assets/bd91f62b-ddc5-4adf-b331-854bc7c70705)


### Aprovação Manual para Produção
![3](https://github.com/user-attachments/assets/867b4147-499d-4621-877c-9f651ca2f995)


### Ambiente de Produção Funcionando
![4](https://github.com/user-attachments/assets/1699edf5-eab0-479f-b00b-94ff0fad1145)



## 💻 Tecnologias Utilizadas
* **Backend:** .NET 8, C#, Oracle SQL
* **Containerização:** Docker, Docker Compose
* **Plataforma de CI/CD:** GitHub Actions
* **Nuvem:** Microsoft Azure (Azure App Service, Azure Container Registry)
* **Controle de Versão:** Git



