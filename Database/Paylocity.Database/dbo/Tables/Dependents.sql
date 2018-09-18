CREATE TABLE [dbo].[Dependents]
(
	[DependentId]	UNIQUEIDENTIFIER CONSTRAINT [DF_Dependents_DependentId] DEFAULT (NEWSEQUENTIALID()) ROWGUIDCOL NOT NULL,
	[EmployeeId]	UNIQUEIDENTIFIER NOT NULL,
	[FirstName]		NVARCHAR (256) NOT NULL,
	[LastName]		NVARCHAR (256) NOT NULL,
	[CreatedDate]	DATETIME2 (7) CONSTRAINT [DF_Dependents_CreatedDate] DEFAULT (GETUTCDATE()) NOT NULL, 
	[ModifiedDate]	DATETIME2 (7) NOT NULL, 

	CONSTRAINT [PK_Dependents] PRIMARY KEY ([DependentId]),
	CONSTRAINT [FK_Dependents_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([EmployeeId])
);
