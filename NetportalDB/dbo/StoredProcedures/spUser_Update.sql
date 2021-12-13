CREATE PROCEDURE [dbo].[spUser_Update]
	@Id INT,
    @instelling_id int, 
    @voornaam varchar(50), 
    @achternaam varchar(50), 
    @email varchar(50), 
    @status varchar(50)
AS
begin
    update dbo.[user] 
    set
    instelling_id = @instelling_id,
    voornaam = @voornaam,
    achternaam =  @achternaam, 
    email = @email, 
    [status] = @status
    where Id = @Id;
end