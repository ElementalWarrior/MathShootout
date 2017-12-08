select * from logs
where session_id not in ('75361551-2d69-4253-9e2f-d8bd21b1fdab')
order by date_created 



select session_id
, max(player_name)
, min(date_created) as first_record
, max(date_created) as last_record
, max(date_created) - min(date_created) as session_duration
, count(case when data='True' and tag='NetCollision' then 1 end) as correct_answers
, count(case when data='False' and tag='NetCollision' then 1 end) as incorrect_answers
, count(case when tag='PuckMiss' then 1 end) as net_misses
, count(*) as number_of_logs
from 
	logs 
group by
	session_id
order by first_record;

select * from logs where session_id = 'cdab1d79-0d54-436a-bfb9-fce5e26a4705'

delete from logs where session_id = 'cdab1d79-0d54-436a-bfb9-fce5e26a4705' and date_created < '2017-11-09 14:25:12'

--delete from logs where session_id in ('bb646890-2e08-4c16-b0c8-923144100e7f', '3e7a8167-68d1-4621-8939-9874b39e6c29' ,'')

--select * from logs where session_id = 'bb646890-2e08-4c16-b0c8-923144100e7f'