# CLIQX GDS HUB
Version of plattaform GDS, for API and sales manangment

## Start server mariadb homebew
brew services start MariaDB

## Create database
Instalar utilitario do dotnet ef (Caso não tenha instalado)
dotnet tool install --global dotnet-ef

CREATE USER 'cliqxgds'@'localhost' IDENTIFIED BY 'cliqxgds#2026';
GRANT ALL PRIVILEGES ON * . * TO 'cliqxgds'@'localhost';

->ef core migration dentro do repositoty

(Criar migration caso ela não exista)
dotnet ef --startup-project ../cliqx.gds.api migrations add Init
dotnet ef --startup-project ../cliqx.gds.api database update    


-- Remover ultima migration não aplicada
dotnet ef --startup-project ../cliqx.gds.api migrations remove

-- Remover ultima migration aplicada
dotnet ef --startup-project ../cliqx.gds.api database update 0

## Observações

- Não fazer alterações de banco fora do entity framework
- Manter as migrations atualizadas e de acordo com o banco de produção

## Manual do publish NET CORE
dotnet publish cliqx.gds.api/cliqx.gds.api.csproj --configuration "Release" --framework net7.0 /p:TargetLatestRuntimePatch=false --self-contained true -r linux-x64 --output "cliqx.gds.api/bin/Release/net7.0/publish"

