CREATE PROCEDURE [dbo].[spUser_Insert]
    @instelling_id int, 
    @voornaam varchar(50), 
    @achternaam varchar(50), 
    @email varchar(50)

AS
begin
    insert into dbo.[user] (instelling_id, voornaam, achternaam, email)
    values(@instelling_id, @voornaam, @achternaam, @email);
end

