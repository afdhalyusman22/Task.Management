insert into enum.gender(name)
select 'Male';
insert into enum.gender(name)
select 'Female';

INSERT INTO public.user_account(
	user_id, email, full_name, password, phone_number, gender_id, last_login, register_at)
	VALUES ('7608c3d6-1267-4119-af33-5344ba8c8e9c', 'admin@mail.com', 'admin', '$2a$12$jRbAEGbryseurlJ2AqrWReJ0sBB7MZGF0tMjNWguDt/T3vjgIhY2C','085111111111',1, now(), now());