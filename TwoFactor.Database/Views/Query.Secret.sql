CREATE VIEW [Query].[Secret]
AS
	SELECT
		 Secrets.[UserLoginName]
		,Secrets.[Secret]
		,Secrets.[ValidUntil]
		FROM
			[dbo].[Secret] Secrets
-- END of [Query].[Secret]