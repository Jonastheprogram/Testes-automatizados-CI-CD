# Projeto - Api Esg Fiap CI-CD

Este repositório contém o código-fonte e a infraestrutura de CI/CD para a API do projeto Api ESG FIAP. A aplicação foi desenvolvida em .NET, containerizada com Docker e configurada para deploy automatizado na nuvem Microsoft Azure através do Azure DevOps.

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

## ⚙️ Pipeline CI/CD

O pipeline de Integração Contínua e Deployment Contínuo (CI/CD) foi implementado utilizando **Azure DevOps** para automatizar todo o ciclo de vida da aplicação, desde o código até a produção.

### Ferramentas Utilizadas
* **Azure DevOps:** Orquestração do pipeline, gerenciamento de builds e releases.
* **Azure Container Registry (ACR):** Repositório privado para armazenar as imagens Docker geradas pelo pipeline.
* **Azure App Service for Containers:** Serviço de hospedagem para executar a aplicação em contêineres nos ambientes de nuvem.

### Etapas do Pipeline
O pipeline é definido no arquivo `azure-pipelines.yml` e acionado a cada `push` na branch `main`. Ele consiste em três estágios:

1.  **Build, Test & Push:**
    * O código-fonte é clonado.
    * As dependências do projeto .NET são restauradas.
    * Os testes automatizados são executados para garantir a qualidade do código. Se um teste falhar, o pipeline é interrompido.
    * Uma imagem Docker é construída a partir do `Dockerfile`.
    * A imagem é marcada com um número de build único e enviada (push) para o Azure Container Registry.

2.  **Deploy to Staging (testes):**
    * Executado automaticamente após o sucesso do estágio de Build.
    * O Azure App Service do ambiente de **Staging** é atualizado com a nova imagem Docker.
    * Este ambiente serve para validação e testes finais em um ambiente semelhante ao de produção.

3.  **Deploy to Production:**
    * Este estágio depende do sucesso do deploy em Staging.
    * **Possui uma trava de aprovação manual:** O pipeline pausa e aguarda a aprovação de um responsável. É uma boa prática para garantir que apenas versões validadas e aprovadas cheguem ao ambiente de produção.
    * Após a aprovação, o Azure App Service de **Produção** é atualizado com a mesma imagem que foi validada em Staging.

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

### Pipeline em Execução no Azure DevOps
`[COLE AQUI O PRINT DO SEU PIPELINE RODANDO COM SUCESSO]`

### Aprovação Manual para Produção
`[COLE AQUI O PRINT DA TELA DE APROVAÇÃO MANUAL]`

### Ambiente de Staging Funcionando
`[COLE AQUI O PRINT DA SUA API RODANDO NO AMBIENTE DE STAGING]`

### Ambiente de Produção Funcionando
`[COLE AQUI O PRINT DA SUA API RODANDO NO AMBIENTE DE PRODUÇÃO]`

## 💻 Tecnologias Utilizadas
* **Backend:** .NET 8, C#
* **Containerização:** Docker, Docker Compose
* **Plataforma de CI/CD:** Azure DevOps
* **Nuvem:** Microsoft Azure (Azure App Service, Azure Container Registry)
* **Controle de Versão:** Git



