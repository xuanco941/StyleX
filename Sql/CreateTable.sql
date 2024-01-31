IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Account] (
    [AccountID] int NOT NULL IDENTITY,
    [FullName] nvarchar(50) NULL,
    [Email] nvarchar(50) NOT NULL,
    [Password] nvarchar(50) NOT NULL,
    [PhoneNumber] nvarchar(20) NULL,
    [Address] nvarchar(300) NULL,
    [isActive] bit NOT NULL,
    [keyActive] nvarchar(max) NULL,
    [Avatar] nvarchar(max) NULL,
    [NumberPlayGame] int NULL,
    [Role] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY ([AccountID])
);
GO

CREATE TABLE [Category] (
    [CategoryID] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Image] nvarchar(max) NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryID])
);
GO

CREATE TABLE [Material] (
    [MaterialID] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Url] nvarchar(max) NOT NULL,
    [AoMap] nvarchar(max) NOT NULL,
    [NormalMap] nvarchar(max) NOT NULL,
    [RoughnessMap] nvarchar(max) NOT NULL,
    [MetalnessMap] nvarchar(max) NOT NULL,
    [Status] bit NOT NULL,
    [IsDecal] bit NOT NULL,
    CONSTRAINT [PK_Material] PRIMARY KEY ([MaterialID])
);
GO

CREATE TABLE [Order] (
    [OrderID] int NOT NULL IDENTITY,
    [TransportFee] float NOT NULL,
    [BasePrice] float NOT NULL,
    [NetPrice] float NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Message] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [PercentSale] float NOT NULL,
    [CreateAt] datetime2 NOT NULL,
    [UpdateAt] datetime2 NOT NULL,
    [AccountID] int NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([OrderID]),
    CONSTRAINT [FK_Order_Account_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Account] ([AccountID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Product] (
    [ProductID] int NOT NULL IDENTITY,
    [ModelUrl] nvarchar(max) NOT NULL,
    [PosterUrl] nvarchar(max) NOT NULL,
    [PosterDesignUrl1] nvarchar(max) NULL,
    [PosterDesignUrl2] nvarchar(max) NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Price] float NOT NULL,
    [Sale] float NOT NULL,
    [SaleEndAt] datetime2 NULL,
    [Status] bit NOT NULL,
    [CreateAt] datetime2 NOT NULL,
    [CategoryID] int NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductID]),
    CONSTRAINT [FK_Product_Category_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [Category] ([CategoryID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Promotion] (
    [PromotionID] int NOT NULL IDENTITY,
    [Number] int NOT NULL,
    [Status] bit NOT NULL,
    [ResultSpin] nvarchar(max) NOT NULL,
    [CreateAt] datetime2 NOT NULL,
    [UsedAt] datetime2 NULL,
    [ExpiredAt] datetime2 NULL,
    [OrderID] int NULL,
    [AccountID] int NOT NULL,
    CONSTRAINT [PK_Promotion] PRIMARY KEY ([PromotionID]),
    CONSTRAINT [FK_Promotion_Account_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Account] ([AccountID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Promotion_Order_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [Order] ([OrderID])
);
GO

CREATE TABLE [CartItem] (
    [CartItemID] int NOT NULL IDENTITY,
    [Amount] int NOT NULL,
    [PosterUrl] nvarchar(max) NOT NULL,
    [Size] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [Sale] float NOT NULL,
    [Price] float NOT NULL,
    [OrderID] int NULL,
    [ProductID] int NOT NULL,
    [AccountID] int NOT NULL,
    CONSTRAINT [PK_CartItem] PRIMARY KEY ([CartItemID]),
    CONSTRAINT [FK_CartItem_Account_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Account] ([AccountID]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartItem_Order_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [Order] ([OrderID]),
    CONSTRAINT [FK_CartItem_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductSetting] (
    [ProductSettingID] int NOT NULL IDENTITY,
    [ProductPartNameDefault] nvarchar(max) NOT NULL,
    [ProductPartNameCustom] nvarchar(max) NOT NULL,
    [IsDefault] bit NOT NULL,
    [NameMaterialDefault] nvarchar(max) NOT NULL,
    [AoMap] nvarchar(max) NOT NULL,
    [NormalMap] nvarchar(max) NOT NULL,
    [RoughnessMap] nvarchar(max) NOT NULL,
    [MetalnessMap] nvarchar(max) NOT NULL,
    [ProductID] int NOT NULL,
    CONSTRAINT [PK_ProductSetting] PRIMARY KEY ([ProductSettingID]),
    CONSTRAINT [FK_ProductSetting_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Warehouse] (
    [WarehouseID] int NOT NULL IDENTITY,
    [Amount] int NOT NULL,
    [Size] nvarchar(max) NOT NULL,
    [ProductID] int NOT NULL,
    CONSTRAINT [PK_Warehouse] PRIMARY KEY ([WarehouseID]),
    CONSTRAINT [FK_Warehouse_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE CASCADE
);
GO

CREATE TABLE [DecalInfo] (
    [DecalInfoID] int NOT NULL IDENTITY,
    [Image] nvarchar(max) NOT NULL,
    [MeshUuid] nvarchar(max) NOT NULL,
    [PositionX] float NOT NULL,
    [PositionY] float NOT NULL,
    [PositionZ] float NOT NULL,
    [OrientationX] float NOT NULL,
    [OrientationY] float NOT NULL,
    [OrientationZ] float NOT NULL,
    [RenderOrder] int NOT NULL,
    [Size] float NOT NULL,
    [CartItemID] int NOT NULL,
    CONSTRAINT [PK_DecalInfo] PRIMARY KEY ([DecalInfoID]),
    CONSTRAINT [FK_DecalInfo_CartItem_CartItemID] FOREIGN KEY ([CartItemID]) REFERENCES [CartItem] ([CartItemID]) ON DELETE CASCADE
);
GO

CREATE TABLE [DesignInfo] (
    [DesignInfoID] int NOT NULL IDENTITY,
    [DesignName] nvarchar(max) NOT NULL,
    [Color] nvarchar(max) NOT NULL,
    [ImageTexture] nvarchar(max) NULL,
    [TextureScale] float NULL,
    [NameMaterial] nvarchar(max) NOT NULL,
    [AoMap] nvarchar(max) NULL,
    [NormalMap] nvarchar(max) NULL,
    [RoughnessMap] nvarchar(max) NULL,
    [MetalnessMap] nvarchar(max) NULL,
    [CartItemID] int NOT NULL,
    CONSTRAINT [PK_DesignInfo] PRIMARY KEY ([DesignInfoID]),
    CONSTRAINT [FK_DesignInfo_CartItem_CartItemID] FOREIGN KEY ([CartItemID]) REFERENCES [CartItem] ([CartItemID]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductSettingMaterial] (
    [ProductSettingMaterialID] int NOT NULL IDENTITY,
    [ProductSettingID] int NOT NULL,
    [MaterialID] int NOT NULL,
    CONSTRAINT [PK_ProductSettingMaterial] PRIMARY KEY ([ProductSettingMaterialID]),
    CONSTRAINT [FK_ProductSettingMaterial_Material_MaterialID] FOREIGN KEY ([MaterialID]) REFERENCES [Material] ([MaterialID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductSettingMaterial_ProductSetting_ProductSettingID] FOREIGN KEY ([ProductSettingID]) REFERENCES [ProductSetting] ([ProductSettingID]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Account_Email] ON [Account] ([Email]);
GO

CREATE INDEX [IX_CartItem_AccountID] ON [CartItem] ([AccountID]);
GO

CREATE INDEX [IX_CartItem_OrderID] ON [CartItem] ([OrderID]);
GO

CREATE INDEX [IX_CartItem_ProductID] ON [CartItem] ([ProductID]);
GO

CREATE INDEX [IX_DecalInfo_CartItemID] ON [DecalInfo] ([CartItemID]);
GO

CREATE INDEX [IX_DesignInfo_CartItemID] ON [DesignInfo] ([CartItemID]);
GO

CREATE INDEX [IX_Order_AccountID] ON [Order] ([AccountID]);
GO

CREATE INDEX [IX_Product_CategoryID] ON [Product] ([CategoryID]);
GO

CREATE INDEX [IX_ProductSetting_ProductID] ON [ProductSetting] ([ProductID]);
GO

CREATE INDEX [IX_ProductSettingMaterial_MaterialID] ON [ProductSettingMaterial] ([MaterialID]);
GO

CREATE INDEX [IX_ProductSettingMaterial_ProductSettingID] ON [ProductSettingMaterial] ([ProductSettingID]);
GO

CREATE INDEX [IX_Promotion_AccountID] ON [Promotion] ([AccountID]);
GO

CREATE INDEX [IX_Promotion_OrderID] ON [Promotion] ([OrderID]);
GO

CREATE INDEX [IX_Warehouse_ProductID] ON [Warehouse] ([ProductID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240121111204_v1', N'6.0.25');
GO

COMMIT;
GO

INSERT INTO [Account] ([FullName], [Email], [Password], [PhoneNumber], [Address], [isActive], [keyActive], [Avatar], [NumberPlayGame], [Role])
VALUES 
    ('Admin', 'admin', '12345', '0388530006', '', 1, 'activationkey456', '2.jpg', 0, 'admin'),
    (N'Người dùng 1', 'user', '12345', '0388530006', N'175 Tây Sơn, Đống Đa, Hà Nội', 1, 'activationkey123', '2.jpg', 9999, 'user');

