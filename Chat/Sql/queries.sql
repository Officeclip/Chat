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
	(end_user_id, custom_value, start_time, is_active)
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
---------------------------