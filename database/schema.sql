-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

BEGIN;

CREATE DATABASE GolfProDB;
GO

USE GolfProDB;
GO

CREATE TABLE [dbo].[users](
	[id] [int] NOT NULL,
	[firstName] [varchar](64) NOT NULL,
	[lastName] [varchar](64) NOT NULL,
	[userName] [varchar](64) NOT NULL,
	[password] [varchar](max) NOT NULL,
	[isAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [userName] UNIQUE NONCLUSTERED 
(
	[userName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_isAdmin]  DEFAULT ((0)) FOR [isAdmin]
GO

CREATE TABLE [dbo].[courses](
	[id] [int] NOT NULL,
	[name] [varchar](128) NOT NULL,
	[par] [int] NOT NULL,
	[holeCount] [int] NOT NULL,
	[totalLengthYards] [int] NOT NULL,
 CONSTRAINT [PK_courses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[leagues](
	[id] [int] NOT NULL,
	[name] [varchar](64) NOT NULL,
	[adminId] [int] NOT NULL,
	[courseId] [int] NOT NULL,
 CONSTRAINT [PK_leagues] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[leagues]  WITH CHECK ADD  CONSTRAINT [FK_courses_id] FOREIGN KEY([courseId])
REFERENCES [dbo].[courses] ([id])
GO

ALTER TABLE [dbo].[leagues] CHECK CONSTRAINT [FK_courses_id]
GO

ALTER TABLE [dbo].[leagues]  WITH CHECK ADD  CONSTRAINT [FK_users_id] FOREIGN KEY([adminId])
REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[leagues] CHECK CONSTRAINT [FK_users_id]
GO

CREATE TABLE [dbo].[matches](
	[id] [int] NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_matches] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[users_leagues](
	[userId] [int] NOT NULL,
	[leagueId] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[users_leagues]  WITH CHECK ADD  CONSTRAINT [FK_league_user_id] FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[users_leagues] CHECK CONSTRAINT [FK_league_user_id]
GO

ALTER TABLE [dbo].[users_leagues]  WITH CHECK ADD  CONSTRAINT [FK_user_league_id] FOREIGN KEY([leagueId])
REFERENCES [dbo].[leagues] ([id])
GO

ALTER TABLE [dbo].[users_leagues] CHECK CONSTRAINT [FK_user_league_id]
GO

CREATE TABLE [dbo].[leagues_matches](
	[leagueId] [int] NOT NULL,
	[matchId] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[leagues_matches]  WITH CHECK ADD  CONSTRAINT [FK_leagues_matches_id] FOREIGN KEY([leagueId])
REFERENCES [dbo].[matches] ([id])
GO

ALTER TABLE [dbo].[leagues_matches] CHECK CONSTRAINT [FK_leagues_matches_id]
GO

ALTER TABLE [dbo].[leagues_matches]  WITH CHECK ADD  CONSTRAINT [FK_matches_leagues_id] FOREIGN KEY([leagueId])
REFERENCES [dbo].[leagues] ([id])
GO

ALTER TABLE [dbo].[leagues_matches] CHECK CONSTRAINT [FK_matches_leagues_id]
GO

CREATE TABLE [dbo].[users_matches](
	[userId] [int] NOT NULL,
	[matcheId] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[users_matches]  WITH CHECK ADD  CONSTRAINT [FK_matches_users_id] FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[users_matches] CHECK CONSTRAINT [FK_matches_users_id]
GO

ALTER TABLE [dbo].[users_matches]  WITH CHECK ADD  CONSTRAINT [FK_users_matches_id] FOREIGN KEY([matcheId])
REFERENCES [dbo].[matches] ([id])
GO

ALTER TABLE [dbo].[users_matches] CHECK CONSTRAINT [FK_users_matches_id]
GO

CREATE TABLE [dbo].[leagueInvitations](
	[id] [int] NOT NULL,
	[code] [varchar](max) NOT NULL,
	[leagueId] [int] NOT NULL,
 CONSTRAINT [PK_leagueInvitations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[leagueInvitations]  WITH CHECK ADD  CONSTRAINT [FK_leagues_id] FOREIGN KEY([leagueId])
REFERENCES [dbo].[leagues] ([id])
GO

ALTER TABLE [dbo].[leagueInvitations] CHECK CONSTRAINT [FK_leagues_id]
GO

COMMIT;