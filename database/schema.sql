-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

CREATE DATABASE [GolfProDB]
GO

USE [GolfProDB]
GO

BEGIN TRANSACTION;

CREATE TABLE [users](
	[id] [int] IDENTITY NOT NULL,
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
)
 ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [users] ADD  CONSTRAINT [DF_users_isAdmin]  DEFAULT ((0)) FOR [isAdmin]


CREATE TABLE [courses](
	[id] [int] IDENTITY NOT NULL,
	[name] [varchar](128) NOT NULL,
	[par] [int] NOT NULL,
	[holeCount] [int] NOT NULL,
	[totalLengthYards] [int] NOT NULL,
 CONSTRAINT [PK_courses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [leagues](
	[id] [int] IDENTITY NOT NULL,
	[name] [varchar](64) NOT NULL,
	[adminId] [int] NOT NULL,
	[courseId] [int] NOT NULL,
 CONSTRAINT [PK_leagues] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [leagues]  WITH CHECK ADD  CONSTRAINT [FK_courses_id] FOREIGN KEY([courseId])
REFERENCES [courses] ([id])


ALTER TABLE [leagues] CHECK CONSTRAINT [FK_courses_id]


ALTER TABLE [leagues]  WITH CHECK ADD  CONSTRAINT [FK_users_id] FOREIGN KEY([adminId])
REFERENCES [users] ([id])


ALTER TABLE [leagues] CHECK CONSTRAINT [FK_users_id]


CREATE TABLE [matches](
	[id] [int] IDENTITY NOT NULL,
	[date] [datetime] NOT NULL,
	[playerCount] [int] NOT NULL
 CONSTRAINT [PK_matches] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [users_leagues](
	[userId] [int] NOT NULL,
	[leagueId] [int] NOT NULL
) ON [PRIMARY]


ALTER TABLE [users_leagues]  WITH CHECK ADD  CONSTRAINT [FK_league_user_id] FOREIGN KEY([userId])
REFERENCES [users] ([id])


ALTER TABLE [users_leagues] CHECK CONSTRAINT [FK_league_user_id]


ALTER TABLE [users_leagues]  WITH CHECK ADD  CONSTRAINT [FK_user_league_id] FOREIGN KEY([leagueId])
REFERENCES [leagues] ([id])


ALTER TABLE [users_leagues] CHECK CONSTRAINT [FK_user_league_id]


CREATE TABLE [leagues_matches](
	[leagueId] [int] NOT NULL,
	[matchId] [int] NOT NULL
) ON [PRIMARY]


ALTER TABLE [leagues_matches]  WITH CHECK ADD  CONSTRAINT [FK_leagues_matches_id] FOREIGN KEY([leagueId])
REFERENCES [matches] ([id])


ALTER TABLE [leagues_matches] CHECK CONSTRAINT [FK_leagues_matches_id]


ALTER TABLE [leagues_matches]  WITH CHECK ADD  CONSTRAINT [FK_matches_leagues_id] FOREIGN KEY([leagueId])
REFERENCES [leagues] ([id])


ALTER TABLE [leagues_matches] CHECK CONSTRAINT [FK_matches_leagues_id]


CREATE TABLE [users_matches](
	[userId] [int] NOT NULL,
	[matcheId] [int] NOT NULL,
	[score] [int] NOT NULL
) ON [PRIMARY]


ALTER TABLE [users_matches]  WITH CHECK ADD  CONSTRAINT [FK_matches_users_id] FOREIGN KEY([userId])
REFERENCES [users] ([id])


ALTER TABLE [users_matches] CHECK CONSTRAINT [FK_matches_users_id]


ALTER TABLE [users_matches]  WITH CHECK ADD  CONSTRAINT [FK_users_matches_id] FOREIGN KEY([matcheId])
REFERENCES [matches] ([id])


ALTER TABLE [users_matches] CHECK CONSTRAINT [FK_users_matches_id]


CREATE TABLE [leagueInvitations](
	[id] [int] IDENTITY NOT NULL,
	[code] [varchar](max) NOT NULL,
	[leagueId] [int] NOT NULL,
 CONSTRAINT [PK_leagueInvitations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [leagueInvitations]  WITH CHECK ADD  CONSTRAINT [FK_leagues_id] FOREIGN KEY([leagueId])
REFERENCES [leagues] ([id])


ALTER TABLE [leagueInvitations] CHECK CONSTRAINT [FK_leagues_id]


COMMIT TRANSACTION;