CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `UserForgetCodes` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `ForgetCode` longtext CHARACTER SET utf8mb4 NULL,
    `ExpirationDate` datetime(6) NOT NULL,
    CONSTRAINT `PK_UserForgetCodes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentServices` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `ApiBaseUrl` longtext CHARACTER SET utf8mb4 NULL,
    `CredentialsJsonObject` varchar(8192) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(255) CHARACTER SET utf8mb4 NULL,
    `ExternalId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `UpdatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    `UserCreatedId` int NULL,
    `UserUpdatedId` int NULL,
    `IsActive` tinyint(1) NOT NULL,
    `InternalProperty` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PaymentServices` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PaymentServices_AspNetUsers_UserCreatedId` FOREIGN KEY (`UserCreatedId`) REFERENCES `AspNetUsers` (`Id`),
    CONSTRAINT `FK_PaymentServices_AspNetUsers_UserUpdatedId` FOREIGN KEY (`UserUpdatedId`) REFERENCES `AspNetUsers` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Plugin` (
    `Id` varchar(36) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `ExternalId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `UpdatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    `UserCreatedId` int NULL,
    `UserUpdatedId` int NULL,
    `IsActive` tinyint(1) NOT NULL,
    `InternalProperty` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Plugin` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Plugin_AspNetUsers_UserCreatedId` FOREIGN KEY (`UserCreatedId`) REFERENCES `AspNetUsers` (`Id`),
    CONSTRAINT `FK_Plugin_AspNetUsers_UserUpdatedId` FOREIGN KEY (`UserUpdatedId`) REFERENCES `AspNetUsers` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ShoppingCartServices` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `ApiBaseUrl` longtext CHARACTER SET utf8mb4 NULL,
    `CredentialsJsonObject` varchar(8192) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(255) CHARACTER SET utf8mb4 NULL,
    `ExternalId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `UpdatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    `UserCreatedId` int NULL,
    `UserUpdatedId` int NULL,
    `IsActive` tinyint(1) NOT NULL,
    `InternalProperty` tinyint(1) NOT NULL,
    CONSTRAINT `PK_ShoppingCartServices` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ShoppingCartServices_AspNetUsers_UserCreatedId` FOREIGN KEY (`UserCreatedId`) REFERENCES `AspNetUsers` (`Id`),
    CONSTRAINT `FK_ShoppingCartServices_AspNetUsers_UserUpdatedId` FOREIGN KEY (`UserUpdatedId`) REFERENCES `AspNetUsers` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PluginConfiguration` (
    `Id` varchar(36) CHARACTER SET utf8mb4 NOT NULL,
    `PluginId` varchar(36) CHARACTER SET utf8mb4 NOT NULL,
    `ApiBaseUrl` longtext CHARACTER SET utf8mb4 NULL,
    `CredentialsJsonObject` varchar(8192) CHARACTER SET utf8mb4 NULL,
    `Options` varchar(255) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(255) CHARACTER SET utf8mb4 NULL,
    `TransactionName` varchar(255) CHARACTER SET utf8mb4 NULL,
    `TransactionLocator` varchar(255) CHARACTER SET utf8mb4 NULL,
    `StoreId` bigint NOT NULL,
    `ShoppingCartId` int NOT NULL,
    `PaymentServiceId` int NOT NULL,
    `ExtraData` varchar(4000) CHARACTER SET utf8mb4 NULL,
    `ExternalId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `UpdatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    `UserCreatedId` int NULL,
    `UserUpdatedId` int NULL,
    `IsActive` tinyint(1) NOT NULL,
    `InternalProperty` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PluginConfiguration` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PluginConfiguration_AspNetUsers_UserCreatedId` FOREIGN KEY (`UserCreatedId`) REFERENCES `AspNetUsers` (`Id`),
    CONSTRAINT `FK_PluginConfiguration_AspNetUsers_UserUpdatedId` FOREIGN KEY (`UserUpdatedId`) REFERENCES `AspNetUsers` (`Id`),
    CONSTRAINT `FK_PluginConfiguration_PaymentServices_PaymentServiceId` FOREIGN KEY (`PaymentServiceId`) REFERENCES `PaymentServices` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_PluginConfiguration_Plugin_PluginId` FOREIGN KEY (`PluginId`) REFERENCES `Plugin` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_PluginConfiguration_ShoppingCartServices_ShoppingCartId` FOREIGN KEY (`ShoppingCartId`) REFERENCES `ShoppingCartServices` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UsersPlugins` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Index` int NOT NULL,
    `UserId` int NOT NULL,
    `PluginConfigurationsId` varchar(36) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_UsersPlugins` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UsersPlugins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_UsersPlugins_PluginConfiguration_PluginConfigurationsId` FOREIGN KEY (`PluginConfigurationsId`) REFERENCES `PluginConfiguration` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

INSERT INTO `PaymentServices` (`Id`, `ApiBaseUrl`, `CredentialsJsonObject`, `Description`, `ExternalId`, `InternalProperty`, `IsActive`, `Name`, `UserCreatedId`, `UserUpdatedId`)
VALUES (1, '{"UrlBaseTreeal":"https://bank-api-production-dot-snog-382317.ue.r.appspot.com/api/v1/bank"}', '{"TokenTreeal":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiU25vZyIsImVudiI6InByb2QiLCJpZCI6ImU1ZWRmNzM2LTY2NjQtNGRiYi1iYTA0LTI5ZDY4NDFmZDBmZCJ9.OscGPowi60QeGSjdPX-09mIhqQ4EPxdEE4GN0jjGrNU","PixKeyTreeal":"ce276502-0206-4b9c-abfe-57e9db60f01e","ExpirationSecondsTreeal":600,"UrlReturnTreeal":"https://np33nn2ki5.execute-api.us-east-2.amazonaws.com/v1/webhook/treeal"}', NULL, 'f2f6921f-470b-442e-957f-80d61a0b951f', FALSE, TRUE, 'DefaultPay', NULL, NULL),
(2, '{"UrlBase":"https://checkout.cliqx.com.br"}', '{"Token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiU25vZyIsImVudiI6InByb2QiLCJpZCI6ImU1ZWRmNzM2LTY2NjQtNGRiYi1iYTA0LTI5ZDY4NDFmZDBmZCJ9.OscGPowi60QeGSjdPX-09mIhqQ4EPxdEE4GN0jjGrNU","PixKey":"ce276502-0206-4b9c-abfe-57e9db60f01e","ExpirationSeconds":600,"UrlReturn":"https://np33nn2ki5.execute-api.us-east-2.amazonaws.com/v1/webhook/treeal","CartaoJuros":0.0299,"MaxParcelas":"3","CartaoExpiracaoLink":"","PixExpiracaoLink":""}', NULL, 'feed115c-0ca8-4607-bec1-a9b91a8f2f55', FALSE, TRUE, 'FacilitaPay', NULL, NULL);

INSERT INTO `Plugin` (`Id`, `ExternalId`, `InternalProperty`, `IsActive`, `Name`, `UserCreatedId`, `UserUpdatedId`)
VALUES ('09a5347d-2eeb-4a6a-a5b8-dea9e9525b16', 'cc13905e-b063-4496-a968-bab49a310adb', FALSE, TRUE, 'Smartbus', NULL, NULL),
('1c7c73b5-a694-4426-b751-baa165b79ed1', '798f43ed-098d-4bc8-aa34-fff94fdeae12', FALSE, TRUE, 'ClickBus', NULL, NULL),
('4434b7e9-942d-4fb0-bd4b-09e3a4408a41', '24f6d213-f507-4b60-ac4b-ecb63fa0f670', FALSE, TRUE, 'Distribusion', NULL, NULL),
('8ecb43ae-7e34-4789-a6a3-1f3ea3884be5', 'd99224d9-6746-4351-baa2-914b44c5f245', FALSE, TRUE, 'Rodosoft', NULL, NULL),
('bcdf6d22-2823-478c-b44f-5fecb733b022', 'b8717116-882d-48b7-b5da-5dbfdf596dd7', FALSE, TRUE, 'RjConsultores', NULL, NULL);

INSERT INTO `ShoppingCartServices` (`Id`, `ApiBaseUrl`, `CredentialsJsonObject`, `Description`, `ExternalId`, `InternalProperty`, `IsActive`, `Name`, `UserCreatedId`, `UserUpdatedId`)
VALUES (1, NULL, NULL, NULL, 'fcae4400-4309-44fd-89c9-beaeaf100223', FALSE, TRUE, 'RecargaPlus', NULL, NULL);

INSERT INTO `PluginConfiguration` (`Id`, `ApiBaseUrl`, `CredentialsJsonObject`, `Description`, `ExternalId`, `ExtraData`, `InternalProperty`, `IsActive`, `Options`, `PaymentServiceId`, `PluginId`, `ShoppingCartId`, `StoreId`, `TransactionLocator`, `TransactionName`, `UserCreatedId`, `UserUpdatedId`)
VALUES ('39bd4c85-68a7-4c74-a8d5-bd90c21837f6', '{"BaseUrl":"http://rjconsultores:2023/api","UrlRodosoft":"http://localhost:2023/api","BaseUrlRecargaPlus":"http://recargaplus:2023/api"}', '{"apiKey":"eyAasdas123"}', '', '49237d22-b05a-4d47-a596-598dfae13d56', NULL, FALSE, TRUE, NULL, 2, '8ecb43ae-7e34-4789-a6a3-1f3ea3884be5', 1, 1, 'RODOSOFT_LOCALIZADOR', 'RODOSOFT_TRANSACAO', NULL, NULL),
('9e4d313b-7041-43d1-9f22-87bc72c188a5', '{"BaseUrl":"https://api.distribusion.com","RecargaPlusUrl":"https://app.snog.com.br/recarga-plus","DistribusionEtlUrl":"http://localhost:20231"}', '{"ApiKey":"o6tLyEqo12zLetXXyZ0eM2kKr2C1abrfe3jhO74j","RecargaPlusStore":"1","RetailerPartnerNumber":"609180","SmtpPassword":"eyAasdas123"}', 'Distribusion', '326daf04-db35-4485-8a18-6478e2be522e', NULL, FALSE, TRUE, NULL, 2, '4434b7e9-942d-4fb0-bd4b-09e3a4408a41', 1, 1, 'DISTRIBUSION_LOCALIZADOR', 'DISTRIBUSION_TRANSACAO', NULL, NULL),
('a3c2b595-d552-4e83-b538-365c09f62063', '{"BaseUrl":"http://rjconsultores:2023/api","UrlClickBus":"http://localhost:2023/api","BaseUrlRecargaPlus":"http://recargaplus:2023/api"}', '{"apiKey":"eyAasdas123"}', '', '4ac98bb8-ffbf-446b-b285-fe3727ed1166', NULL, FALSE, TRUE, NULL, 2, '1c7c73b5-a694-4426-b751-baa165b79ed1', 1, 1, 'CLICKBUS_LOCALIZADOR', 'CLICKBUS_TRANSACAO', NULL, NULL),
('b420bf9b-068a-41f7-b726-dcc32898f399', '{"BaseUrl":"http://3.141.251.80"}', '{"Authorization":"Basic c25vZzpzbm9n","TenantId":"e9ef81ac-1fd0-40a2-84c0-df077dc46e4b","RecargaPlusUser":"omshub-api","RecargaPlusPassword":"999999","RecargaPlusStore":"23"}', '', '428a921d-2c0b-4846-a246-11f9a413c6a0', NULL, FALSE, TRUE, NULL, 2, 'bcdf6d22-2823-478c-b44f-5fecb733b022', 1, 1, 'RJ_LOCALIZADOR', 'RJ_TRANSACAO', NULL, NULL),
('f74146f1-9c82-4581-b2e8-ef75de6b7e14', '{"BaseUrl":"https://prod-andorinha-gateway-smartbus.oreons.com/J3/clickbus","UrlAuth":"http://prod-andorinha-gateway-smartbus.oreons.com:58677/OAuth","UrlEtl":"https://app.snog.com.br/smartbus"}', '{"userName":"SNOG","password":"SN@cc90Pxd"}', 'Smartbus Plugin', 'e3f92adb-83c9-4772-9f49-a75a7913ccc5', '{"contrato":"MMS2021"}', FALSE, TRUE, NULL, 2, '09a5347d-2eeb-4a6a-a5b8-dea9e9525b16', 1, 1, 'SMARTBUS_LOCALIZADOR', 'SMARTBUS_TRANSACAO', NULL, NULL);

CREATE INDEX `IX_PaymentServices_UserCreatedId` ON `PaymentServices` (`UserCreatedId`);

CREATE INDEX `IX_PaymentServices_UserUpdatedId` ON `PaymentServices` (`UserUpdatedId`);

CREATE INDEX `IX_Plugin_UserCreatedId` ON `Plugin` (`UserCreatedId`);

CREATE INDEX `IX_Plugin_UserUpdatedId` ON `Plugin` (`UserUpdatedId`);

CREATE INDEX `IX_PluginConfiguration_PaymentServiceId` ON `PluginConfiguration` (`PaymentServiceId`);

CREATE INDEX `IX_PluginConfiguration_PluginId` ON `PluginConfiguration` (`PluginId`);

CREATE INDEX `IX_PluginConfiguration_ShoppingCartId` ON `PluginConfiguration` (`ShoppingCartId`);

CREATE INDEX `IX_PluginConfiguration_UserCreatedId` ON `PluginConfiguration` (`UserCreatedId`);

CREATE INDEX `IX_PluginConfiguration_UserUpdatedId` ON `PluginConfiguration` (`UserUpdatedId`);

CREATE INDEX `IX_ShoppingCartServices_UserCreatedId` ON `ShoppingCartServices` (`UserCreatedId`);

CREATE INDEX `IX_ShoppingCartServices_UserUpdatedId` ON `ShoppingCartServices` (`UserUpdatedId`);

CREATE INDEX `IX_UsersPlugins_PluginConfigurationsId` ON `UsersPlugins` (`PluginConfigurationsId`);

CREATE INDEX `IX_UsersPlugins_UserId` ON `UsersPlugins` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230726161445_init', '7.0.5');

COMMIT;

