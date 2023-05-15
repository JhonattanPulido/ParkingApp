CREATE OR ALTER PROCEDURE	[dbo].[CreateLog]
	 @ENTRY				DATETIME
	,@NUMBER_PLATE		VARCHAR		(6)
	,@VEHICLE_TYPE_ID	TINYINT
AS
BEGIN
	SET NOCOUNT ON;

	IF (SELECT COUNT(*) FROM [dbo].[Vehicles] AS [ve] WHERE [ve].[NumberPlate] = @NUMBER_PLATE) = 0
	BEGIN
		INSERT INTO	[dbo].[Vehicles]
		([NumberPlate]	,[TypeId])
		VALUES
		(@NUMBER_PLATE	,@VEHICLE_TYPE_ID)
		;
	END;

	INSERT INTO	[dbo].[Logs]
	([Entry]	,[VehicleId])
	VALUES
	(@ENTRY		,@NUMBER_PLATE)
	;
END;

CREATE OR ALTER PROCEDURE	[dbo].[GetLog]
	@NUMBER_PLATE	VARCHAR	(6)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1
		 CAST([lo].[Id]	AS VARCHAR (36))	AS [Id]
		,[lo].[Entry]						AS [Entry]
		,[lo].[Departure]					AS [Departure]
		,[lo].[Price]						AS [Price]
		,[lo].[BillDiscountNumber]			AS [Discount]
		,(SELECT
			 [ve].[NumberPlate]	AS [NumberPlate]
			,JSON_QUERY((SELECT
				 [vt].[Id]		AS [Id]
				,[vt].[Name]	AS [Name]
				,[vt].[Cost]	AS [Cost]
			 FOR JSON PATH
			,WITHOUT_ARRAY_WRAPPER))		AS [Type]
		  FOR JSON PATH
		 ,WITHOUT_ARRAY_WRAPPER )	AS [VehicleJSON]
	FROM		[dbo].[Logs]			AS [lo]
	INNER JOIN	[dbo].[Vehicles]		AS [ve]	ON [lo].[VehicleId]	= [ve].[NumberPlate]
	INNER JOIN	[dbo].[VehicleTypes]	AS [vt]	ON [ve].[TypeId]	= [vt].[Id]
	WHERE	[ve].[NumberPlate]			= @NUMBER_PLATE
	AND		[lo].[Departure]			IS NULL
	AND		[lo].[Price]				IS NULL
	AND		[lo].[BillDiscountNumber]	IS NULL
	ORDER BY
		[lo].[Entry]	DESC
	;
END;

CREATE OR ALTER PROCEDURE	[dbo].[GetLogs]
	 @ENTRY			DATETIME
	,@DEPARTURE		DATETIME
	,@PAGE_INDEX	TINYINT
	,@ITEMS_COUNT	TINYINT
AS
BEGIN
	SET NOCOUNT ON;

	SET	@DEPARTURE	= DATEADD(DD, 1, @DEPARTURE)
	;

	DECLARE
		 @EntryDate		DATE	= CAST(@ENTRY AS DATE)
		,@DepartureDate	DATE	= CAST(@DEPARTURE AS DATE)
	;

	SELECT
		 @ENTRY		= CAST(@EntryDate AS DATETIME)
		,@DEPARTURE	= CAST(@DepartureDate AS DATETIME)
	;

	SELECT
		 CAST([lo].[Id] AS VARCHAR (36))	AS [Id]
		,[lo].[Entry]						AS [Entry]
		,[lo].[Departure]					AS [Departure]
		,[lo].[Price]						AS [Price]
		,[lo].[Time]						AS [Time]
		,[lo].[BillDiscountNumber]			AS [BillDiscountNumber]
		,(SELECT
			 [ve].[NumberPlate]	AS [NumberPlate]
			,JSON_QUERY((SELECT
				 [vt].[Id]		AS [Id]
				,[vt].[Name]	AS [Name]
			 FOR JSON PATH
			,WITHOUT_ARRAY_WRAPPER))	AS [Type]
		  FOR JSON PATH
		 ,WITHOUT_ARRAY_WRAPPER )			AS [VehicleJSON]
	FROM		[dbo].[Logs]			AS [lo]
	INNER JOIN	[dbo].[Vehicles]		AS [ve]	ON [lo].[VehicleId]	= [ve].[NumberPlate]
	INNER JOIN	[dbo].[VehicleTypes]	AS [vt]	ON [ve].[TypeId]	= [vt].[Id]
	WHERE	[lo].[Departure]	IS NOT NULL
	AND		[lo].[Entry]		>= @ENTRY
	AND		[lo].[Departure]	< @DEPARTURE
	ORDER BY
		[lo].[Entry]	ASC
	OFFSET		(@PAGE_INDEX - 1) * @ITEMS_COUNT	ROWS
	FETCH NEXT	@ITEMS_COUNT	ROWS ONLY
	;

	SELECT
		COUNT(*)	AS [TotalItems]
	FROM		[dbo].[Logs]			AS [lo]
	INNER JOIN	[dbo].[Vehicles]		AS [ve]	ON [lo].[VehicleId]	= [ve].[NumberPlate]
	INNER JOIN	[dbo].[VehicleTypes]	AS [vt]	ON [ve].[TypeId]	= [vt].[Id]
	WHERE	[lo].[Departure]	IS NOT NULL
	AND		[lo].[Entry]		>= @ENTRY
	AND		[lo].[Departure]	< @DEPARTURE
	;
END;

CREATE OR ALTER PROCEDURE	[dbo].[UpdateLog]
	 @NUMBER_PLATE			VARCHAR		(6)
	,@DEPARTURE				DATETIME
	,@PRICE					FLOAT
	,@TIME					VARCHAR		(36)
	,@BILL_DISCOUNT_NUMBER	VARCHAR		(8)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE
		@Id		UNIQUEIDENTIFIER
	;

	IF (SELECT COUNT(*) FROM [dbo].[Vehicles] AS [ve] WHERE [ve].[NumberPlate] = @NUMBER_PLATE) = 0
	BEGIN
		SELECT	'01'	AS [Status];
		RETURN;
	END;

	SELECT TOP 1
		@Id	= [lo].[Id]
	FROM	[dbo].[Logs]	AS [lo]
	WHERE	[lo].[VehicleId]			= @NUMBER_PLATE
	AND		[lo].[Departure]			IS NULL
	AND		[lo].[Price]				IS NULL
	AND		[lo].[BillDiscountNumber]	IS NULL
	;

	IF	@Id	IS NULL
	BEGIN
		SELECT	'02'	AS [Status];
		RETURN;
	END;

	UPDATE	[dbo].[Logs]
	SET
		 [Departure]			= @DEPARTURE
		,[Price]				= @PRICE
		,[Time]					= @TIME
		,[BillDiscountNumber]	= @BILL_DISCOUNT_NUMBER
	WHERE	[Id]	= @Id
	;

	SELECT	'00'	AS [Status];
END;