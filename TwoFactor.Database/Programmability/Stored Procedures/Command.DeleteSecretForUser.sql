CREATE PROCEDURE [Command].[DeleteSecretForUser]
	@UserLoginName NVARCHAR(15) = NULL
AS
BEGIN
	SET @UserLoginName = LTRIM(RTRIM(@UserLoginName))

	IF(@UserLoginName IS NULL OR @UserLoginName = '')
		RETURN;

	BEGIN
		DELETE
			Secrets
			FROM
				[dbo].[Secret] Secrets
			WHERE
				Secrets.[UserLoginName] = @UserLoginName
	END
END