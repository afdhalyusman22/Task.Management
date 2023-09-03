insert into enum.task_type(name)
select 'Meeting';
insert into enum.task_type(name)
select 'Daily Task';
insert into enum.task_type(name)
select 'Sharing';
insert into enum.task_type(name)
select 'Research';

insert into enum.task_status(name)
select 'Created';
insert into enum.task_status(name)
select 'In Progress';
insert into enum.task_status(name)
select 'Reschedule';
insert into enum.task_status(name)
select 'Cancel';
insert into enum.task_status(name)
select 'Finished';