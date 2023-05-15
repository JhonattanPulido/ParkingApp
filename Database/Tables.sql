CREATE TABLE	[dbo].[VehicleTypes]	(
	 [Id]	TINYINT	IDENTITY	NOT NULL
	,[Name]	VARCHAR	(32)		NOT NULL
	,[Cost]	FLOAT				NOT NULL
	,CONSTRAINT	PK_VehicleTypes_Id		PRIMARY KEY	([Id])
	,CONSTRAINT	UK_VehicleTypes_Name	UNIQUE		([Name])
);

INSERT INTO	[dbo].[VehicleTypes]
 ([Name]		,[Cost])
VALUES
 ('Bicycle'		,10)
,('Motorcycle'	,50)
,('Car'			,110)
;

CREATE TABLE	[dbo].[Vehicles] (
	 [NumberPlate]	VARCHAR	(6)			NOT NULL
	,[TypeId]		TINYINT				NOT NULL
	,CONSTRAINT	PK_Vehicles_NumberPlate				PRIMARY KEY	([NumberPlate])
	,CONSTRAINT	FK_VehicleTypes_Id_Vehicles_TypeId	FOREIGN KEY	([TypeId])	REFERENCES	[dbo].[VehicleTypes]	([Id])
);

CREATE TABLE	[dbo].[Logs] (
	 [Id]					UNIQUEIDENTIFIER	DEFAULT	NEWID()
	,[Entry]				DATETIME			NOT NULL
	,[Departure]			DATETIME			NULL
	,[Price]				FLOAT				NULL
	,[Time]					VARCHAR	(32)		NULL
	,[BillDiscountNumber]	VARCHAR	(8)			NULL
	,[VehicleId]			VARCHAR	(6)			NOT NULL
	,CONSTRAINT	PK_Logs_Id						PRIMARY KEY	([Id])
	,CONSTRAINT	FK_Vehicles_Id_Logs_VehicleId	FOREIGN KEY	([VehicleId])	REFERENCES	[dbo].[Vehicles]	([NumberPlate])
);