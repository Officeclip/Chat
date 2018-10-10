IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'InsSessionChatSP')
BEGIN
	PRINT 'Dropping Procedure InsSessionChatSP'
	DROP  Procedure  InsSessionChatSP
END

GO

PRINT 'Creating Procedure InsSessionChatSP'
GO

Create Procedure InsSessionChatSP
(
	@userName nvarchar(255),
	@emailAddress nvarchar(255),
	@customValue nvarchar(255),
	@connectionId varchar(255),
	@chatSessionId int output
)
AS
	DECLARE @endUserId int
	SET @endUserId = (SELECT end_user_id from endusers where email = @emailAddress)
	IF (@endUserId IS NULL)
	BEGIN
		INSERT INTO endusers
		(name, email, connection_id)
		VALUES
		(@userName, @emailAddress, @connectionId)
		SET @endUserId = (SELECT scope_identity())
	END
	INSERT INTO chatsessions
	(end_user_id, custom_value, start_time, mode)
	VALUES
	(@endUserId, @customValue, GETDATE(), 1)
	SET @chatSessionId = (SELECT scope_identity())
GO
GRANT EXEC ON InsSessionChatSP TO PUBLIC
GO
--------------------------------------
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'GetUsersChatSP')
BEGIN
	PRINT 'Dropping Procedure GetUsersChatSP'
	DROP  Procedure  GetUsersChatSP
END

GO

PRINT 'Creating Procedure GetUsersChatSP'
GO

Create Procedure GetUsersSP
AS
	SELECT * from users
GO

GRANT EXEC ON GetUsersChatSP TO PUBLIC
GO
--------------------------------------------
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'GetEndUsersChatSP')
BEGIN
	PRINT 'Dropping Procedure GetEndUsersChatSP'
	DROP  Procedure  GetEndUsersChatSP
END

GO

PRINT 'Creating Procedure GetEndUsersChatSP'
GO

Create Procedure GetEndUsersChatSP
AS
	SELECT * from endusers
GO

GRANT EXEC ON GetEndUsersChatSP TO PUBLIC
GO
---------------------------
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'GetActiveSessonsChatSP')
BEGIN
	PRINT 'Dropping Procedure GetActiveSessonsChatSP'
	DROP  Procedure  GetActiveSessonsChatSP
END

GO

PRINT 'Creating Procedure GetActiveSessonsChatSP'
GO

Create Procedure GetActiveSessonsChatSP
(
	@isActive bit
)
AS
	SELECT 
		chat_session_id as chatSessionId, 
		endusers.name as name, 
		endusers.email as email, 
		start_time as startTime, 
		end_time as endTime, 
		mode as endUserMode
	FROM 
		chatsessions 
	INNER JOIN 
		endusers 
	ON
		chatsessions.end_user_id = endusers.end_user_id
	WHERE 
		(@isActive = 0)
		AND (mode = 3)
		OR 
			(@isActive = 1)
			AND (
				(mode = 1) 
				OR (mode = 2))
GO

GRANT EXEC ON GetActiveSessonsChatSP TO PUBLIC
GO
---------------------------
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'InsUserConnectionChatSP')
BEGIN
	PRINT 'Dropping Procedure InsUserConnectionChatSP'
	DROP  Procedure  InsUserConnectionChatSP
END

GO

PRINT 'Creating Procedure InsUserConnectionChatSP'
GO

Create Procedure InsUserConnectionChatSP
(
	@userId int,
	@connectionId varchat(25)
)
AS
UPDATE endusers
SET
	connection_id = @connectionId
WHERE
	end_user_id = @userId
GO

GRANT EXEC ON InsUserConnectionChatSP TO PUBLIC
GO
---------------------------