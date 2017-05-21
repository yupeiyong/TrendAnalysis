USE [db_record]
GO

/****** Object:  Table [dbo].[t_hk_record]    Script Date: 04/02/2014 22:47:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_hk_record](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[record_date] [datetime] NOT NULL,
	[times] [char](7) NOT NULL,
	[first_num] [tinyint] NULL,
	[second_num] [tinyint] NULL,
	[third_num] [tinyint] NULL,
	[fourth_num] [tinyint] NULL,
	[fifth_num] [tinyint] NULL,
	[sixth_num] [tinyint] NULL,
	[seventh_num] [tinyint] NULL,
 CONSTRAINT [PK__t_hk_rec__3213E83F7F60ED59] PRIMARY KEY CLUSTERED 
(
	[record_date] ASC,
	[times] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


