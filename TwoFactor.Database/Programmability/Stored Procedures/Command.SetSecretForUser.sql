CREATE PROCEDURE [Command].[SetSecretForUser]
	@UserLoginName NVARCHAR(15) = NULL,
	@Secret VARBINARY(16) = NULL,
	@ValidUntil DATETIME = NULL
AS
BEGIN
	SET @UserLoginName = LTRIM(RTRIM(@UserLoginName))

	-- If we don't receive valid input, quit right away
	IF(@UserLoginName IS NULL
		OR @UserLoginName = ''
		OR @Secret IS NULL
		OR @ValidUntil IS NULL)
		RETURN;

	BEGIN
		-- First, we assume that there is already a secret to update
		--   and that the new secret has a later date it is valid until
		UPDATE
			Secrets
			SET
				 Secrets.[Secret] = @Secret
				,Secrets.[ValidUntil] = @ValidUntil
				FROM
					[dbo].[Secret] Secrets
				WHERE
					Secrets.[UserLoginName] = @UserLoginName
					AND
					Secrets.[ValidUntil] < @ValidUntil

		-- If there was no existing secret to update, then insert a new one
		IF(@@ROWCOUNT = 0
			AND NOT EXISTS
			(
				SELECT 1 
				FROM [dbo].[Secret] Secrets
				WHERE Secrets.[UserLoginName] = @UserLoginName
			)
		)
		BEGIN
			INSERT
				INTO [dbo].[Secret] 
					([UserLoginName], [Secret], [ValidUntil])
				VALUES
					(@UserLoginName, @Secret, @ValidUntil)
		END
	END
END