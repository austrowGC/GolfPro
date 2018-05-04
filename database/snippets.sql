use golfprodb

select * from users

select id from users where username = 'dude'

	--userLeaguesSql
select l.id
	, l.name'leagueName'
	, u.username'orgUsername'
	, u.firstname'orgFirstName'
	, u.lastname'orgLastName'
	, c.name'courseName'
from users_leagues ul
	inner join leagues l on (l.id = ul.leagueId)
	inner join users u on (u.id = ul.userId)
	inner join courses c on (l.courseId = c.id)
where ul.userId = @userId

	--userMatchesSql
select m.id
	, m.date
	, um.score
	, l.name'leagueName'
	, c.name'courseName'
	, c.par
	, c.holeCount
from users_matches as um
	inner join matches m on (m.id = um.matchId)
	inner join leagues_matches lm on (lm.matchId = um.matchId)
	inner join leagues l on (l.id = lm.leagueId)
	inner join courses c on (c.id = l.courseId)
where userId = @userId


Insert into leagues(name, organizerId, courseId)
values(@name, (select id from users where username = @username), @courseId)

select u.id
	, u.username
	, u.isadmin
	, count(l.id)'ownedL'
from users u
	left join leagues l on (u.id = l.organizerId)
where u.username = 'dude'
group by u.id, u.username, u.isadmin;

select u.id, u.username, u.isadmin, count(l.id)'ownedL' from users u inner join leagues l on (u.id = l.organizerId) where u.username = 'dude' group by u.id, u.username, u.isadmin;

insert into users_leagues (userId, leagueId)
values(@userId, @leagueId);

update users
set isadmin = 1
where username = 'dude'

select * from leagues

select * from users_leagues

alter table users_matches alter column score int null

alter table matches alter column playerCount int null

alter table leagues_matches drop constraint [FK_leagues_matches_id]

alter table leagues_matches ADD  CONSTRAINT [FK_leagues_matches_id] FOREIGN KEY([matchId])
REFERENCES [matches] ([id])

select * from matches

select um.matchid
	, m.date
	, u.id
	, u.username
	, u.firstname
	, u.lastname
from users_matches um
	inner join matches m on (um.matchid = m.id)
	inner join users u on (um.userid = u.id)
	inner join leagues_matches lm on (um.matchid = lm.matchid)
where (lm.leagueid = 5) and
	um.score is null and
	m.date < getdate()
order by m.date ASC


select * from leagues_matches

select * from users_matches

select um.matchid, m.date, u.id, u.username, u.firstname, u.lastname from users_matches um inner join matches m on (um.matchid = m.id) inner join users u on (um.userid = u.id) inner join leagues_matches lm on (um.matchid = lm.matchid) where (lm.leagueid = 5) and (um.score is null) and (m.date < getdate()) order by m.date ASC

update users_matches set (score = @score) where (userid = @userid) and (matchid = @matchid);

update users_matches set score = 52 where userid = 15 and matchid = 5;