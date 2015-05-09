CREATE TABLE [dbo].[Secret]
(
	[UserLoginName] NVARCHAR(15) NOT NULL, 
    [Secret] VARBINARY(16) NOT NULL, 
    [ValidUntil] DATETIME NOT NULL,

	CONSTRAINT [PK_Secret] PRIMARY KEY([UserLoginName])
)
