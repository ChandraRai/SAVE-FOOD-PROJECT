

CREATE TABLE [dbo].[USERS] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Username]  NVARCHAR (50)  NOT NULL,
    [Password]  NVARCHAR (MAX) NOT NULL,
    [Email]     NVARCHAR (50)  NOT NULL,
    [Phone]     NVARCHAR (50)  NOT NULL,
    [FirstName] NVARCHAR (50)  NULL,
    [LastName]  NVARCHAR (50)  NULL,
    [Privilege] SMALLINT       DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



CREATE TABLE [dbo].[FoodItems] (
    [FId]           INT           IDENTITY (1, 1) NOT NULL,
    [FoodName]      VARCHAR (MAX) NOT NULL,
    [FoodDesc]      VARCHAR (MAX) NOT NULL,
    [Status]        SMALLINT      DEFAULT ((0)) NOT NULL,
    [FoodCondition] SMALLINT      NOT NULL,
    [Expiry]        DATETIME      NOT NULL,
    [Id]            INT           NOT NULL,
    [PostingDate]   DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([FId] ASC),
    CONSTRAINT [FK_FoodItems] FOREIGN KEY ([Id]) REFERENCES [dbo].[USERS] ([Id])
);




CREATE TABLE [dbo].[Orders] (
    [OId]      INT      IDENTITY (1, 1) NOT NULL,
    [FId]      INT      NOT NULL,
    [UId]      INT      NOT NULL,
    [PickedUp] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([OId] ASC),
    CONSTRAINT [FK_DonatedFood] FOREIGN KEY ([FId]) REFERENCES [dbo].[FoodItems] ([FId]),
    CONSTRAINT [FK_User] FOREIGN KEY ([UId]) REFERENCES [dbo].[USERS] ([Id])
);



CREATE TABLE [dbo].[Comments]
(
    [CId] INT IDENTITY(1,1) NOT NULL,
    CONSTRAINT FK_FoodComments FOREIGN KEY (DonorId)     
    REFERENCES Users(Id),	 
    [DonorId] INT NOT NULL,
	CONSTRAINT FK_UserComments FOREIGN KEY (UId)     
    REFERENCES Users(Id),
	[UId] INT NOT NULL,
	[Title] VARCHAR(MAX) NOT NULL,
    [Comment] VARCHAR(MAX) NOT NULL, 
    [Date] DATETIME NOT NULL, 
    PRIMARY KEY CLUSTERED ([CId] ASC)
);





CREATE TABLE [dbo].[Posts]
(
    [PId] INT IDENTITY(1,1) NOT NULL,
    CONSTRAINT FK_Posts FOREIGN KEY (UId)     
    REFERENCES Users(Id), 
	[UId] INT NOT NULL,
    [Title] VARCHAR(MAX) NULL,	
    [Post] VARCHAR(MAX) NOT NULL, 
    [Date] DATETIME NOT NULL, 
	[PostType] SMALLINT DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([PId] ASC)
);







