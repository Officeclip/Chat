IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'GetUsersSP')
BEGIN
	PRINT 'Dropping Procedure GetUsersSP'
	DROP  Procedure  GetUsersSP
END

GO

PRINT 'Creating Procedure GetUsersSP'
GO
--
-- Gets the user-id given the email address
--
Create Procedure GetUsersSP
(
	@emailAddress nvarchar(255),
	@isExternal bit,
	@userId int output	-- returns -1 if no users found
)
AS
	SET @userId = (SELECT DISTINCT user_id
	FROM users
	WHERE  email_address = @emailAddress
	AND is_external = @isExternal
	AND active = 'Y')

GO
GRANT EXEC ON GetUsersSP TO PUBLIC
GO
---------------------------