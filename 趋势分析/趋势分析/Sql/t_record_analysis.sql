USE [db_record]
GO

/****** Object:  Table [dbo].[t_record_analysis]    Script Date: 05/13/2014 19:49:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_record_analysis](
	[times] [varchar](7) NOT NULL,
	[what_number] [tinyint] NULL,
	[multiple] [smallint] NULL,
	[num_money] [varchar](500) NULL,
	[outlay] [numeric](4, 2) NULL,
	[expect_income] [varchar](50) NULL,
	[num] [smallint] NULL,
	[relity_income] [numeric](10, 10) NULL,
	[balance] [numeric](10, 10) NULL,
	[writer] [nvarchar](20) NULL,
	[write_date] [smalldatetime] NOT NULL,
	[modify_date] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_t_record_analysis] PRIMARY KEY CLUSTERED 
(
	[times] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

