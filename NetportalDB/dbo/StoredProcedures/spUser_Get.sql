CREATE PROCEDURE [dbo].[spUser_Get]
	@Id INT
AS
begin
    select Id, instelling_id, voornaam, achternaam, username, [password], email, [status], internal_user, failed_attempts, pwd_exp_date, pwd_changed_date
    from dbo.[User]
    where Id = @Id
end

