delete from chatmessages
delete from chatsessions
delete from endusers
delete from users

INSERT INTO users
(name, email, password)
VALUES
('agent', 'agent1@officeclip.com', 'password')
GO
