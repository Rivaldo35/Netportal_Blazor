CREATE PROCEDURE [dbo].[spUser_Insert]
    @instelling_id int, 
    @voornaam varchar(50), 
    @achternaam varchar(50), 
    @username varchar(50), 
    @password varchar(MAX), 
    @email varchar(50), 
    @status varchar(50), 
    @internal_user varchar(50), 
    @failed_attempts int, 
    @pwd_exp_date date, 
    @pwd_changed_date date
AS
begin
    insert into dbo.[user] (instelling_id, voornaam, achternaam, username, [password], email, [status], internal_user, failed_attempts, pwd_exp_date, pwd_changed_date)
    values(@instelling_id, @voornaam, @achternaam, @username, @password, @email, @status, @internal_user, @failed_attempts, @pwd_exp_date, @pwd_changed_date);
end

