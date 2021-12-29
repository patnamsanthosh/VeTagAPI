USE [vetag]
GO

/****** Object:  Table [dbo].[customer]    Script Date: 29-12-2021 09:48:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[customer](
	[CustomerId] [varchar](50) NOT NULL,
	[FullName] [varchar](150) NULL,
	[EmailId] [varchar](150) NULL,
	[MobileNumber] [nvarchar](50) NULL,
	[VehicleNumber] [nvarchar](50) NULL,
	[IsRegisterByCustomer] [bit] NULL,
	[EmergencyContactNumber] [nvarchar](50) NULL,
	[AllowCalls] [bit] NULL,
	[ReferenceID] [varchar](50) NULL,
	[ContactOptions] [varchar](max) NULL,
	[Created_Date] [datetime] NOT NULL,
	[Created_By] [varchar](50) NULL,
	[Modified_Date] [datetime] NOT NULL,
	[Modified_By] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[customer] ADD  DEFAULT ((0)) FOR [IsRegisterByCustomer]
GO

ALTER TABLE [dbo].[customer] ADD  DEFAULT ((0)) FOR [AllowCalls]
GO

ALTER TABLE [dbo].[customer] ADD  DEFAULT (getdate()) FOR [Created_Date]
GO

ALTER TABLE [dbo].[customer] ADD  DEFAULT (getdate()) FOR [Modified_Date]
GO


