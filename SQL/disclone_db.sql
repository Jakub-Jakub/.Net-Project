/*Check if database exists then drops it.*/

IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'disclone_db')
BEGIN 
	DROP DATABASE disclone_db
	print '' print '***droping database disclone_db ***'
END
GO

print'' print'*** creating disclone_db ***'
GO
CREATE DATABASE disclone_db
GO

print''print'***using disclone_db ***'
GO
USE disclone_db
GO

print''print'***create user table***'
GO
CREATE TABLE [dbo].[User_Account](
	[UserID]		[int]		IDENTITY(1000000, 1)	NOT NULL,
	[Email]			[nvarchar]	(100)					NOT NULL,
	[UserName]		[nvarchar]	(20)					NOT NULL,
	[PasswordHash]	[nvarchar]	(100)					NOT NULL 	DEFAULT 'b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342',
	[Active]		[bit]								NOT NULL	DEFAULT 1,
	[UserImage]		[varbinary](max)						NULL,
	[isAdmin]		[bit]								NOT NULL 	DEFAULT 0,
	CONSTRAINT [pk_userID] PRIMARY KEY ([UserID] ASC),
	CONSTRAINT [ak_email] UNIQUE([Email] ASC),
	CONSTRAINT [ak_userName] UNIQUE([UserName] ASC)
)
GO


print '' print '*** creating server table ***'
GO
CREATE TABLE [dbo].[Server](
	[ServerID]		[int]		IDENTITY(1000000, 1)		NOT NULL,
	[CreatedAt]		[DateTime]								NOT NULL DEFAULT GETDATE(),
	[Tag]			[nvarchar] 	(8)							NOT NULL,
	[UserID]		[int]									NOT NULL,
	[Name]			[nvarchar]	(100)						NOT NULL,
	[LastMessage]	[DateTime]								NOT NULL DEFAULT GETDATE(),
	[ServerImage]	[varbinary](max)						NULL,
	CONSTRAINT [pk_ServerID] PRIMARY KEY ([ServerID] ASC),
	CONSTRAINT [fk_server_userID] FOREIGN KEY([UserID]) REFERENCES [dbo].[User_Account]([UserID]),
	CONSTRAINT [ak_tag] UNIQUE([Tag] ASC),
	)
GO


print '' print '*** creating chatroom table ***'
GO
CREATE TABLE [dbo].[Chatroom](
	[ChatroomID]	[int]		IDENTITY(1000000, 1)		NOT NULL,
	[ServerID]		[int]									NOT NULL,
	[CreatedAt]		[DateTime]								NOT NULL DEFAULT GETDATE(),
	[Name]			[nvarchar]	(100)						NOT NULL,
	[LastMessage]	[DateTime]								NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [pk_ChatroomID] PRIMARY KEY ([ChatroomID] ASC),
	CONSTRAINT [fk_chatroom_serverID] FOREIGN KEY([ServerID]) REFERENCES [dbo].[Server]([ServerID]),
	)
GO

print '' print'*** creating message table ***'
GO
CREATE TABLE [dbo].[Message](
	[MessageID]		[int]		IDENTITY(1000000, 1)		NOT NULL,
	[UserID]		[int]									NOT NULL,
	[CreatedAt]		[DateTime]								NOT NULL DEFAULT GETDATE(),
	[Message]		[nvarchar]	(2000)						NOT NULL,
	[hasMedia]		[bit]									NOT NULL DEFAULT 0,
	[isVisible]		[bit]									NOT NULL DEFAULT 1,
	CONSTRAINT 	[pk_MessageID] PRIMARY KEY ([MessageID] ASC)
)

print '' print'*** creating chatroom_message table ***'
GO
CREATE TABLE [dbo].[Chatroom_Message_List](
	[ChatroomID]	[int]									NOT NULL,
	[MessageID]		[int]									NOT NULL,
	CONSTRAINT [fk_chatroom_message_list_chatroomID] FOREIGN KEY([ChatroomID]) REFERENCES [dbo].[Chatroom]([ChatroomID]),
	CONSTRAINT [fk_chatroom_message_list_messageID] FOREIGN KEY([MessageID]) REFERENCES [dbo].[Message]([MessageID]) 
)
GO

print '' print'*** creating server_user_list table ***'
GO
CREATE TABLE [dbo].[Server_User_List](
	[ServerID]		[int]									NOT NULL,
	[UserID]		[int]									NOT NULL,
	[isModerator]	[bit]									NOT NULL DEFAULT 0,
	CONSTRAINT [fk_Server_User_List_serverID] FOREIGN KEY([ServerID]) REFERENCES [dbo].[Server]([ServerID]),
	CONSTRAINT [fk_Chatroom_User_List_UserID] FOREIGN KEY([UserID]) REFERENCES [dbo].[User_Account]([UserID])
)
GO

print '' print'*** creating report table ***'
GO
CREATE TABLE [dbo].[Report_Message](
	[UserID]		[int]									NOT NULL,
	[Reason]		[nvarchar](250)							NOT NULL,
	[MessageID]		[int]									NOT NULL,
	[isActive]		[bit]									NOT NULL DEFAULT 1,
	CONSTRAINT [fk_Report_Message_MessageID] FOREIGN KEY([MessageID]) REFERENCES [dbo].[Message]([MessageID]),
	CONSTRAINT [fk_Report_Message_UserID] FOREIGN KEY([UserID]) REFERENCES [dbo].[User_Account]([UserID])
)
GO

print'' print'*** creating Report_Types table ***'
GO
CREATE TABLE [dbo].[Report_Types](
	[ReportTypeName]	[nvarchar](20)						NOT NULL,
	[Description]		[nvarchar](200)						NOT NULL,
	CONSTRAINT [pk_ReportTypeName] PRIMARY KEY ([ReportTypeName] ASC)
)

print '' print'*** creating direct_message_list'
GO
CREATE TABLE [dbo].[Direct_Message_List](
	[SenderUserID]		[int]								NOT NULL,
	[ReceiverUserID]	[int]								NOT NULL,
	[MessageID]			[int]								NOT NULL,
	CONSTRAINT [fk_Direct_Message_List_MessageID] FOREIGN KEY([MessageID]) REFERENCES [dbo].[Message]([MessageID]),
	CONSTRAINT [fk_Direct_Message_List_SenderUserID] FOREIGN KEY([SenderUserID]) REFERENCES [dbo].[User_Account]([UserID]),
	CONSTRAINT [fk_Direct_Message_List_ReceiverUserID] FOREIGN KEY([ReceiverUserID]) REFERENCES [dbo].[User_Account]([UserID])
)

print'' print '***STORED PROCEDURES***'
GO


print '' print'*** creating sp_insert_new_user_account ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_user_account]
	(
		@Email					[nvarchar]	(100),	
		@UserName				[nvarchar]	(50),		
		@PasswordHash			[nvarchar]	(100)
	)
AS
	BEGIN
		INSERT INTO User_Account
			(Email, UserName, PasswordHash)
		VALUES
			(@Email, @UserName, @PasswordHash)
		RETURN SCOPE_IDENTITY()
	END
GO

print'' print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
	@Email				[nvarchar](100),
	@PasswordHash		[nvarchar](100)	
	)
AS 
	BEGIN
		SELECT COUNT(Email)
		FROM User_Account
		WHERE Email = @Email
			AND PasswordHash = @PasswordHash
			AND Active = 1
	END
GO

print'' print '*** creating sp_select_user_by_email***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
	(
		@Email					[nvarchar](100)
	)
AS
	BEGIN
		SELECT UserID, UserName, Active, isAdmin, UserImage
		FROM User_Account
		WHERE Email = @Email		
	END
GO

print '' print '*** creating sp_update_passwordhash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordhash]
	(
		@Email				[nvarchar](100),
		@OldPasswordHash	[nvarchar](100),
		@NewPasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE User_Account
			SET PasswordHash = @NewPasswordHash
			WHERE Email = @Email
			  AND PasswordHash = @OldPasswordHash
		RETURN @@ROWCOUNT
	END
GO

print'' print '*** creating sp_select_all_users***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_users]
AS
	BEGIN
		SELECT UserID, UserName, Active, isAdmin, UserImage
		FROM User_Account		
	END
GO


print '' print '*** creating sp_set_user_account_active***'
GO
CREATE PROCEDURE [dbo].[sp_set_user_account_active_by_id]
	(
		@UserID [int],
		@Active [bit]
	)
AS
	BEGIN
		UPDATE User_Account
		SET Active = @Active
		WHERE UserID = @UserID
	END
GO

print '' print '*** creating sp_set_user_account_active***'
GO
CREATE PROCEDURE [dbo].[sp_set_user_account_isAdmin_by_id]
	(
		@UserID [int],
		@isAdmin [bit]
	)
AS
	BEGIN
		UPDATE User_Account
		SET isAdmin = @isAdmin
		WHERE UserID = @UserID
	END
GO

print '' print '*** creating sp_update_userimage ***'
GO
CREATE PROCEDURE [dbo].[sp_update_userimage]
	(
		@UserID [int],
		@UserImage [varbinary](max)
	)
AS
	BEGIN
		UPDATE User_Account
			SET UserImage = @UserImage
			WHERE UserID = @UserID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** creating sp_add_user_to_server_user_list'
GO
CREATE PROCEDURE [dbo].[sp_add_user_to_server_user_list]
(
	@UserID [int],
	@ServerID [int],
	@isModerator [bit] = 0
)
AS
	BEGIN
		INSERT INTO server_user_list
			(UserID, ServerID, isModerator)
		VALUES
			(@UserID, @ServerID, @isModerator)
	END
GO

print '' print '*** creating sp_remove_user_from_server_user_list'
GO
CREATE PROCEDURE [dbo].[sp_remove_user_from_server_user_list]
(
	@UserID [int],
	@ServerID [int]
)
AS
	BEGIN
		DELETE FROM server_user_list
		WHERE UserID = @UserID
			AND ServerID = @ServerID
			AND isModerator = 0;
	END
GO


print '' print '*** creating sp_delete_chatroom_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_chatroom_by_id]
(
	@ChatroomID [int]
)
AS
	BEGIN
		DELETE FROM Chatroom_Message_List
		WHERE ChatroomID = @ChatroomID
		;
		DELETE FROM Chatroom
		WHERE ChatroomID = @ChatroomID
	END
GO


print '' print '*** creaing sp_add_user_to_server_user_list_by_tag'
GO
CREATE PROCEDURE [dbo].[sp_add_user_to_server_user_list_by_tag]
(
	@UserID [int],
	@Tag 	[nvarchar]	(8)
)
AS
	BEGIN
		INSERT INTO server_user_list
			(UserID, ServerID)
		VALUES
			(@UserID, (SELECT ServerID FROM Server WHERE Tag = LOWER(@Tag)))
	END
GO

print '' print '*** creating sp_insert_new_server'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_server]
(				
	@UserID		[int],		
	@Name		[nvarchar]	(100),
	@Tag		[nvarchar]  (8),
	@ServerImage [varbinary](max) = null
)
AS
	BEGIN TRY
		BEGIN TRANSACTION
			DECLARE @ServerID [int]
			INSERT INTO Server
				(UserID, Name, Tag, ServerImage)
			VALUES
				(@UserID, @Name, UPPER(@Tag), @ServerImage)
			SET @ServerID = SCOPE_IDENTITY()			
			;
			EXEC sp_add_user_to_server_user_list @UserID = @UserID, @ServerID = @ServerID, @isModerator = 1;
				
		COMMIT
		RETURN @ServerID
				
	END TRY
			
	BEGIN CATCH
				
		ROLLBACK
		RETURN 0
				
	END CATCH
	
GO

print '' print '*** creating sp_update_server ***'
GO
CREATE PROCEDURE [dbo].[sp_update_server]
(				
	@UserID		[int],
	@ServerID	[int],
	@Name		[nvarchar]	(100),
	@Tag		[nvarchar]  (8),
	@ServerImage [varbinary](max) = null
)
AS
	BEGIN TRY
		BEGIN TRANSACTION
		UPDATE Server
		SET Server.Name = @Name,
			Server.Tag = @Tag		
		WHERE Server.UserID = @UserID
			AND Server.ServerID = @ServerID
		;
		IF @ServerImage IS NOT NULL
			BEGIN
				UPDATE Server
				SET Server.ServerImage = @ServerImage
				WHERE Server.UserID = @UserID
					AND Server.ServerID = @ServerID
				;
			END
			
		COMMIT
		RETURN @@ROWCOUNT
				
	END TRY
			
	BEGIN CATCH
		
		ROLLBACK
		RETURN 0
				
	END CATCH
	
GO



print'' print'*** creating sp_select_servers_by_userid ***'
GO
CREATE PROCEDURE sp_select_servers_by_userid
(
	@UserID	[int]
)
AS
	BEGIN
		SELECT Server.ServerID, Server.CreatedAt, Server.Tag, Server.UserID, Server.Name, Server.LastMessage, User_Account.UserName, Server.ServerImage
		FROM Server
		JOIN Server_User_List
			ON Server.ServerID = Server_User_List.ServerID
		JOIN User_Account
			ON Server.UserID = User_Account.UserID
		WHERE Server_User_List.UserID = @UserID
	END
GO
	
print'' print'*** creating sp_select_users_by_serverid ***'
GO
CREATE PROCEDURE sp_select_users_by_serverid
(
	@ServerID	[int]
)
AS
	BEGIN
		SELECT User_Account.UserID, User_Account.UserName, Server_User_List.isModerator, User_Account.isAdmin, User_Account.UserImage 
		FROM User_Account
		JOIN Server_User_List			
			ON Server_User_List.UserID = User_Account.UserID
		WHERE Server_User_List.ServerID = @ServerID
			AND Active = 1;
	END
GO	
		
		

print''print'*** creating sp_insert_chatroom'
GO
CREATE PROCEDURE [dbo].[sp_insert_chatroom]
(	
	@ServerID		[int]		,
	@Name			[nvarchar]	(100)
)
AS
	BEGIN TRY
		INSERT INTO Chatroom
			(ServerID, Name)
		VALUES
			(@ServerID, @Name)
		RETURN SCOPE_IDENTITY()
	END TRY
	BEGIN CATCH
	
		RETURN 0
	
	END CATCH
GO

print'' print'*** creating sp_select_chatrooms_by_serverid ***'
GO
CREATE PROCEDURE sp_select_chatrooms_by_serverid
(
	@ServerID	[int]
)
AS
	BEGIN
		SELECT Chatroom.ChatroomID, Chatroom.CreatedAt, Chatroom.Name, Chatroom.LastMessage
		FROM Chatroom
		WHERE Chatroom.ServerID = @ServerID;
	END
GO




print''print'*** creating sp_insert_message'
GO
CREATE PROCEDURE [dbo].[sp_insert_message]
(	
	@UserID			[int]		,
	@Message		[nvarchar](2000),
	@Media			[varbinary](max) = NULL
)
AS
		BEGIN
			INSERT INTO Message
				(UserID, Message)
			VALUES
				(@UserID, @Message)
			RETURN SCOPE_IDENTITY()
		END
	
	
GO
			
			

print'' print '*** creating sp_insert_chatroom_message'
GO
CREATE PROCEDURE [dbo].[sp_insert_chatroom_message]
(
	@UserID			[int]		,
	@Message		[nvarchar]	(2000),
	@hasMedia		[bit]	= 0 ,
	@ChatroomID		[int]		
)
AS
	BEGIN TRY	
		BEGIN TRANSACTION
			DECLARE @MessageID [int]
			EXEC @MessageID = sp_insert_message @UserID = @UserID, @Message = @Message;
			;
			INSERT INTO Chatroom_Message_List
				(ChatroomID, MessageID)
			VALUES
				(@ChatroomID, @MessageID)
			;
			
			UPDATE Chatroom
			SET LastMessage = GETDATE()
			WHERE ChatroomID = @ChatroomID
			;
			UPDATE [dbo].[Server]
			SET LastMessage = GETDATE()
			FROM Server
				JOIN Chatroom
				ON Chatroom.ServerID = Server.ServerID
			WHERE ChatroomID = @ChatroomID
			;
		COMMIT
	END TRY
			
	BEGIN CATCH
			
		ROLLBACK
		RETURN 0
		
	END CATCH
	RETURN @@ROWCOUNT
GO
			
print'' print '*** creating sp_insert_direct_message'
GO
CREATE PROCEDURE [dbo].[sp_insert_direct_message]
(
	@SenderUserID	[int]		,
	@CreatedAt		[DateTime]	,
	@Message		[nvarchar]	(2000),
	@hasMedia		[bit]		,
	@ReceiverUserID	[int]		
)
AS
	BEGIN TRY
		BEGIN TRANSACTION
			DECLARE @MessageID [int]
			DECLARE @RowsAffected [int]
			EXEC sp_insert_message @UserID = @SenderUserID, @Message = @Message;
			SET @MessageID = SCOPE_IDENTITY()
			SET @RowsAffected = @@ROWCOUNT
			;
			INSERT INTO Direct_Message_List
				(SenderUserID, ReceiverUserID, MessageID)
			VALUES
				(@SenderUserID, @ReceiverUserID, @MessageID)
			SET @RowsAffected += @@ROWCOUNT
			;
		COMMIT
	END TRY
			
	BEGIN CATCH
			
		ROLLBACK
				
	END CATCH
	RETURN @RowsAffected
GO		
			
print '' print'*** creating sp_select_messages_by_chatroomID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_messages_by_chatroomID]
(
	@ChatroomID		[int]
)
AS
	BEGIN
		SELECT 
			Message.MessageID,
			Message.UserID,	
			Message.CreatedAt,
			Message.Message,	
		    Message.hasMedia,
			Message.isVisible,
			User_Account.UserName,
			User_Account.UserImage
		FROM Message
		JOIN Chatroom_Message_List
			ON Message.MessageID = Chatroom_Message_List.MessageID
		JOIN User_Account
			ON User_Account.UserID = Message.UserID
		WHERE Message.isVisible = 1
			AND Chatroom_Message_List.ChatroomID = @ChatroomID
	END
GO


			
print'' print '*** ADDING DATA USING STORED PROCEDURES ***'
print'' print '*** adding users with sp_insert_new_user_account***'
DECLARE @UserID_Zero [int]
DECLARE @UserID_One [int]
DECLARE @UserID_Two [int]
DECLARE @UserID_Three [int]

EXEC @UserID_Zero = sp_insert_new_user_account 
	@Email = 'jakub@domain.com', 
	@UserName = 'Kuba', 
	@PasswordHash = 'b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342'
;

UPDATE User_Account
SET isAdmin = 1
WHERE UserName = 'Kuba'
;
	


EXEC @UserID_One = sp_insert_new_user_account 
	@Email = 'greg@domain.com', 
	@UserName = 'Greg_The_Red', 
	@PasswordHash = 'b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342'
;
EXEC @UserID_Two = sp_insert_new_user_account 
	@Email = 'frank@domain.com', 
	@UserName = 'Franky', 
	@PasswordHash = 'b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342'
;
EXEC @UserID_Three = sp_insert_new_user_account 
	@Email = 'sarah@domain.com', 
	@UserName = 'Sneaky_Sarah', 
	@PasswordHash = 'b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342'
;	
print'' print'*** adding server with sp_insert_new_server ***'
DECLARE @ServerID_Zero [int]
EXEC @ServerID_Zero = sp_insert_new_server 
	@UserID = @UserID_Zero, 
	@Name = "Kuba's Server", 
	@Tag = 'KUBAkuba'
;


print'' print'*** adding users to the new server with sp_add_user_to_server_user_list ***'
EXEC sp_add_user_to_server_user_list @UserID = @UserID_One, @ServerID = @ServerID_Zero;
EXEC sp_add_user_to_server_user_list @UserID = @UserID_Two, @ServerID = @ServerID_Zero;

print'' print'*** adding users to the new server with sp_add_user_to_server_user_list_by_tag ***'
EXEC sp_add_user_to_server_user_list_by_tag @UserID = @UserID_Three, @Tag = 'kuBakubA';

print'' print'*** adding chatroom to server with sp_insert_chatroom ***'
DECLARE @ChatroomID_Zero [int]
EXEC @ChatroomID_Zero = sp_insert_chatroom @ServerID = @ServerID_Zero, @Name = 'The Fun Room';

print'' print'***adding messages with sp_insert_chatroom_message ***'
EXEC sp_insert_chatroom_message 
	@UserID = @UserID_Zero, 
	@Message = "Welcome to the chatroom, this is the first message!", 
	@ChatroomID = @ChatroomID_Zero
;
EXEC sp_insert_chatroom_message 
	@UserID = @UserID_Zero, 
	@Message = "Second message. Watch out!", 
	@ChatroomID = @ChatroomID_Zero
;
EXEC sp_insert_chatroom_message 
	@UserID = @UserID_One, 
	@Message = "Crazy stuff man.", 
	@ChatroomID = @ChatroomID_Zero
;
EXEC sp_insert_chatroom_message 
	@UserID = @UserID_Two, 
	@Message = "I like turtles.", 
	@ChatroomID = @ChatroomID_Zero
;
EXEC sp_insert_chatroom_message 
	@UserID = @UserID_Three, 
	@Message = "Me too! And I am making thi message super extra duper long so that we can test the apps wraping system, Oh boy I hope it works", 
	@ChatroomID = @ChatroomID_Zero
;

