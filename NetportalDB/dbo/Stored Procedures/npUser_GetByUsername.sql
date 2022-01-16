CREATE PROCEDURE [dbo].[npUser_GetByUsername]
	@username varchar(50)
AS
begin
    select [user_id], instelling_id, voornaam, achternaam, username, [password], email, [status], internal_user, failed_attempts, pwd_exp_date, pwd_changed_date
    from dbo.[User]
    where username = @username
end

